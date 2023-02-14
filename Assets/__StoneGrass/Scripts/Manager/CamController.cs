using UnityEngine;
using Cinemachine;
using System.Collections;

public class CamController : MonoBehaviour
{
    public CinemachineVirtualCamera crrCam;

    public CinemachineVirtualCamera MainCam;
    public CinemachineVirtualCamera BaseCame;
    public CinemachineVirtualCamera UpdateCam;
    public CinemachineVirtualCamera ShopCam;
    public CinemachineVirtualCamera IsLandCam;
    public CinemachineVirtualCamera HelicopterCam;
    public CinemachineVirtualCamera BuildBrigdeCam;
    public CinemachineVirtualCamera MapForcusCam;
    public GameObject LoobyCam;

    public GameObject Room;

    public CinemachineBrain _brain;

    public static CamController Ins;
    private void Awake()
    {
        cam = Camera.main;
        if (_brain == null)
            _brain = Camera.main.GetComponent<CinemachineBrain>();
        Ins = this;
    }

    #region Zoom On Target OBJ

    public GameObject target;

    public Vector3 offset;
    public float smoothTime = .5f;

    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimited = 50f;

    private Vector3 velocity;
    private Camera cam;

    private void LateUpdate()
    {
        if (target == null)
            return;
        CameraFocus();
    }

    void CameraFocus()
    {
        Vector3 pointOnside = target.transform.position + new Vector3(target.transform.localScale.x * 35f, 50f, target.transform.localScale.z * 35f);
        float aspect = (float)Screen.width / (float)Screen.height;
        float maxDistance = (target.transform.localScale.y * 0.5f) / Mathf.Tan(Mathf.Deg2Rad * (Camera.main.fieldOfView / aspect));
        IsLandCam.transform.position = Vector3.Lerp(pointOnside, target.transform.position, -maxDistance);
        IsLandCam.transform.LookAt(target.transform.position);
    }
}

#endregion