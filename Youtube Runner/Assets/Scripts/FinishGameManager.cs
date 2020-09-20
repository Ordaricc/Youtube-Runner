using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishGameManager : MonoBehaviour
{
    public static FinishGameManager Instance;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI youLostText;

    private void Awake()
    {
        Instance = this;
    }

    public void FinishGame()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);

        bool isNewHighscore = YardsManager.Instance.CheckNewHigscore();
        if (isNewHighscore)
            youLostText.text = "New highscore!";

        int moneyMadeThisGame = PlayerMoney.Instance.GetMoneyMadeAndSaveMoney();
        moneyText.text = "Money: " + moneyMadeThisGame;
    }
    
    public void CheckIfPlayerCanRestartGame()
    {
        bool isGonnaWatchAd = WatchAdWhenLosing.Instance.ShouldPlayerWatchInterstitialAd();
        if (!isGonnaWatchAd)
            RestartGame();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}