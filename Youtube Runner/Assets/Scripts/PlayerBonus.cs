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

        while (true)
        {
            if (randomNum <= 25 && Nets.Instance.netsLevel > 0 && Nets.Instance.canAddNet)
            {
                Nets.Instance.AddNet();
                AnimationPrefabs.Instance.SpawnAnimation("net");
                break;
            }
            else if (randomNum <= 50 && Lantern.Instance.lanternLevel > 0 && Fog.Instance.isFogOn)
            {
                Lantern.Instance.AddExtraFuel();
                AnimationPrefabs.Instance.SpawnAnimation("lantern");
                break;
            }
            else
            {
                bonusMoney = Random.Range(1, 11);
                AnimationPrefabs.Instance.SpawnAnimation("booty");
                break;
            }
        }
        
        return bonusMoney;
    }
    
    public void ResetBootiesCollected()
    {
        bootiesInARowCollected = 0;
    }
}