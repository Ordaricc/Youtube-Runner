using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFunctions : MonoBehaviour
{
    public static void ChangeSceneByAddingIndex(int buildIndexToAdd)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + buildIndexToAdd);
    }
}