using UnityEngine;

public class BuyAllItemsAchievement : MonoBehaviour
{
    public static BuyAllItemsAchievement Instance;

    [SerializeField] private ShopItem[] shopItems;

    private void Awake()
    {
        Instance = this;
    }

    public void CheckIfAllItemsAreMaxLeveled()
    {
        foreach (ShopItem s in shopItems)
        {
            if (PlayerPrefs.GetInt(s._itemType.ToString()) != s._itemLevelMax)
                return;
        }

        AchievementsManager.Instance.UnlockAchievement(Achievement.AchievemntTypes.buyAllItems);
    }
}