using UnityEngine;
using MoreMountains.Tools;

public class UiController : MonoBehaviour
{
    public UiMainLobby UiMainLobby;
    public UiLose UiLose;
    public UiWin UiWin;
    public ShopCharacter ShopCharater;
    public UiTop UiTop;
    public UiFactory UiFactory;
    public InGame UiInGame;
    //public UiLeaderBoardIngame UiLeaderboard;
    public PopupRewardEndGame PopupRewardEndGame;
    public PopupChestKey PopupChestKey;
    public LuckyWheel LuckeyWheel;
    public UnlockTrain unlockTrain;
    public GameObject Loading;
    public BuildBridgerPopup buildbridger;
    public UIMap UiMap;
    public ShopSkin UiShopSkin;
    public PopUpReturnHome popReturnHome;
    public IsLandPopUp _IsLandPopUp;
    public UISetting _UISetting;
    public PopupRateGame rateGame;

    public static UiController Ins;

    public float FadeTime;
    public float OutTime;
    public float InTime;

    public Color _colorGridMapPlace;

    private void Awake()
    {
        Ins = this;
    }

    /*public void Init()
    {
    }*/

    public void OpenUI(int i)
    {
        MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins._OpenPopUp, MMSoundManager.MMSoundManagerTracks.UI, Camera.main.gameObject.transform.position);
        switch (i)
        {
            case 0: // map
                UiMap.Show(true);
                break;
            case 1: // factory
                UiFactory.Show(true);
                break;
            case 2: // in game
                UiInGame.Show(true);
                break;
            case 3: // update
                ShopCharater.Show(true);
                ShopCharater.TurnOn();
                    break;
            case 4: // build brigde
                buildbridger.OpenPop();
                buildbridger.Show(true);
                break;
            case 5: // shop skin
                UiShopSkin.Show(true);
                break;
            case 6: // setting

                break;
            case 7: // isLand
                _IsLandPopUp.Show(true);
                break;
        }
    }

    public void OpenUiLose()
    {
        UiLose.Show(true);
    }

    public void OpenUiWin(int gold)
    {
        var UiWin = GameManager.Instance.UiController.UiWin;
        UiWin.Show(true);
        UiWin.Init(gold);
    }

    public void OpenPopupUnlockTrain(XeDay xe)
    {
        unlockTrain.Show(true);
        unlockTrain.takeXe(xe);
    }

    public void CompleteBuild()
    {
        BuildBridgerPopup.Ins.Complete();
    }

    public void ClosePopUpReturnHome()
    {
        popReturnHome.Show(false);
    }

    public void ClosePopupBuildBridger()
    {
        buildbridger.OnClickClose();
    }

    public void ClosePopupUnlockTrain()
    {
        unlockTrain.Show(false);
    }

    public void OpenPopupReward(RewardEndGame reward, TypeDialogReward type)
    {
        if (PopupRewardEndGame.IsShow)
            return;

        PopupRewardEndGame.Show(true);
        PopupRewardEndGame.Init(reward, type);
    }

    public void OpenPopupChestKey(RewardEndGame reward)
    {
        PopupChestKey.Show(true);
        PopupChestKey.Init(reward);
    }

    public void OpenLuckyWheel()
    {
        LuckeyWheel.Show(true);
    }

    public void OpenLoading(bool isLoading)
    {
        Loading.SetActive(isLoading);
    }
}

