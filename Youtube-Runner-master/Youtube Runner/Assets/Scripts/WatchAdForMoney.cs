using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class WatchAdForMoney : MonoBehaviour, IAdManagerListener
{
    [SerializeField] private Button watchAdForMoneyButton;
    [SerializeField] private Button playButton;

    [SerializeField] private int adMoneyToGiveToPlayer = 100;

    public void GetAdResult(ShowResult adResult)
    {
        GiveThePlayerAdMoney(adResult);
    }

    public void WatchRewardAdForMoney()
    {
        watchAdForMoneyButton.interactable = false;
        playButton.interactable = false;
        
        AdManager.Instance.CallAdCoroutine(true, this);
    }

    private void GiveThePlayerAdMoney(ShowResult result)
    {
        playButton.interactable = true;

        if (result == ShowResult.Finished)
        {
            PlayerMoney.Instance.AddMoneyAndSave(adMoneyToGiveToPlayer);
            ShopManager.Instance.UpdateMoneyInShopUI();
        }
        else if (result == ShowResult.Failed)
        {
            watchAdForMoneyButton.interactable = true;
        }
    }
}