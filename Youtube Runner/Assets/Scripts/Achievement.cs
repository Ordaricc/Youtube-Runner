using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    private Image img;

    public enum AchievementTypes { undefined, ocotpus, seeFog, horca, wind,
        whirlwind, thunderstorm, defeatEnemiesInSingleRun, dontLoseBootyInARun, buyAllItems }

    [SerializeField] private AchievementTypes achievementType;
    public AchievementTypes _achievementType { get { return achievementType; } }

    [SerializeField] private string trophyName;
    [SerializeField] private string trophyDescription;
    
    private void Awake()
    {
        img = GetComponent<Image>();
        CheckIfAchievementIsUnlocked();
    }

    public void CheckIfAchievementIsUnlocked()
    {
        if (PlayerPrefs.GetInt(achievementType.ToString()) == 0)
        {
            img.color = Color.black;
        }
        else
        {
            img.color = Color.white;
        }
    }

    public void UnlockThisAchievement()
    {
        PlayerPrefs.SetInt(achievementType.ToString(), 1);
        Awake();
    }

    public void OnTouchTrophy()
    {
        AchievementsUIManager.Instance.UpdateTrophyTextsUI(trophyName, trophyDescription);
    }
}