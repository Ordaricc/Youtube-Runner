using UnityEngine;

public class PlayerBonus : MonoBehaviour
{
    public static PlayerBonus Instance;

    [SerializeField] private Sprite bootySprite;
    [SerializeField] private Sprite lanternSprite;
    [SerializeField] private Sprite netSprite;

    [SerializeField] private GameObject bonusItemPrefab;
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
                SpawnBonusAnimationBasedOnSprite(netSprite);
                break;
            }
            else if (randomNum <= 50 && Lantern.Instance.lanternLevel > 0 && Fog.Instance.isFogOn)
            {
                Lantern.Instance.AddExtraFuel();
                SpawnBonusAnimationBasedOnSprite(lanternSprite);
                break;
            }
            else
            {
                bonusMoney = Random.Range(1, 11);
                SpawnBonusAnimationBasedOnSprite(bootySprite);
                break;
            }
        }
        
        return bonusMoney;
    }

    private void SpawnBonusAnimationBasedOnSprite(Sprite spriteToUse)
    {
        GameObject bonusAnimationSpawned = Instantiate(bonusItemPrefab, transform.position, Quaternion.identity);
        bonusAnimationSpawned.GetComponentInChildren<SpriteRenderer>().sprite = spriteToUse;
        Destroy(bonusAnimationSpawned, 2);
    }

    public void ResetBootiesCollected()
    {
        bootiesInARowCollected = 0;
    }
}