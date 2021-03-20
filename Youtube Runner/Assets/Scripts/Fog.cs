using UnityEngine;

public class Fog : MonoBehaviour
{
    public static Fog Instance;

    [SerializeField] private GameObject fogParent;
    [SerializeField] private Animator fog2Anim;
    [SerializeField] private float timer;
    [SerializeField] private float timerMax = 10;
    [SerializeField] private float timerVariance = 5;

    public bool isFogOn { get; private set; }

    private void Awake()
    {
        Instance = this;

        timer = timerMax;
    }

    private void Update()
    {
        if (!fogParent.activeInHierarchy)
            return;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = timerMax + Random.Range(-timerVariance, timerVariance);
            fog2Anim.SetTrigger("SwitchFog");
        }
    }

    public void ActivateFog(bool isAnimationOnly)
    {
        isFogOn = true;
        fogParent.SetActive(true);

        if (isAnimationOnly)
            return;

        Lantern.Instance.ActivateLantern();

        AchievementsManager.Instance.UnlockAchievement(Achievement.AchievementTypes.seeFog);
    }
}