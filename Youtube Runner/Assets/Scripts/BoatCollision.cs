using System.Collections;
using UnityEngine;

public class BoatCollision : MonoBehaviour, IHeadStartReceiver
{
    public static BoatCollision Instance;

    private SpriteRenderer sr;
    
    [SerializeField] private int lives = 1;

    public bool isInHeadstart { get; private set; }

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
            StartCoroutine(OnLostLifeWithoutLosing("heart"));
        }
    }

    private IEnumerator OnLostLifeWithoutLosing(string animationItemToSpawn)
    {
        Time.timeScale = 0.5f;
        gameObject.layer = 8;

        AnimationPrefabs.Instance.SpawnAnimation(animationItemToSpawn);

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
        StartCoroutine(OnLostLifeWithoutLosing("heart"));
    }

    public void ActivateHeadstart(float speedMultiplier)
    {
        isInHeadstart = true;
        gameObject.layer = 8;
    }

    public void EndHeadstart()
    {
        isInHeadstart = false;
        StartCoroutine(OnLostLifeWithoutLosing("headstart"));
    }
}