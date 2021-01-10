using UnityEngine;
using TMPro;

public class NPCManager : MonoBehaviour
{
    public static NPCManager Instance;

    [SerializeField] private NPC currentNPC;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueBoxText;
    public TextMeshProUGUI _dialogueBoxText { get { return dialogueBoxText; } }

    [SerializeField] private SNPCdialogue currentDialogue;
    private int currentDialogueIndex;

    public delegate void OnDialogueLoad(int currentDialogueIndex);
    public OnDialogueLoad OnDialogueLoadEvent;

    private void Awake()
    {
        Instance = this;
    }
    
    public void ActivateNPC(SNPCdialogue dialogueToLoad)
    {
        currentDialogue = dialogueToLoad;
        SpawnManager.Instance.ChangeCanSpawnTo(false);
        currentNPC.ActivateNPC();
    }

    public void StartNPCDialogue()
    {
        Time.timeScale = 0;
        dialogueBox.SetActive(true);
        LoadNextDialogueText();
    }

    public void LoadNextDialogueText()
    {
        if (currentDialogue.GetDialogueLine(currentDialogueIndex, out string dialogueLine))
        {
            dialogueBoxText.text = dialogueLine;
            OnDialogueLoadEvent?.Invoke(currentDialogueIndex);
            currentDialogueIndex++;
        }
        else
        {
            OnDialogueLoadEvent = null;
            currentDialogueIndex = 0;
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        Time.timeScale = 1;
        dialogueBox.SetActive(false);
    }
}