using UnityEngine;

public class StartGameManager : MonoBehaviour
{
    [SerializeField] private MoveTowards[] movingBackgrounds;

    public void StartGame()
    {
        SpawnManager.Instance.StartScript();
        YardsManager.Instance.StartScript();
        GetMobManager.Instance.StartScript();
        BoatMovement.Instance.StartScript();
        BoatCollision.Instance.StartScript();
        PlayerBonus.Instance.StartScript();
        Nets.Instance.StartScript();
        Lantern.Instance.StartScript();
        HeadStart.Instance.StartScript();

        foreach (MoveTowards m in movingBackgrounds)
        {
            m.StartScript();
        }

        MenuInputManager.Instance.enabled = false;
        enabled = false;
    }
}