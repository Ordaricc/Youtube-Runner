using UnityEngine;
using UnityEngine.UI;

public class MenuInputManager : MonoBehaviour
{
    public static MenuInputManager Instance;

    [SerializeField] private Button rightButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private float marginForSwipe = 25;

    private bool isShopOpen = true;

    private Vector3 startingFingerPosition;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                startingFingerPosition = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended)
            {
                if (Input.touches[0].position.x > startingFingerPosition.x + marginForSwipe)
                {
                    //swipe right
                    if (leftButton.interactable)
                        OnMenuButtonsPress(-1);
                }
                else if (Input.touches[0].position.x < startingFingerPosition.x - marginForSwipe)
                {
                    //swipe left
                    if (rightButton.interactable)
                        OnMenuButtonsPress(1);
                }
            }
        }
    }

    public void OnMenuButtonsPress(int direction)
    {
        if (isShopOpen)
        {
            PagesInShopManager.Instance.TurnPage(direction);
        }
        else
        {
            AchievementsUIManager.Instance.TurnPage(direction);
        }
    }

    public void ChangeIsShopOpenTo(bool value)
    {
        isShopOpen = value;

        if (isShopOpen)
        {
            PagesInShopManager.Instance.UpdateButtonsInteractability();
        }
        else
        {
            AchievementsUIManager.Instance.UpdateButtonsInteractability();
        }
    }
}