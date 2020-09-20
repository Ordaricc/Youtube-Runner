using UnityEngine;
using UnityEngine.UI;

public class LockedShopItem : MonoBehaviour
{
    [SerializeField] private Button buyButton = null;

    [SerializeField] private ShopItem.ShopItems itemType;
    [SerializeField] private int itemLevel = 1;

    private void OnEnable()
    {
        UpdateAvailability();
    }

    private void UpdateAvailability()
    {
        buyButton.interactable = IsItemUnlocked();
    }

    private bool IsItemUnlocked()
    {
        return PlayerPrefs.GetInt(itemType.ToString()) >= itemLevel;
    }

    public static void UpdateAllLockedItems()
    {
        LockedShopItem[] lockedItemsInSceneAndActive = FindObjectsOfType<LockedShopItem>();
        foreach (LockedShopItem l in lockedItemsInSceneAndActive)
        {
            l.UpdateAvailability();
        }
    }
}