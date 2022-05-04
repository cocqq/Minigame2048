using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class StartAd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Load an app open ad when the scene starts
        AppOpenAdManager.Instance.LoadAd();
    }
    public void OnApplicationPause(bool paused)
    {
        // Display the app open ad when the app is foregrounded
        if (!paused)
        {
            AppOpenAdManager.Instance.ShowAdIfAvailable();
        }
    }
}
