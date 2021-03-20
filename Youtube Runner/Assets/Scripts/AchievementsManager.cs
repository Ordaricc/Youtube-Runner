using System;
using UnityEngine;

public class AchievementsManager : MonoBehaviour
{
    public static AchievementsManager Instance;
    
    [SerializeField] private Achievement[] trophies;

    private void Awake()
    {
        Instance = this;
    }

    public void UnlockAchievement(Achievement.AchievementTypes achievementType)
    {
        if (PlayerPrefs.GetInt(achievementType.ToString()) == 1)
            return;

        Achievement achievementToUnlock = 
            Array.Find(trophies, dummyTrophy => dummyTrophy._achievementType == achievementType);

        if (achievementToUnlock == null)
        {
            Debug.LogWarning("Trophy not added with achievement: " + achievementType);
            return;
        }

        achievementToUnlock.UnlockThisAchievement();
    }
}