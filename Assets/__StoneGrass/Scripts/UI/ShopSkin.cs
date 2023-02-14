using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using MoreMountains.Tools;

public class ShopSkin : UICanvas
{
    public Button btnBack;
    public Animator _ani;

    public RectTransform rectTransform;
    public CanvasGroup canvas;

    public DataTruckItem data;
    public DataSkin _dataSkin;

    public static ShopSkin Ins;

    private void Awake()
    {
        Ins = this;
    }


    private void Start()
    {
        btnBack.onClick.AddListener(OnClickBack);
    }

    private void OnClickBack()
    {
        PlayerDataManager.SetDataTruckPreView(-1);
        PlayerDataManager.SetDataPlayerPreView(-1);

        MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins._ClosePopUp, MMSoundManager.MMSoundManagerTracks.UI, Camera.main.gameObject.transform.position);
        canvas.DOFade(0, UiController.Ins.FadeTime);
        CamController.Ins.crrCam.gameObject.SetActive(true);
        CamController.Ins.ShopCam.gameObject.SetActive(false);
        rectTransform.DOAnchorPos(new Vector3(0, -1000f, 0), UiController.Ins.OutTime).OnComplete(OnBackPressed);
        if(data != null)
            ChangerSkin.Ins.Change(data.idItem, data.body, data.bodyScale, data.bodyMaterial, data.whellList[PlayerDataManager.GetPlayerWhell()].wheel, data.whellList[PlayerDataManager.GetPlayerWhell()].mtr);
        if(_dataSkin != null)
        changerPlayer.Ins.ChangerCharacter(_dataSkin.idItem);
    }

    public override void OnBackPressed()
    {
        off();
        _ani.SetBool("_out", false);
        Show(false);
    }

    public override void Show(bool _isShown, bool isHideMain = true)
    {
        base.Show(_isShown, isHideMain);
        if (!_isShown)
        {
            PlayerController.Ins._isInputAble = true;
            return;
        }
        if(TruckSkin.Ins != null)
            TruckSkin.Ins.On();
        if (PlayerSkin.Ins != null)
            PlayerSkin.Ins.On();
        canvas.alpha = 0;
        canvas.DOFade(1, UiController.Ins.FadeTime);
        _ani.SetBool("_out", true);
        PlayerController.Ins._isInputAble = false;
    }
}
