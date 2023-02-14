using System;
using RocketTeam.Sdk.Services.Ads;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Unicorn;

public class UiWin : UICanvas
{
    [SerializeField] private Button btnCollectGold;
    [SerializeField] private Button btnX3Coin;
    [SerializeField] private Image iconRewardBg;
    [SerializeField] private Image iconReward;
    [SerializeField] private Image imgProcessReward;
    [SerializeField] private TextMeshProUGUI txtProcessReward;
    [SerializeField] private TextMeshProUGUI txtCoin;
    [SerializeField] private TextMeshProUGUI txtCoinBonus;
    [SerializeField] private TextMeshProUGUI txtCoinBonusFree;
    [SerializeField] private TextMeshProUGUI txtCoinReward;
    [SerializeField] private ForceBar forceBar;
    [SerializeField] private DataRewardEndGame dataRewards;
    [SerializeField] private GameObject objFx;
    private int percentReward;
    private int _gold;
    private bool isShowAd;

    public int Gold
    {
        get => _gold;
        protected set => _gold = value;
    }

    private void Start()
    {
        btnCollectGold.onClick.AddListener(OnClickBtnNoThank);
        btnX3Coin.onClick.AddListener(OnClickBtnX3Coin);
    }

    public virtual void Init(int gold)
    {
        objFx.SetActive(false);

        CalculateGold(gold);
        txtCoin.text = $"COLLECT {Gold}";
        btnX3Coin.gameObject.SetActive(true);
        btnCollectGold.interactable = false;
        // SetupRewardEndGame();
        int lvlShowRate = RocketRemoteConfig.GetIntConfig("config_show_rate_game", 4);
        if (PlayerPrefs.GetInt("showRate", 0) == 0 && GameManager.Instance.DataLevel.DisplayLevel >= lvlShowRate)
        {
            StartCoroutine(IEShowRateGame());
            PlayerPrefs.SetInt("showRate", 1);
        }
        StartCoroutine(IEWaitShowFx());

        isShowAd = !(GameManager.Instance.DataLevel.DisplayLevel <= 2);
        if (!isShowAd)
        {
            btnX3Coin.transform.GetChild(0).gameObject.SetActive(false);
            btnX3Coin.transform.GetChild(1).gameObject.SetActive(true);
            btnCollectGold.gameObject.SetActive(false);
        }
        else
        {
            btnX3Coin.transform.GetChild(0).gameObject.SetActive(true);
            btnX3Coin.transform.GetChild(1).gameObject.SetActive(false);
            btnCollectGold.gameObject.SetActive(true);
        }
    }

    protected virtual void CalculateGold(int gold)
    {
        Gold = gold;
    }

    private void Update()
    {
        var value = forceBar.GetValue();
        txtCoinBonus.text = $"+{Gold * value}";
        txtCoinBonusFree.text = $"+{Gold * value}";
    }

    private void SetupRewardEndGame()
    {
        txtCoinReward.gameObject.SetActive(false);
        var playerData = GameManager.Instance.PlayerDataManager;
        int indexReward = PlayerDataManager.GetCurrentIndexRewardEndGame();
        if (indexReward >= dataRewards.Datas.Count)
        {
            indexReward = 0;
            PlayerDataManager.SetCurrentIndexRewardEndGame(indexReward);
        }

        var reward = dataRewards.Datas[indexReward];
        /*if (PlayerDataManager.GetUnlockSkin(reward.Type, reward.Id))
        {
            iconReward.sprite = playerData.DataTexture.IconCoin;
            txtCoinReward.text = string.Format("+{0}", reward.NumberCoinReplace);
            txtCoinReward.gameObject.SetActive(true);
        }
        else
        {
            switch (reward.Type)
            {
                case TypeEquipment.Hat:
                    {
                        iconReward.sprite = playerData.DataTexture.GetIconHat(reward.Id);
                    }

                    break;
                case TypeEquipment.Skin:
                    {
                        iconReward.sprite = playerData.DataTexture.GetIconSkin(reward.Id);
                    }
                    break;
                case TypeEquipment.Pet:
                    {
                        iconReward.sprite = playerData.DataTexture.GetIconPet(reward.Id);
                    }
                    break;
                case TypeEquipment.Mask:
                    {
                        iconReward.sprite = playerData.DataTexture.GetIconSkill(reward.Id);
                    }
                    break;
            }

        }*/

        iconRewardBg.sprite = iconReward.sprite;
        iconRewardBg.SetNativeSize();
        iconReward.SetNativeSize();

        int process = PlayerDataManager.GetProcessReceiveRewardEndGame();
        process++;
        PlayerDataManager.SetProcessReceiveRewardEndGame(process);
        if (process >= reward.NumberWin)
        {
            PlayerDataManager.SetProcessReceiveRewardEndGame(0);

            StartCoroutine(IEWaitShowRewardEndGame(reward));

        }
        else
        {
            // check show rate game

        }

        float ratio = (float)process / (float)reward.NumberWin;
        imgProcessReward.fillAmount = 0;
        imgProcessReward.DOFillAmount(ratio, 1f);
        percentReward = (int)(ratio * 100);

        iconReward.fillAmount = 0;
        iconReward.DOFillAmount(ratio, 1f);

        SetPercentReward(percentReward);
    }

    private Tweener tweenCoin;
    private int tmpPercent;
    private void SetPercentReward(int percent)
    {
        tweenCoin = tweenCoin ?? DOTween.To(() => tmpPercent, x =>
        {
            tmpPercent = x;
            txtProcessReward.text = string.Format("{0}%", tmpPercent);
        }, percent, 1f).SetAutoKill(false).OnComplete(() =>
         {
             tmpPercent = percentReward;
             txtProcessReward.text = string.Format("{0}%", tmpPercent);
         });

        tweenCoin.ChangeStartValue(tmpPercent);
        tweenCoin.ChangeEndValue(percent);
        tweenCoin.Play();
    }

    private void OnClickBtnNoThank()
    {

        SoundManager.Instance.PlayFxSound(SoundManager.GameSound.RewardClick);

        btnCollectGold.interactable = false;

        int lvl = RocketRemoteConfig.GetIntConfig("config_lvl_show_video_end_game", 10);
        bool isShowVideo = GameManager.Instance.DataLevel.DisplayLevel % lvl == 0 ? true : false;
        if (isShowVideo)
        {
            RewardAdStatus adStatus = AdManager.Instance.ShowAdsReward((x) =>
            {

            }, Helper.video_reward_x3_gold_end_game, true);

            OnRewardVideo(1);
        }
        else
        {
            GameManager.Instance.Profile.AddGold(Gold);
            StartCoroutine(IEGoLobby(true));
        }
           

        

    }

    private void OnClickBtnX3Coin()
    {
        if (!isShowAd)
        {
            OnRewardVideo(1);
            return;
        }
        UnicornAdManager.ShowAdsReward(OnRewardVideo, Helper.video_reward_x3_gold_end_game);
    }

    private void OnRewardVideo(int x)
    {
        if (x <= 0 && !isShow)
            return;

        btnX3Coin.gameObject.SetActive(false);
        btnCollectGold.interactable = false;
        forceBar.StopRunning();
        GameManager.Instance.Profile.AddGold(Gold * forceBar.GetValue());

        //txtCoin.text = string.Format("+{0}", _gold * 3);
        SoundManager.Instance.PlaySoundReward();

        StartCoroutine(IEGoLobby(false));
    }

    private IEnumerator IEWaitShowRewardEndGame(RewardEndGame reward)
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.UiController.OpenPopupReward(reward, TypeDialogReward.END_GAME);
    }

    private IEnumerator IEWaitShowFx()
    {
        yield return new WaitForSeconds(0.5f);
        objFx.SetActive(true);
    }

    private IEnumerator IEShowRateGame()
    {
        yield return new WaitForSeconds(0.5f);
        PopupRateGame.Instance.Show();
    }

    private IEnumerator IEGoLobby(bool isShowInter)
    {
        yield return new WaitForSeconds(0.5f);
        OnBackPressed();
        if (isShowInter)
            UnicornAdManager.ShowInterstitial("end_game_win");
        GameManager.Instance.LoadLevel();
    }
}
