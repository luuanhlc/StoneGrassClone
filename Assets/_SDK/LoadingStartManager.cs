using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingStartManager : MonoBehaviour
{
    [SerializeField] CanvasGroup group;
    [SerializeField] private Image imgLoading;
    [SerializeField] private float timeLoading = 5;

    private AsyncOperation loadSceneAsync;
    private AppOpenAdManager appOpenAdManager;

    private void Awake()
    {
        appOpenAdManager = AppOpenAdManager.Instance;
    }

    void Start()
    {
        LoadAppOpen();
        DontDestroyOnLoad(gameObject);
        LoadMasterLevel();
        RunLoadingBar();
    }

    private void LoadAppOpen()
    {
#if UNITY_EDITOR
        return;
#endif        
        MobileAds.Initialize(initStatus => { appOpenAdManager.LoadAd();});
    }
    
    private void LoadMasterLevel()
    {
        loadSceneAsync = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }

    private void RunLoadingBar()
    {
        imgLoading.DOFillAmount(0.9f, timeLoading)
            .SetEase(Ease.OutQuint)
            .OnComplete(() => { StartCoroutine(Fade()); });
    }
    
    private IEnumerator Fade()
    {
        yield return new WaitUntil(() => loadSceneAsync.isDone);
        imgLoading.DOFillAmount(1f, 0.1f);
        group.DOFade(0, 0.2f)
            .OnComplete(() => { Destroy(group.gameObject); });
    }
    
    public void OnApplicationPause(bool paused)
    {
        // Display the app open ad when the app is foregrounded
        if (!paused)
        {
            if (appOpenAdManager.IsShowAds  && !PlayerDataManager.IsNoAds())
            {
                appOpenAdManager.ShowAdIfAvailable();
            }

            appOpenAdManager.IsShowAds = true;
        }
    }
}