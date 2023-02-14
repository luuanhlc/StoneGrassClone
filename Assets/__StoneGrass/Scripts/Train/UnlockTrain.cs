using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockTrain : UICanvas
{
    [SerializeField] private Button btnPayGem;
    [SerializeField] private Button btnWatchAd;
    [SerializeField] private Button btnNoThanks;

    public XeDay xe;

    infor _infor;
    private void Start()
    {
        _infor = infor.Ins;
        btnPayGem.onClick.AddListener(OnClickBtnPayGem);
        btnNoThanks.onClick.AddListener(OnClickBtnNoThanks);
        btnWatchAd.onClick.AddListener(OnClickBtnWatchAd);
    }

    public void takeXe(XeDay d)
    {
        xe = d;
    }

    private void OnClickBtnPayGem()
    {

        xe._isActive = true;
        GameManager.Instance.Profile.AddDimond(-5);
        UiTop.Ins.UpdateUi(1);
        OnBackPressed();
    }

    private void OnClickBtnNoThanks()
    {
        OnBackPressed();
    }

    private void OnClickBtnWatchAd()
    {
        xe._isActive = true;
        OnBackPressed();
    }
    public override void OnBackPressed()
    {
        off();
        Show(false);
    }

    public void nOn()
    {
        on();
    }

    public override void Show(bool _isShown, bool isHideMain = true)
    {
        base.Show(_isShown, isHideMain);
        if (!isShow)
        {
            return;
        }
    }
}

