using UnityEngine;

public class URLsManager : MonoBehaviour
{
    public void OpenPlayStore()
    {
        Application.OpenURL("market://details?id=" + Application.identifier);
    }

    public void OpenYoutubeChannel()
    {
        Application.OpenURL("https://www.youtube.com/channel/UC-hohI8Y8F3vydCdy84PnPg/featured");
    }

    public void OpenGameWiki()
    {
        Application.OpenURL("https://boatventure.fandom.com/it/wiki/BoatVenture_Wiki");

    }
}