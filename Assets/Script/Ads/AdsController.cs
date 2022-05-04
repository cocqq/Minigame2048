using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdsController : MonoBehaviour
{
    [SerializeField] Button restartGameOverButton, continueWinningButton, continuePauseButton;
    private void Start() 
    {
        AdManager.instance.RequestInterstitial();
        //restartGameOverButton.onClick.AddListener(RunAds);
        continueWinningButton.onClick.AddListener(RunAds);
        continuePauseButton.onClick.AddListener(RunAds);
    }
    public void RunAds()
    {
        AdManager.instance.Show_InterstitialAd();
    }
}
