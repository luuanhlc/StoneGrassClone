using UnityEngine;
using MoreMountains.Feedbacks;
using System.Collections;

public class IsLandPopUp : UICanvas
{
    //public MMFeedbacks FlyToIsLandFeedBack;
    public GameObject joystickCanvas;
    public GameObject isLand;
    public GameObject _player;

    //public float crrVinette;
    public override void Show(bool _isShown, bool isHideMain = true)
    {
        base.Show(_isShown, isHideMain);
        UiTop.Ins.UpdateUi(2);
        if (!isShow)
        {

            return;
        }
    }

    public void BtnIsLandClick()
    {
        LoadScreen.Ins.Changer(1.1f);

        StartCoroutine(wait());

    }

    IEnumerator wait()
    {
        yield return Yielders.Get(.4f);
        _player.SetActive(false);
        CameraController.Ins._isISland = true;
        joystickCanvas.SetActive(false);
        UiTop.Ins.ActiveIslandMode();
        OnBackPressed();
        //FlyToIsLandFeedBack.PlayFeedbacks();
        //MobileFrost _mbf = Camera.main.GetComponent<MobileFrost>();
        //crrVinette = _mbf.Vignette;
        //_mbf.Vignette = 0;
        if (LobbyManager.Ins.weather != null)
            LobbyManager.Ins.weather.Pause();

        if (PlayerDataManager.GetTotalStar() >= 5 && PlayerDataManager.GetTutorialMap() != 1)
        {
            PlayerDataManager.SetTutorialMap(1);
            runTutorial.Ins.run();

        }

        Show(false);
    }


    public void activeMainCam(bool l)
    {
        CamController.Ins.IsLandCam.gameObject.SetActive(!l);
        CamController.Ins.crrCam.gameObject.SetActive(l);
    }
}
