using UnityEngine;
using UnityEngine.Advertisements;

public class WatchAdWhenLosing : MonoBehaviour, IAdManagerListener
{
    public static WatchAdWhenLosing Instance;

    [SerializeField] private int howManyGamesToLoseToWatchInterstitialAd = 4;

    private const string gamesInARowLost = "gamesInARowLost";

    private void Awake()
    {
        Instance = this;
    }

    public bool ShouldPlayerWatchInterstitialAd()
    {
        int gamesLost = PlayerPrefs.GetInt(gamesInARowLost) + 1;
        PlayerPrefs.SetInt(gamesInARowLost, gamesLost);

        if (gamesLost >= howManyGamesToLoseToWatchInterstitialAd)
        {
            PlayerPrefs.SetInt(gamesInARowLost, 0);
            WatchInterstitialAd();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void WatchInterstitialAd()
    {
        AdManager.Instance.CallAdCoroutine(false, this);
    }

    private void OnWatchedInterstitialAd()
    {
        FinishGameManager.Instance.RestartGame();
    }

    public void GetAdResult(ShowResult adResult)
    {
        OnWatchedInterstitialAd();
    }
}