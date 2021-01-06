using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    [SerializeField] private float timeForAnimationToEnd = 3;
    [SerializeField] private GameObject leftText;
    [SerializeField] private GameObject rightText;
    [SerializeField] private GameObject finalTipParent;
    [SerializeField] private GameObject hook;
    [SerializeField] private Button endTutorialButton;

    private bool hasReachedRight;
    private bool hasReachedLeft;

    public const string prefTutorial = "prefTutorial";

    private void Awake()
    {
        Instance = this;

        Invoke("StartTutorial", timeForAnimationToEnd);
    }

    private void StartTutorial()
    {
        rightText.SetActive(true);
        BoatTutorialMovement.Instance.ChangeCanMoveTo(true);
        hook.SetActive(true);
    }

    public void OnBoatReachRightPartOfScreen()
    {
        if (hasReachedRight)
            return;
        hasReachedRight = true;

        rightText.SetActive(false);
        leftText.SetActive(true);

        Vector3 hookPosition = hook.transform.position;
        hookPosition.x *= -1;
        hook.transform.position = hookPosition;
    }

    public void OnBoatReachLeftPartOfScreen()
    {
        if (hasReachedLeft || !hasReachedRight)
            return;
        hasReachedLeft = true;

        BoatTutorialMovement.Instance.gameObject.SetActive(false);
        leftText.SetActive(false);
        hook.SetActive(false);
        finalTipParent.SetActive(true);

        Invoke("EnableEndTutorialButton", 1.5f);
    }

    public void EndTutorial()
    {
        PlayerPrefs.SetInt(prefTutorial, 1);
        SceneFunctions.ChangeSceneByAddingIndex(-1);
    }

    private void EnableEndTutorialButton()
    {
        endTutorialButton.interactable = true;
    }
}