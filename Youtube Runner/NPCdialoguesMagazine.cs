using System;
using UnityEngine;

public class NPCdialoguesMagazine : MonoBehaviour
{
    public static NPCdialoguesMagazine Instance;

    [SerializeField] private SNPCdialogue[] dialogues;

    private void Awake()
    {
        Instance = this;
    }

    public SNPCdialogue GetDialogue(string dialogueName)
    {
       SNPCdialogue dialogueToReturn = Array.Find(dialogues, dummyFind => dummyFind._dialogueName == dialogueName);
        if (dialogueToReturn != null)
            return dialogueToReturn;
        else
        {
            Debug.LogError("No dialogue with name " + dialogueName + " was found");
            return null;
        }
    }
}