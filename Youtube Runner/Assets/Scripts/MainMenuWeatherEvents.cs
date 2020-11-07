using UnityEngine;

public class MainMenuWeatherEvents : MonoBehaviour
{
    private void Awake()
    {
        Invoke("RandomlyCallWeatherEvents", 0.0001f);
    }

    private void RandomlyCallWeatherEvents()
    {
        if (Random.Range(1, 3) == 1) //&& PlayerPrefs.GetInt(Achievement.AchievemntTypes.seeFog.ToString()) == 1)
        {
            Fog.Instance.ActivateFog(true);
        }

        if (Random.Range(1, 3) == 1) //&& PlayerPrefs.GetInt(Achievement.AchievemntTypes.dummy2.ToString()) == 1)
        {
            Thunderstorm.Instance.ActivateThunderstorm();
        }
    }
}