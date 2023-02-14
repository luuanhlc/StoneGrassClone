using System;
using RocketTeam.Sdk.Services.Ads;
using UnityEngine;

namespace Unicorn
{
    public static class UnicornAdManager
    {
            public static void LoadBannerAds()
            {
                if (PlayerDataManager.IsNoAds())
                    return;
        
                AdManager.Instance.ShowBanner();
            }
        
            public static void ShowInterstitial(string _placement)
            {
                if (PlayerDataManager.IsNoAds())
                    return;
        
                //int lvlDisplayAds = RocketRemoteConfig.GetIntConfig("config_from_lvl_display_ads", 0);
        
                //if (DataLevel.DisplayLevel > lvlDisplayAds)
                AdManager.Instance.ShowInterstitial(_placement, (int)AdEnums.ShowType.INTERSTITIAL);
            }
            
            public static RewardAdStatus ShowAdsReward(Action<int> onRewarded, string placement, bool isAutoLog = true)
            {
/*#if UNITY_EDITOR
                onRewarded?.Invoke(1);
                if (isAutoLog)
                    Debug.Log("Free reward in development!");
                return RewardAdStatus.ShowVideoReward;
#endif*/
            
#pragma warning disable CS0162
                // ReSharper disable HeuristicUnreachableCode
                var adStatus = AdManager.Instance.ShowAdsReward(onRewarded, placement, isAutoLog);
                switch (adStatus)
                {
                    case RewardAdStatus.NoInternet:
                        PopupDialogCanvas.Instance.Show("No Internet!");
                        break;
                    case RewardAdStatus.ShowVideoReward:
                        break;
                    case RewardAdStatus.ShowInterstitialReward:
                        break;
                    case RewardAdStatus.NoVideoNoInterstitialReward:
                        PopupDialogCanvas.Instance.Show("No Video!");
                        break;
                    default:
                        break;
                }

                return adStatus;
                // ReSharper restore HeuristicUnreachableCode
#pragma warning restore CS0162
            }
    }
}