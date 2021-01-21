using UnityEngine;

public class AnimationPrefabs : MonoBehaviour
{
    public static AnimationPrefabs Instance;

    [SerializeField] private GameObject animationPrefab;

    [SerializeField] private Sprite bootySprite;
    [SerializeField] private Sprite heartSprite;
    [SerializeField] private Sprite headstartSprite;
    [SerializeField] private Sprite lanternSprite;
    [SerializeField] private Sprite netSprite;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnAnimation(string spriteName)
    {
        Sprite spriteChosen = null;
        switch (spriteName)
        {
            case "booty":
                spriteChosen = bootySprite;
                break;

            case "heart":
                spriteChosen = heartSprite;
                break;

            case "headstart":
                spriteChosen = headstartSprite;
                break;

            case "lantern":
                spriteChosen = lanternSprite;
                break;

            case "net":
                spriteChosen = netSprite;
                break;

            default:
                break;
        }

        GameObject animationSpawned = Instantiate(animationPrefab, BoatMovement.Instance.transform.position, Quaternion.identity);
        animationSpawned.GetComponentInChildren<SpriteRenderer>().sprite = spriteChosen;
        Destroy(animationSpawned, 2);
    }
}