using System.Collections;
using UnityEngine;
using TMPro;

public class Lantern : MonoBehaviour
{
    public static Lantern Instance;

    [SerializeField] private GameObject fogSpriteGO;

    [SerializeField] private GameObject lanternUIPanel;
    [SerializeField] private TextMeshProUGUI lanternFuelText;

    public int lanternLevel { get; private set; }
    private int lanternFuel;
    public bool CanAddLanternFuel { get { return lanternLevel > 0 && Fog.Instance.isFogOn; } }

    private WaitForSeconds lanternTick = new WaitForSeconds(1);

    private void Awake()
    {
        Instance = this;
    }

    public void StartScript()
    {
        lanternLevel = PlayerPrefs.GetInt(ShopItem.ShopItems.lantern.ToString());
        lanternFuel = lanternLevel * 10;
    }

    private IEnumerator TickLantern()
    {
        while (lanternFuel > 0)
        {
            lanternFuelText.text = lanternFuel.ToString();
            yield return lanternTick;
            lanternFuel--;

            if (lanternFuel == 0)
            {
                lanternUIPanel.SetActive(false);
                fogSpriteGO.SetActive(true);
            }
        }
    }

    public void ActivateLantern()
    {
        if (lanternLevel > 0)
        {
            StartCoroutine(TickLantern());
            fogSpriteGO.SetActive(false);
            lanternUIPanel.SetActive(true);
            lanternFuelText.text = lanternFuel.ToString();
        }
    }

    public void AddExtraFuel()
    {
        if (lanternFuel == 0)
        {
            lanternFuel += lanternLevel * 10;
            fogSpriteGO.SetActive(false);
            lanternUIPanel.SetActive(true);
            StartCoroutine(TickLantern());
        }
        else
        {
            lanternFuel += lanternLevel * 10;
        }
    }
}