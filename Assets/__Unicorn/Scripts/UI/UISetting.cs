using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using MoreMountains.Tools;
using DG.Tweening;
public class UISetting : UICanvas
{
    public CanvasGroup canvas;
    public RectTransform rectTransform;

    public static UISetting Ins;

    bool _soundToggle;
    bool _musicToggle;
    bool _vibrationToggle;

    protected override void Awake()
    {
        Ins = this;

        _soundToggle = PlayerDataManager.GetSoundSetting();
        _musicToggle = PlayerDataManager.GetMusicSetting();
        _vibrationToggle = PlayerDataManager.GetVibraviton();
    }


    private void Rate()
    {
        Application.OpenURL(RocketConfig.OPEN_LINK_RATE);
    }

    private void RestorePurchase()
    {
        bool isOn = !PlayerDataManager.GetVibraviton();
        PlayerDataManager.SetVibration(isOn);
        MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins._btnClickSound, MMSoundManager.MMSoundManagerTracks.UI, Camera.main.gameObject.transform.position);
    }

    



    public void ToggleSound()
    {
        _soundToggle = !_soundToggle;
        MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins._btnClickSound, MMSoundManager.MMSoundManagerTracks.UI, Camera.main.gameObject.transform.position);
        PlayerDataManager.SetSoundSetting(_soundToggle);

        if (_soundToggle)
        {
            MMSoundManagerMySeft.Ins._soundManager.UnmuteMaster();
            MMSoundManagerMySeft.Ins._soundManager.SetVolumeMaster(1.01f);
        }
        else
            MMSoundManagerMySeft.Ins._soundManager.MuteMaster();

    }

    public void ToggleMusic()
    {

        _musicToggle = !_musicToggle;

        PlayerDataManager.SetMusicSetting(_musicToggle);
        MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins._btnClickSound, MMSoundManager.MMSoundManagerTracks.UI, Camera.main.gameObject.transform.position);
        if (_musicToggle)
        {
            MMSoundManagerMySeft.Ins._soundManager.UnmuteMusic();
            MMSoundManagerMySeft.Ins._soundManager.SetVolumeMusic(1.01f);
        }
        else
            MMSoundManagerMySeft.Ins._soundManager.MuteMusic();
    }

    public void ToggleVibration()
    {
        _vibrationToggle = !_vibrationToggle;
        MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins._btnClickSound, MMSoundManager.MMSoundManagerTracks.UI, Camera.main.gameObject.transform.position);
        PlayerDataManager.SetVibration(_vibrationToggle);
    }

    public void Open()
    {
        UiTop.Ins._ani.SetBool("_out", true);
        canvas.alpha = 0;
        canvas.DOFade(1, UiController.Ins.FadeTime).SetUpdate(true);
        gameObject.SetActive(true);
        GameManager.Instance.Pause();
    }

    public void _Close(bool _isBtnClick)
    {
        if (_isBtnClick)
        {
            SettingsMenu.Ins.ToggleMenu();
        }
        canvas.DOFade(0, UiController.Ins.FadeTime - 0.2f).OnComplete(Off).SetUpdate(true);
        MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins._closeBtnClick, MMSoundManager.MMSoundManagerTracks.UI, Camera.main.gameObject.transform.position);
    }
    public void Off()
    {
        gameObject.SetActive(false);
        GameManager.Instance.Resume();
        UiTop.Ins._ani.SetBool("_out", false);
    }
}
