using System.Collections;
using UnityEngine;

public class BoatCollision : MonoBehaviour, IHeadStartReceiver
{
    public static BoatCollision Instance;

    private SpriteRenderer sr;
    
    [SerializeField] private int lives = 1;

    public bool isInHeadstart { get; private set; }
    private WaitForSeconds hitAnimation = new WaitForSeconds(0.125f);

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
            if (entityTypeCollidedWith.OnHitPlayerLoseLife())
                OnHitEnemy();
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
            yield return hitAnimation;
            sr.enabled = true;
            yield return hitAnimation;
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