using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemPriceText;

    [Header("Values")]
    public ShopItems ItemType;
    public enum ShopItems { strongerOars, pirateGreed, pirateLuck, seaHeart, nets, lantern, headstart, dummyItem4 }

    [SerializeField] private string itemName = "Placeholder";
    [SerializeField] private int itemPrice = 10;
    [SerializeField] private int itemLevel;
    [SerializeField] private int itemLevelMax = 5;

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
            PlayerPrefs.SetInt(ItemType.ToString(), itemLevel);

            PlayerMoney.Instance.AddMoneyAndSave(-itemPrice);

            UpdateItemUI();
            ShopManager.Instance.UpdateMoneyInShopUI();
            LockedShopItem.UpdateAllLockedItems();
        }
    }

    private void UpdateItemUI()
    {
        itemLevel = PlayerPrefs.GetInt(ItemType.ToString());

        itemNameText.text = "LV. " + itemLevel + " " + itemName;
        itemPriceText.text = itemPrice + " G";

        if (itemLevel == itemLevelMax)
        {
            itemNameText.text = "MAX LV: " + itemName;
            itemPriceText.text = "0 G";
        }
    }
}