using System.Collections;
using System.Collections.Generic;
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

    private List<SpriteRenderer> animationsInMagazine = new List<SpriteRenderer>();
    private readonly WaitForSeconds animationDelay = new WaitForSeconds(2);

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < 3; i++)
        {
            InstantiateNewAnimation();
        }
    }

    private void InstantiateNewAnimation()
    {
        GameObject animationSpawned = Instantiate(animationPrefab);
        animationSpawned.SetActive(false);
        animationsInMagazine.Add(animationSpawned.GetComponentInChildren<SpriteRenderer>());
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
                Debug.LogError("Error! Sprite " + spriteName + " not found!");
                break;
        }

        if (animationsInMagazine.Count == 0)
            InstantiateNewAnimation();

        SpriteRenderer animationInMagazineSR = animationsInMagazine[0];
        GameObject animationInMagazineGO = animationInMagazineSR.gameObject.transform.parent.gameObject;
        animationInMagazineGO.SetActive(true);
        animationInMagazineGO.transform.position = BoatMovement.Instance.transform.position;
        animationInMagazineSR.sprite = spriteChosen;

        animationsInMagazine.Remove(animationInMagazineSR);
        StartCoroutine(PutAnimationBackIntoMagazine(animationInMagazineSR, animationInMagazineGO));
    }

    private IEnumerator PutAnimationBackIntoMagazine(SpriteRenderer animationSR, GameObject parentGO)
    {
        yield return animationDelay;
        animationsInMagazine.Add(animationSR);
        parentGO.SetActive(false);
    }
}