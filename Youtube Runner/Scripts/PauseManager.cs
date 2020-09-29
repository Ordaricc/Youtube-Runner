using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseTextGO;
    [SerializeField] private Image pauseButtonImage;

    [SerializeField] private Sprite pauseButtonSprite;
    [SerializeField] private Sprite playButtonSprite;

    private bool isGamePaused;
    private bool canPause = true;
    private float timeBeforePause;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            TogglePause();
    }

    public void TogglePause()
    {
        if (!canPause)
            return;

        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            pauseTextGO.SetActive(true);
            pauseButtonImage.sprite = playButtonSprite;

            timeBeforePause = Time.timeScale;
            Time.timeScale = 0;
        }
        else
        {
            pauseTextGO.SetActive(false);
            pauseButtonImage.sprite = pauseButtonSprite;

            Time.timeScale = timeBeforePause;
        }
    }

    public void ChangePauseTo(bool changeItTo)
    {
        canPause = changeItTo;
    }
}