using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemPriceText;

    [Header("Values")]
    [SerializeField] private ShopItems itemType;
    public ShopItems _itemType { get { return itemType; } }

    public enum ShopItems { strongerOars, pirateGreed, pirateLuck, seaHeart, nets, lantern, headstart, dummyItem4 }

    [SerializeField] private string itemName = "Placeholder";
    [SerializeField] private int itemPrice = 10;
    [SerializeField] private int itemLevel;
    [SerializeField] private int itemLevelMax = 5;
    public int _itemLevelMax { get { return itemLevelMax; } }

    private void Awake()
    {
        UpdateItemUI();
    }

    public void BuyItem()
    {
        if (itemLevel < itemLevelMax 
            && PlayerMoney.Instance.ReturnCurrentMoney() >= itemPrice)
        {
            itemLevel++;
            PlayerPrefs.SetInt(itemType.ToString(), itemLevel);

            PlayerMoney.Instance.AddMoneyAndSave(-itemPrice);

            UpdateItemUI();
            ShopManager.Instance.UpdateMoneyInShopUI();
            LockedShopItem.UpdateAllLockedItems();
            BuyAllItemsAchievement.Instance.CheckIfAllItemsAreMaxLeveled();
        }
    }

    private void UpdateItemUI()
    {
        itemLevel = PlayerPrefs.GetInt(itemType.ToString());

        itemNameText.text = "LV. " + itemLevel + " " + itemName;
        itemPriceText.text = itemPrice + " G";

        if (itemLevel == itemLevelMax)
        {
            itemNameText.text = "MAX LV: " + itemName;
            itemPriceText.text = "0 G";
        }
    }
}