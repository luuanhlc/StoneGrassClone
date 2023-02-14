using UnityEngine;
using Cinemachine;

public class BuilBridgeView : MonoBehaviour
{
    UiController uiCt;
    GameObject ui;

    GridMap _grm;
    public bool _isBuild;

    public CinemachineVirtualCamera buildCam;

    public static BuilBridgeView Ins;

    private void Start()
    {
        /*ui = GameObject.Find("UiController");
        uiCt = ui.GetComponent<UiController>();
*/
        Ins = this;
        _grm = GridMap.Isn;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || _grm.Complete || PlayerController.Ins._isReturnHome)
            return;
        _isBuild = true;
        CamController.Ins.BuildBrigdeCam = buildCam;
        UiController.Ins.OpenUI(4);
        BuildBridgerPopup.Ins.OnClickBuild();
    }
}

