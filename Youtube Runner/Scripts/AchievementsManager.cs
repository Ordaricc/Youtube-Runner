using System;
using UnityEngine;
using TMPro;

public class AchievementsManager : MonoBehaviour
{
    public static AchievementsManager Instance;

    [SerializeField] private TextMeshProUGUI trophyNameText;
    [SerializeField] private TextMeshProUGUI trophyDescriptionText;

    [SerializeField] private Achievement[] trophies;

    private void Awake()
    {
        Instance = this;
    }

    public void UnlockAchievement(Achievement.AchievemntTypes achievementType)
    {
        Achievement achievementToUnlock = 
            Array.Find(trophies, dummyTrophy => dummyTrophy._achievementType == achievementType);

        if (achievementToUnlock == null)
        {
            Debug.LogWarning("Trophy not added with achievement: " + achievementType);
            return;
        }

        if (!achievementToUnlock.isUnlocked)
            achievementToUnlock.UnlockThisAchievement();
    }

    public void UpdateTrophyTextsUI(string trophyName, string trophyDescription)
    {
        trophyNameText.text = trophyName;
        trophyDescriptionText.text = trophyDescription;
    }
}