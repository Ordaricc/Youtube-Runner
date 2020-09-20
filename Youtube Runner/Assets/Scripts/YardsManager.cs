using UnityEngine;
using TMPro;

public class YardsManager : MonoBehaviour, IHeadStartReceiver
{
    public static YardsManager Instance;

    public const string prefYards = "prefYards";

    [SerializeField] private TextMeshProUGUI yardsText;
    public float yardsTraveled { get; private set; }
    private bool isTraveling;

    [SerializeField] private float yardsTraveledSpeedMultiplierMultiplier;
    [SerializeField] private float yardsTraveledSpeedMultiplier;
    [SerializeField] private float yardsTraveledSpeedMultiplierMax;
    private bool isSpeedingUp;
    
    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!isTraveling)
            return;

        yardsTraveled += Time.deltaTime * yardsTraveledSpeedMultiplier;
        yardsText.text = (int)yardsTraveled + " YD";

        if (isSpeedingUp)
            SpeedUp();
    }

    public void StartScript()
    {
        isTraveling = true;
        isSpeedingUp = true;
    }

    private void SpeedUp()
    {
        if (Time.timeScale == 0)
            return;

        yardsTraveledSpeedMultiplier *= yardsTraveledSpeedMultiplierMultiplier;

        if (yardsTraveledSpeedMultiplier >= yardsTraveledSpeedMultiplierMax)
        {
            yardsTraveledSpeedMultiplier = yardsTraveledSpeedMultiplierMax;
            isSpeedingUp = false;
        }
    }

    public bool CheckNewHigscore()
    {
        if ((int)yardsTraveled > PlayerPrefs.GetInt(prefYards))
        {
            //new highscore
            if ((int)yardsTraveled >= PagesInShopManager.Instance._unlockExtraPageAtHighscore 
                && PlayerPrefs.GetInt(prefYards) < PagesInShopManager.Instance._unlockExtraPageAtHighscore)
                PagesInShopManager.Instance.UnlockExtraPage();

            PlayerPrefs.SetInt(prefYards, (int)yardsTraveled);
            //Debug.Log("New highscore: " + (int)yardsTraveled);
            return true;
        }
        else
        {
            //no new highscore
            //Debug.Log("No new highscore");
            return false;
        }
    }

    public void ActivateHeadstart(float speedMultiplier)
    {
        isSpeedingUp = false;
        yardsTraveledSpeedMultiplier = yardsTraveledSpeedMultiplierMax * speedMultiplier;
    }

    public void EndHeadstart()
    {
        yardsTraveledSpeedMultiplier = yardsTraveledSpeedMultiplierMax;
    }
}