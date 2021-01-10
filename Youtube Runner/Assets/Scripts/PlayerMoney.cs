using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    public static PlayerMoney Instance;

    [SerializeField] private int currentMoney;

    public const string prefMoney = "prefMoney";

    private void Awake()
    {
        Instance = this;

        currentMoney = PlayerPrefs.GetInt(prefMoney);
    }

    public void CollectBooty(int moneyToAdd)
    {
        currentMoney += moneyToAdd;
        currentMoney += PlayerBonus.Instance.OnCollectBooty();
    }

    public void AddMoney(int moneyToAdd)
    {
        currentMoney += moneyToAdd;
    }

    public void AddMoneyAndSave(int moneyToAdd)
    {
        currentMoney += moneyToAdd;
        PlayerPrefs.SetInt(prefMoney, currentMoney);
    }

    public int GetMoneyMadeAndSaveMoney()
    {
        int moneyMadeThisGame = currentMoney - PlayerPrefs.GetInt(prefMoney);
        PlayerPrefs.SetInt(prefMoney, currentMoney);

        return moneyMadeThisGame;
    }

    public int ReturnCurrentMoney()
    {
        return currentMoney;
    }
}