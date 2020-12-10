using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsUIManager : MonoBehaviour
{
    public static AchievementsUIManager Instance;

    [SerializeField] private TextMeshProUGUI trophyNameText;
    [SerializeField] private TextMeshProUGUI trophyDescriptionText;

    [SerializeField] private Button turnLeftPageButton;
    [SerializeField] private Button turnRightPageButton;

    [SerializeField] private GameObject[] pagesInShop;
    private int indexOfPagesInShopWithCurrentlyOpenPage;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateTrophyTextsUI(string trophyName, string trophyDescription)
    {
        trophyNameText.text = trophyName;
        trophyDescriptionText.text = trophyDescription;
    }
        
    public void TurnPage(int pageIndexAddend)
    {
        int pageIndexToGoTo = indexOfPagesInShopWithCurrentlyOpenPage + pageIndexAddend;
        if (pageIndexToGoTo >= 0 && pageIndexToGoTo < pagesInShop.Length)
        {
            pagesInShop[indexOfPagesInShopWithCurrentlyOpenPage].SetActive(false);
            pagesInShop[pageIndexToGoTo].SetActive(true);
            indexOfPagesInShopWithCurrentlyOpenPage = pageIndexToGoTo;

            UpdateButtonsInteractability();

            trophyNameText.text = "";
            trophyDescriptionText.text = "Page " + (pageIndexToGoTo + 1);
        }
    }

    public void UpdateButtonsInteractability()
    {
        turnLeftPageButton.interactable = indexOfPagesInShopWithCurrentlyOpenPage > 0;
        turnRightPageButton.interactable = pagesInShop.Length - 1 > indexOfPagesInShopWithCurrentlyOpenPage;
    }
}