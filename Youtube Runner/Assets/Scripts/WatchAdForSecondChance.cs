using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class WatchAdForSecondChance : MonoBehaviour, IAdManagerListener
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button watchAdToContinueButton;
    [SerializeField] private Button gameOverButton;

    public void GetAdResult(ShowResult adResult)
    {
        GivePlayerSecondChance(adResult);
    }

    public void WatchRewardAdToGetSecondChance()
    {
        watchAdToContinueButton.interactable = false;
        gameOverButton.interactable = false;
        
        AdManager.Instance.CallAdCoroutine(true, this);
    }

    private void GivePlayerSecondChance(ShowResult result)
    {
        gameOverButton.interactable = true;

        if (result == ShowResult.Finished)
        {
            gameOverPanel.SetActive(false);
            Time.timeScale = 1;
            BoatCollision.Instance.SecondChance();
        }
    }
}