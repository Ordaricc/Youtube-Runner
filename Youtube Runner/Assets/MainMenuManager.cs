using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void OnPressPlay()
    {
        if (PlayerPrefs.GetInt(TutorialManager.prefTutorial) == 0)
        {
            SceneFunctions.ChangeSceneByAddingIndex(2);
        }
        else
        {
            SceneFunctions.ChangeSceneByAddingIndex(1);
        }
    }
}