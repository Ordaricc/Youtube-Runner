using UnityEngine;
using UnityEngine.UI;

public class PagesInShopManager : MonoBehaviour
{
    public static PagesInShopManager Instance;

    [SerializeField] private Button turnLeftPageButton;
    [SerializeField] private Button turnRightPageButton;

    [SerializeField] private GameObject[] pagesInShop;
    private int indexOfPagesInShopWithCurrentlyOpenPage;

    private const string prefExtraPagesUnlocked = "prefExtraPagesUnlocked";

    [SerializeField] private int unlockExtraPageAtHighscore = 1000;
    public int _unlockExtraPageAtHighscore { get { return unlockExtraPageAtHighscore; } }

    private void Awake()
    {
        Instance = this;

        UpdateButtonsInteractability();
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
        }
    }

    public void UnlockExtraPage()
    {
        PlayerPrefs.SetInt(prefExtraPagesUnlocked, PlayerPrefs.GetInt(prefExtraPagesUnlocked) + 1);
        if (PlayerPrefs.GetInt(prefExtraPagesUnlocked) >= pagesInShop.Length)
            Debug.LogWarning("Unlocked more pages than available");

        UpdateButtonsInteractability();
    }

    private void UpdateButtonsInteractability()
    {
        turnLeftPageButton.interactable = indexOfPagesInShopWithCurrentlyOpenPage > 0;
        turnRightPageButton.interactable = pagesInShop.Length - 1 > indexOfPagesInShopWithCurrentlyOpenPage;

        turnRightPageButton.interactable = PlayerPrefs.GetInt(prefExtraPagesUnlocked) >= indexOfPagesInShopWithCurrentlyOpenPage + 1;
    }
}