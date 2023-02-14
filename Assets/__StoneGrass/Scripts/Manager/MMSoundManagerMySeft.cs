using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
public class MMSoundManagerMySeft : MonoBehaviour
{
    public MMSoundManagerSettingsSO _setting;
    public MMSoundManager _soundManager;
    
    [Header("UI")]
    public AudioClip _closeBtnClick;
    public AudioClip _btnClickSound;
    public AudioClip _tabClickSound;
    public AudioClip _OpenPopUp;
    public AudioClip _ClosePopUp;

    [Header("MiniGame")]
    public AudioClip _completeSnapBrick;
    public AudioClip _snapFailBrick;

    [Header("MainGame")]
    public AudioClip _background;
    public AudioClip _lobbyBg;

    public AudioClip _sellSound;

    public AudioSource lobbySource;
    public AudioSource bgSource;

    public AudioClip moneySound;
    public AudioClip takeSound;

    public AudioClip powerUp;

    public AudioClip completeSound;

    public static MMSoundManagerMySeft Ins;

    private void Awake()
    {
        Ins = this;
        //MMSoundManagerSoundPlayEvent.Trigger(_closeBtnClick, MMSoundManager.MMSoundManagerTracks.UI, Camera.main.gameObject.transform.position);
    }
}
