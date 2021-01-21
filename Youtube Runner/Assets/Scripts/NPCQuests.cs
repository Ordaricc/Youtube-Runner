using UnityEngine;

public static class NPCQuests
{
    private const string unlockAllEnemiesQuestPref = "unlockAllEnemiesQuestPref";
    private const string gamesPlayedPref = "gamesPlayedPref";
    private const string yardsTraveledFirstPlaythrough = "yardsTraveledFirstPlaythrough";

    public static void OnUnlockAllEnemies()
    {
        if (PlayerPrefs.GetInt(unlockAllEnemiesQuestPref) == 0)
        {
            PlayerPrefs.SetInt(unlockAllEnemiesQuestPref, 1);
            NPCManager.Instance.ActivateNPC(NPCdialoguesMagazine.Instance.GetDialogue("UnlockAllEnemies"));
        }
    }

    public static void OnGameEnd()
    {
        int gamesPlayed = PlayerPrefs.GetInt(gamesPlayedPref);
        if (gamesPlayed == 0)
        {
            PlayerPrefs.SetInt(yardsTraveledFirstPlaythrough, (int)YardsManager.Instance.yardsTraveled);
        }
        PlayerPrefs.SetInt(gamesPlayedPref, gamesPlayed + 1);
    }

    public static void OnGameStart()
    {
        if (PlayerPrefs.GetInt(gamesPlayedPref) == 1)
        {
            NPCManager.Instance.OnDialogueLoadEvent += OnDialogueLoadSecondPlaythrough;
            NPCManager.Instance.ActivateNPC(NPCdialoguesMagazine.Instance.GetDialogue("SecondPlaythrough"));
        }
    }

    public static void OnDialogueLoadSecondPlaythrough(int currentDialogueIndex)
    {
        if (currentDialogueIndex == 1)
        {
            string yardsTraveled = PlayerPrefs.GetInt(yardsTraveledFirstPlaythrough).ToString();
            string newText = NPCManager.Instance._dialogueBoxText.text.Replace("[yards]", yardsTraveled);
            NPCManager.Instance._dialogueBoxText.text = newText;
        }
        else if (currentDialogueIndex == 3)
        {
            PlayerMoney.Instance.AddMoney(100);
            AnimationPrefabs.Instance.SpawnAnimation("booty");
        }
    }
}