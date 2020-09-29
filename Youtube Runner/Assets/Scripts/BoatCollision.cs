using System.Collections;
using UnityEngine;

public class BoatCollision : MonoBehaviour, IHeadStartReceiver
{
    public static BoatCollision Instance;

    private SpriteRenderer sr;

    [SerializeField] private GameObject lostLifeAnimationPrefab;
    [SerializeField] private GameObject headstartAnimationPrefab;
    [SerializeField] private int lives = 1;

    private void Awake()
    {
        Instance = this;

        sr = GetComponent<SpriteRenderer>();
    }

    public void StartScript()
    {
        lives += PlayerPrefs.GetInt(ShopItem.ShopItems.seaHeart.ToString());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Entity"))
        {
            EntityType entityTypeCollidedWith = collision.GetComponent<EntityType>();

            if (entityTypeCollidedWith.entityType == EntityType.EntityTypes.booty)
            {
                PlayerMoney.Instance.CollectBooty(1);
                Destroy(collision.gameObject);
            }
            else
            {
                if (entityTypeCollidedWith._isAnimal)
                {
                    if (Nets.Instance.TryRemoveNet())
                    {
                        //CHANGE THIS: make a new function on each entityType script to tell how to destroy them properly
                        if (entityTypeCollidedWith.entityType == EntityType.EntityTypes.tentacle)
                        {
                            Destroy(collision.gameObject.transform.parent.gameObject);
                        }
                        else
                        {
                            Destroy(collision.gameObject);
                        }
                    }
                    else
                    {
                        OnHitEnemy();
                    }
                }
                else
                {
                    OnHitEnemy();
                }
            }
        }
    }

    private void OnHitEnemy()
    {
        lives--;
        if (lives <= 0)
        {
            FinishGameManager.Instance.FinishGame();
        }
        else
        {
            StartCoroutine(OnLostLifeWithoutLosing(lostLifeAnimationPrefab));
        }
    }

    private IEnumerator OnLostLifeWithoutLosing(GameObject animationItemToSpawn)
    {
        Time.timeScale = 0.5f;
        gameObject.layer = 8;

        GameObject lostLifeAnimationSpawned = Instantiate(animationItemToSpawn, transform.position, Quaternion.identity);
        Destroy(lostLifeAnimationSpawned, 2);

        int i = 0;
        while (i < 8)
        {
            i++;
            sr.enabled = false;
            yield return new WaitForSeconds(0.125f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.125f);
        }
        
        Time.timeScale = 1;
        gameObject.layer = 10;
    }

    public void SecondChance()
    {
        lives++;
        StartCoroutine(OnLostLifeWithoutLosing(lostLifeAnimationPrefab));
    }

    public void ActivateHeadstart(float speedMultiplier)
    {
        gameObject.layer = 8;
    }

    public void EndHeadstart()
    {
        StartCoroutine(OnLostLifeWithoutLosing(headstartAnimationPrefab));
    }
}