using DG.Tweening;
using RocketTeam.Sdk.Services.Ads;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unicorn;
using UnityEngine;
using UnityEngine.UI;

public class UiLose : UICanvas
{
    [SerializeField] private TextMeshProUGUI txtRetry;
    [SerializeField] private Button btnRetry;
    [SerializeField] private Button btnSkipLevel;
    [SerializeField] private TextMeshProUGUI txtSkipLevel;
    [SerializeField] private Image imgSkipLevelBg;
    private Tween tween;

    private void Start()
    {
        btnRetry.onClick.AddListener(OnClickBtnRetry);
        btnSkipLevel.onClick.AddListener(OnClickBtnRevive);
    }

    public override void Show(bool _isShown, bool isHideMain = true)
    {
        base.Show(_isShown, isHideMain);

        if (!isShow)
        {
            return;
        }
        if (tween != null)
            tween.Kill();

        btnRetry.interactable = false;
        txtRetry.color = new Color(1, 1, 1, 0);
        txtRetry.DOFade(0.8f, 0.5f).SetDelay(3).OnComplete(() => { btnRetry.interactable = true; });

        bool isRevivable = LevelManager.Instance.IsRevivable();
        btnSkipLevel.gameObject.SetActive(true);
        txtSkipLevel.text = isRevivable ? "Revive" : "Next";

        imgSkipLevelBg.fillAmount = 0;
        tween = imgSkipLevelBg.DOFillAmount(1, Constants.REVIVE_CHOOSING_TIME)
            .SetEase(Ease.Linear)
            .OnComplete(() => { btnSkipLevel.gameObject.SetActive(false); });
    }

    private void OnClickBtnRetry()
    {
        OnBackPressed();

        UnicornAdManager.ShowInterstitial("end_game_lose");
        GameManager.Instance.LoadLevel();

        SoundManager.Instance.PlaySoundButton();
    }

    private void OnClickBtnRevive()
    {
        if (imgSkipLevelBg.fillAmount == 1)
        {
            return;
        }

#if UNITY_EDITOR

        OnReward(1);

#else
        string placement = GameManager.Instance.LevelManager.PlacementRevive;
        if (GameManager.Instance.LevelManager.IsRevivable())
        {
            placement = "video_revive_" + placement;
        }
        else
        {
            placement = "video_next_" + placement;
        }

        RewardAdStatus adStatus = AdManager.Instance.ShowAdsReward((x) =>
        {
            OnReward(1);

        }, placement, true);

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
#endif


        SoundManager.Instance.PlaySoundButton();
    }

    private void OnReward(int x)
    {
        if (x <= 0 && !isShow)
            return;
        StartCoroutine(IEWaitRevive());

    }

    private IEnumerator IEWaitRevive()
    {
        yield return new WaitForSeconds(0.2f);
        OnBackPressed();
        if (LevelManager.Instance.IsRevivable())
        {
            GameManager.Instance.Revive();
        }
        else
        {
            GameManager.Instance.IncreaseLevel();
            GameManager.Instance.LoadLevel();
        }
    }
}
