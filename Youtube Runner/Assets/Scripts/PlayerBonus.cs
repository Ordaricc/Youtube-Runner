using UnityEngine;

public class PlayerBonus : MonoBehaviour
{
    public static PlayerBonus Instance;
    
    [SerializeField] private int bootiesInARowCollected;
    [SerializeField] private int bootiesInARowNeededForBonus = 5;

    private void Awake()
    {
        Instance = this;
    }

    public void StartScript()
    {
        bootiesInARowNeededForBonus -= PlayerPrefs.GetInt(ShopItem.ShopItems.pirateGreed.ToString());
    }

    public int OnCollectBooty()
    {
        bootiesInARowCollected++;
        if (bootiesInARowCollected == bootiesInARowNeededForBonus)
        {
            ResetBootiesCollected();
            return GetBonus();
        }
        else
        {
            return 0;
        }
    }

    private int GetBonus()
    {
        int bonusMoney = 0;
        int randomNum = Random.Range(1, 101);

        if (randomNum <= 25 && Nets.Instance.CanAddNet)
        {
            Nets.Instance.AddNet();
            AnimationPrefabs.Instance.SpawnAnimation("net");
        }
        else if (randomNum <= 50 && Lantern.Instance.CanAddLanternFuel)
        {
            Lantern.Instance.AddExtraFuel();
            AnimationPrefabs.Instance.SpawnAnimation("lantern");
        }
        else
        {
            bonusMoney = Random.Range(1, 11);
            AnimationPrefabs.Instance.SpawnAnimation("booty");
        }

        return bonusMoney;
    }
    
    public void ResetBootiesCollected()
    {
        bootiesInARowCollected = 0;
    }
}