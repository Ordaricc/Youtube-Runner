using UnityEngine;

public class MySceneManager : MonoBehaviour
{
    public const string prefHasChosenLanguage = "prefHasChosenLanguage";

    private void Awake()
    {
        if (PlayerPrefs.GetInt(prefHasChosenLanguage) == 0)
        {
            SceneFunctions.ChangeSceneByAddingIndex(1);
        }
        else
        {
            SceneFunctions.ChangeSceneByAddingIndex(2);
        }
    }
}