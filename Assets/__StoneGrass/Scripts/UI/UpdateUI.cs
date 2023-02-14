using UnityEngine;

public class UpdateUI : UICanvas
{
    [SerializeField] private GameObject objNotEnoughtGold;

    LobbyManager _lbmg;
    CamController _camCtrl;
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
        on();
        GinderUI.Ins.setButton();
    }

    public override void OnBackPressed()
    {
        _camCtrl.crrCam.gameObject.SetActive(true);
        _camCtrl.UpdateCam.gameObject.SetActive(false);
        off();
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
