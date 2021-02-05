using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    public static AdManager Instance;

    private const string androidGameID = "3820227";
    private const string appleGameID = "3820226";
    private const string skippableVideo = "video";
    private const string rewardedVideo = "rewardedVideo";

    private IAdManagerListener callback;
    private WaitForSecondsRealtime adDelay = new WaitForSecondsRealtime(0.25f);

    private void Awake()
    {
        Instance = this;

        if (Advertisement.isSupported)
        {
            if (Application.platform == RuntimePlatform.Android)
                Advertisement.Initialize(androidGameID);

            if (Application.platform == RuntimePlatform.IPhonePlayer)
                Advertisement.Initialize(appleGameID);

            if (Application.isEditor)
                Advertisement.Initialize(appleGameID);

            Advertisement.AddListener(this);
        }
    }

    public void CallAdCoroutine(bool isAdRewarded, IAdManagerListener _callback)
    {
        callback = _callback;
        StartCoroutine(WatchAd(isAdRewarded));
    }

    private IEnumerator WatchAd(bool isAdRewarded)
    {
        string videoID = isAdRewarded ? rewardedVideo : skippableVideo;
        
        int i = 0;
        while (i < 10)
        {
            i++;
            if (Advertisement.IsReady(videoID))
            {
                Advertisement.Show(videoID);
                yield break;
            }
            yield return adDelay;
        }
    }

    public void OnUnityAdsReady(string placementId)
    {

    }

    public void OnUnityAdsDidError(string message)
    {

    }

    public void OnUnityAdsDidStart(string placementId)
    {

    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (callback != null)
        {
            callback.GetAdResult(showResult);
            callback = null;
        }
    }
}