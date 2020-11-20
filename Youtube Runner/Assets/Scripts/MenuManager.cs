using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI topPanelText;

    [SerializeField] private GameObject shopMenu;
    [SerializeField] private GameObject infoMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject achievementsMenu;
    [SerializeField] private GameObject urlsMenu;

    [ContextMenu("Open Info Menu")]
    public void OpenInfoMenu()
    {
        if (infoMenu.activeInHierarchy)
        {
            CloseAllMenus();
            OpenShopMenu();
            return;
        }

        CloseAllMenus();
        topPanelText.text = "INFO";
        infoMenu.SetActive(true);
    }

    [ContextMenu("Open Options Menu")]
    public void OpenOptionsMenu()
    {
        if (optionsMenu.activeInHierarchy)
        {
            CloseAllMenus();
            OpenShopMenu();
            return;
        }

        CloseAllMenus();
        topPanelText.text = "OPTIONS";
        optionsMenu.SetActive(true);
    }

    [ContextMenu("Open Achievements Menu")]
    public void OpenAchievementsMenu()
    {
        if (achievementsMenu.activeInHierarchy)
        {
            CloseAllMenus();
            OpenShopMenu();
            return;
        }

        CloseAllMenus();
        topPanelText.text = "ACHIEVEMENTS";
        achievementsMenu.SetActive(true);
        MenuInputManager.Instance.ChangeIsShopOpenTo(false);
    }

    [ContextMenu("Open Urls Menu")]
    public void OpenUrlsMenu()
    {
        if (urlsMenu.activeInHierarchy)
        {
            CloseAllMenus();
            OpenShopMenu();
            return;
        }

        CloseAllMenus();
        topPanelText.text = "EXTRA";
        urlsMenu.SetActive(true);
    }

    private void OpenShopMenu()
    {
        MenuInputManager.Instance.ChangeIsShopOpenTo(true);
        shopMenu.SetActive(true);
        topPanelText.text = "SHOP";
    }

    [ContextMenu("Close All Menus")]
    private void CloseAllMenus()
    {
        shopMenu.SetActive(false);
        infoMenu.SetActive(false);
        optionsMenu.SetActive(false);
        achievementsMenu.SetActive(false);
        urlsMenu.SetActive(false);
    }
}