using UnityEngine;
using MoreMountains.Tools;
using DG.Tweening;
public class ShopCharacter : UICanvas
{
    [SerializeField] private GameObject objNotEnoughtGold;

    public Animator _ani;
    LobbyManager _lbmg;
    CamController _camCtrl;
    public RectTransform rectTransform;
    public CanvasGroup canvas;
    protected void Start()
    {
        _camCtrl = CamController.Ins;
        _lbmg = LobbyManager.Ins;
        GinderUI.Ins.setButton();
    }

    public override void Show(bool _isShown, bool isHideMain = true)
    {
        base.Show(_isShown, isHideMain);
    }

    public void TurnOn()
    {
        _ani.SetBool("_out", true);
        GinderUI.Ins.setButton();

        canvas.alpha = 0;
        canvas.DOFade(1, UiController.Ins.FadeTime);
        on();
        GinderUI.Ins.setButton();
    }
    public void Close()
    {
        MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins._ClosePopUp, MMSoundManager.MMSoundManagerTracks.UI, Camera.main.gameObject.transform.position);

        _camCtrl.crrCam.gameObject.SetActive(true);
        _camCtrl.UpdateCam.gameObject.SetActive(false);
        canvas.DOFade(0, UiController.Ins.FadeTime);
        rectTransform.DOAnchorPos(new Vector3(0, -1000f, 0), UiController.Ins.OutTime).OnComplete(OnBackPressed);
    }
    public override void OnBackPressed()
    {
        MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins._closeBtnClick, MMSoundManager.MMSoundManagerTracks.UI, Camera.main.gameObject.transform.position);
        off();
        _ani.SetBool("_out", false);
        Show(false);
    }

    public void NotifyNotEnoughGold(Transform trans)
    {
        SetParentNotiNotEnoughGold(trans);
        objNotEnoughtGold.SetActive(true);
    }

    public void DisableNotiNotEnoughtGold()
    {
        objNotEnoughtGold.SetActive(false);
    }

    private void SetParentNotiNotEnoughGold(Transform trans)
    {
        objNotEnoughtGold.transform.SetParent(trans);
        objNotEnoughtGold.transform.localPosition = Vector3.zero;
    }
}
