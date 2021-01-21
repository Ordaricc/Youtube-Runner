using UnityEngine;

public class DontLoseBootyAchievement : MonoBehaviour
{
    public static DontLoseBootyAchievement Instance;

    [SerializeField] private int minimumAmountOfYardsTraveled;
    private bool hasPlayerLostBootyThisGame;

    private void Awake()
    {
        Instance = this;
    }

    public void OnPlayerLoseBooty()
    {
        if (!hasPlayerLostBootyThisGame)
        {
            if (YardsManager.Instance.yardsTraveled < minimumAmountOfYardsTraveled && !BoatCollision.Instance.isInHeadstart)
            {
                hasPlayerLostBootyThisGame = true;
            }
        }
    }

    public void OnGameEnd()
    {
        if (!hasPlayerLostBootyThisGame && YardsManager.Instance.yardsTraveled >= minimumAmountOfYardsTraveled)
            AchievementsManager.Instance.UnlockAchievement(Achievement.AchievemntTypes.dontLoseBootyInARun);
    }
}