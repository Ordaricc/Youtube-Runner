using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "New Dialogue", menuName = "Create Scriptable/Dialogue")]
public class SNPCdialogue : ScriptableObject
{
    [SerializeField] private string dialogueName;
    public string _dialogueName { get { return dialogueName; } }

    [SerializeField, TextArea] private string[] dialogueLines;

    public bool GetDialogueLine(int dialogueIndex, out string dialogueLine)
    {
        if (dialogueIndex == dialogueLines.Length)
        {
            dialogueLine = null;
            return false;
        }
        else
        {
            dialogueLine = dialogueLines[dialogueIndex];
            return true;
        }
    }
}