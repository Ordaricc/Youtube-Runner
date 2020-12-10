using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [SerializeField] private TextMeshProUGUI moneyInShopText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateMoneyInShopUI();
    }

    public void UpdateMoneyInShopUI()
    {
        moneyInShopText.text = PlayerMoney.Instance.ReturnCurrentMoney() + " GOLD";
    }

    public void DebugAddMoney()
    {
        PlayerMoney.Instance.AddMoneyAndSave(1000);
        UpdateMoneyInShopUI();
    }
}