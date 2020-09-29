using UnityEngine;

public class Wind : MonoBehaviour
{
    public static Wind Instance;
        
    [SerializeField] private RectTransform windFlagImageRect;

    [SerializeField] private float windForceMin = 25;
    [SerializeField] private float windForceMax = 75;
    private float windForce;

    [SerializeField] private float windOnTimerMin = 1;
    [SerializeField] private float windOnTimerMax = 5;
    private float windOnTimer;

    [SerializeField] private float windOffTimerMin = 1;
    [SerializeField] private float windOffTimerMax = 5;
    private float windOffTimer;

    private bool isWindUnlocked;
    private bool isWindOn;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!isWindUnlocked)
            return;

        WindTimer();
    }

    private void WindTimer()
    {
        if (isWindOn)
        {
            windOnTimer -= Time.deltaTime;
            if (windOnTimer <= 0)
            {
                isWindOn = false;
                windOnTimer = Random.Range(windOnTimerMin, windOnTimerMax);
                BoatMovement.Instance.SetWind(0);
                SetFlagUI();
            }
        }
        else
        {
            windOffTimer -= Time.deltaTime;
            if (windOffTimer <= 0)
            {
                isWindOn = true;
                windOffTimer = Random.Range(windOffTimerMin, windOffTimerMax);

                windForce = Random.Range(windForceMin, windForceMax);
                if (Random.Range(1, 3) == 1)
                    windForce *= -1;
                BoatMovement.Instance.SetWind(windForce);
                SetFlagUI();
            }
        }
    }

    private void SetFlagUI()
    {
        windFlagImageRect.gameObject.SetActive(isWindOn);

        if (windForce > 0)
        {
            windFlagImageRect.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            windFlagImageRect.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void ActivateWind()
    {
        isWindUnlocked = true;
        isWindOn = true;
        windForce = Random.Range(windForceMin, windForceMax);
        windOnTimer = Random.Range(windOnTimerMin, windOnTimerMax);
        windOffTimer = Random.Range(windOffTimerMin, windOffTimerMax);
        BoatMovement.Instance.SetWind(windForce);
        SetFlagUI();
    }
}