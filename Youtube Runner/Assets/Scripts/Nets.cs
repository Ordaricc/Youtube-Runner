using UnityEngine;
using TMPro;

public class Nets : MonoBehaviour
{
    public static Nets Instance;

    [SerializeField] private GameObject netImageGO;
    [SerializeField] private TextMeshProUGUI netsAvailableText;

    [SerializeField] private int netsAvailable;
    public int netsLevel { get; private set; }
    
    public bool canAddNet { get { return netsAvailable < netsLevel; } }

    public int defeatedEnemies { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void StartScript()
    {
        netsLevel = PlayerPrefs.GetInt(ShopItem.ShopItems.nets.ToString());
        netsAvailable = netsLevel;
        UpdateNetsUI();
    }

    private void UpdateNetsUI()
    {
        netImageGO.SetActive(netsAvailable > 0);

        string newTextForNetsAvaialableText = "";

        switch (netsAvailable)
        {
            case 0:
                newTextForNetsAvaialableText = "";
                break;

            case 1:
                newTextForNetsAvaialableText = "I";
                break;

            case 2:
                newTextForNetsAvaialableText = "II";
                break;

            case 3:
                newTextForNetsAvaialableText = "III";
                break;

            default:
                Debug.LogWarning("Roman number conversion not set up!");
                newTextForNetsAvaialableText = "> 3";
                break;
        }

        netsAvailableText.text = newTextForNetsAvaialableText;
    }

    public bool TryRemoveNet()
    {
        if (netsAvailable > 0)
        {
            defeatedEnemies++;
            if (defeatedEnemies == 5)
                AchievementsManager.Instance.UnlockAchievement(Achievement.AchievemntTypes.defeatEnemiesInSingleRun);

            netsAvailable--;
            UpdateNetsUI();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddNet()
    {
        netsAvailable++;
        UpdateNetsUI();
    }
}