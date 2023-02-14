using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using MoreMountains.Tools;

public class BuildBridgerPopup : UICanvas
{
    #region Declare
    [SerializeField] private Button btnBuild;
    [SerializeField] private Button btnClose;
    [SerializeField] private Button btnReset;

    [SerializeField] private GameObject _panel;
    [SerializeField] private Canvas _joystick;

    LobbyManager _lbmg;
    CamController _camCtrl;
    GridMap _grm;
    public Animator _ani;

    public bool hasRunTutorial;

    #endregion
    #region singleton
    public static BuildBridgerPopup Ins;

    private void Awake()
    {
        Ins = this;
    }
    #endregion
    private void Start()
    {
        _grm = GridMap.Isn;
        _camCtrl = CamController.Ins;

        //buildCam = LobbyManager.Ins.BuildBridgerCam;

        btnBuild.onClick.AddListener(OnClickBuild);
        btnClose.onClick.AddListener(OnClickClose);
        btnReset.onClick.AddListener(OnClicReset);
    }


    #region OnClickBtn
    public void OnClickBuild()
    {
        //Camera.main.orthographic = true;
        CamController.Ins.crrCam.gameObject.SetActive(false);
        CamController.Ins.BuildBrigdeCam.gameObject.SetActive(true);

        _panel.SetActive(false);
        _joystick.enabled = false;
        btnClose.gameObject.SetActive(true);
        btnReset.gameObject.SetActive(true);
        Shape._isBuilding = true;
        _ani.SetBool("_out", true);
        if (BuildBridgerTut.Ins!= null && !hasRunTutorial)
        {
            //TutorialDemoScript.Ins.KhoiTao();
            StartCoroutine(waitForRunTut());
            hasRunTutorial = true;
        }
    }

    IEnumerator waitForRunTut()
    {
        yield return Yielders.Get(1.4f);
        BuildBridgerTut.Ins.step1();

    }
    public void OnClickClose()
    {
        CamController.Ins.crrCam.gameObject.SetActive(true);
        CamController.Ins.BuildBrigdeCam.gameObject.SetActive(false);
        BuilBridgeView.Ins._isBuild = false;
        btnClose.gameObject.SetActive(false);
        btnReset.gameObject.SetActive(false);
        _joystick.enabled = true;
        OnBackPressed();
        outBuild();
        Shape._isBuilding = false;
        if (BuildBridgerTut.Ins != null)
        {
            BuildBridgerTut.Ins.step = 0;
            BuildBridgerTut.Ins.nextStep();
        }
    }

    public void OnClicReset()
    {
        GridMap.Isn.resetMiniGame();

        if (BuildBridgerTut.Ins != null)
        {
            BuildBridgerTut.Ins.step = 0;
            BuildBridgerTut.Ins.nextStep();
            BuildBridgerTut.Ins.step1();
        }
    }
    #endregion
    public void outBuild()
    {
        _ani.SetBool("_out", false);
    }

    public void Complete()
    {
        Camera.main.orthographic = false;
        CamController.Ins.crrCam.gameObject.SetActive(true);
        CamController.Ins.BuildBrigdeCam.gameObject.SetActive(false);
        _joystick.enabled = true;
        off();
        Show(false);
    }

    public override void OnBackPressed()
    {
        GridMap.Isn.resetMiniGame();
        off();
        Show(false);
    }

    public void OpenPop()
    {
        _panel.SetActive(true);
        on();
    }

    public override void Show(bool _isShown, bool isHideMain = true)
    {
        base.Show(_isShown, isHideMain);
        if (!isShow)
            return;
    }
}
