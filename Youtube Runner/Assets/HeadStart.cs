using UnityEngine;

public class HeadStart : MonoBehaviour
{
    public static HeadStart Instance;

    [SerializeField] private float headStartDuration = 1;
    [SerializeField] private float speedMultiplier = 2;
    [SerializeField] private MoveTowards[] backgrounds;

    private void Awake()
    {
        Instance = this;
    }

    public void StartScript()
    {
        if (PlayerPrefs.GetInt(ShopItem.ShopItems.headstart.ToString()) >= 1)
        {
            //turn on
            YardsManager.Instance.ActivateHeadstart(speedMultiplier);
            SpawnManager.Instance.ActivateHeadstart(speedMultiplier);
            BoatMovement.Instance.ActivateHeadstart(speedMultiplier);
            BoatCollision.Instance.ActivateHeadstart(speedMultiplier);
            foreach (MoveTowards b in backgrounds)
            {
                b.ActivateHeadstart(speedMultiplier);
            }

            Invoke("TurnOffHeadstart", headStartDuration);
        }
    }

    private void TurnOffHeadstart()
    {
        //turn off
        YardsManager.Instance.EndHeadstart();
        SpawnManager.Instance.EndHeadstart();
        BoatMovement.Instance.EndHeadstart();
        BoatCollision.Instance.EndHeadstart();
        foreach (MoveTowards b in backgrounds)
        {
            b.EndHeadstart();
        }
    }
}

public interface IHeadStartReceiver
{
    void ActivateHeadstart(float speedMultiplier);

    void EndHeadstart();
}