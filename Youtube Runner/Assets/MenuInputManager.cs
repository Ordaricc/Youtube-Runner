using UnityEngine;

public class MenuInputManager : MonoBehaviour
{
    public static MenuInputManager Instance;

    private bool isShopOpen = true;

    private void Awake()
    {
        Instance = this;
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