using UnityEngine;
using Cinemachine;
using System.Collections;
using UnityEngine.Events;
public class CameraController : MonoBehaviour
{
    public Camera Camera;
    public bool Rotate;
    protected Plane Plane;
    public CinemachineVirtualCamera cam;

    public static CameraController Ins;

    private void Awake()
    {
        Ins = this;
        if (Camera == null)
            Camera = Camera.main;
    }

    public bool _isISland;

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.N))
        {
            IsLandBuildTut.Ins._isTutRuning = false;
            IsLandBuildTut.Ins.Swipe.gameObject.SetActive(false);
            StartCoroutine(WaitForRunAction(0, .3f));
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            IsLandBuildTut.Ins.Zoom.gameObject.SetActive(false);
            IsLandBuildTut.Ins._isTutRuning = false;
            StartCoroutine(WaitForRunAction(1, .3f));
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(WaitForRunAction(2, 1f));
        }

#endif
        if (!_isISland)
            return;

        if(BuildingSystem.Ins != null)
            if (BuildingSystem.Ins._isDrag)
                return;

        //Update Plane
        if (Input.touchCount >= 1)
            Plane.SetNormalAndPosition(transform.up, transform.position);

        var Delta1 = Vector3.zero;
        var Delta2 = Vector3.zero;

        //Scroll
        if (Input.touchCount >= 1)
        {
            if (IsLandBuildTut.Ins != null && IsLandBuildTut.Ins.Swipe.gameObject.active)
            {
                IsLandBuildTut.Ins._isTutRuning = false;
                IsLandBuildTut.Ins.Swipe.gameObject.SetActive(false);
                StartCoroutine(WaitForRunAction(0, .3f));
            }
            Delta1 = PlanePositionDelta(Input.GetTouch(0));
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                cam.gameObject.transform.Translate(Delta1, Space.World);
            }
        }
        #region
        #endregion
        //Pinch
        if (Input.touchCount >= 2)
        {
            var pos1 = PlanePosition(Input.GetTouch(0).position);
            var pos2 = PlanePosition(Input.GetTouch(1).position);
            var pos1b = PlanePosition(Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition);
            var pos2b = PlanePosition(Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);

            //calc zoom
            var zoom = Vector3.Distance(pos1, pos2) /
                       Vector3.Distance(pos1b, pos2b);

            //edge case
            if (zoom == 0 || zoom > 10)
                return;

            //Move cam amount the mid ray
            if(cam.gameObject.transform.position.y >= 3f || zoom < 1)
            {
                if (IsLandBuildTut.Ins != null && IsLandBuildTut.Ins.Zoom.gameObject.active)
                {
                    IsLandBuildTut.Ins.Zoom.gameObject.SetActive(false);
                    IsLandBuildTut.Ins._isTutRuning = false;
                    StartCoroutine(WaitForRunAction(1, .3f));

                }
                cam.gameObject.transform.position = Vector3.LerpUnclamped(pos1, Camera.transform.position, 1 / zoom);
            }

            if (Rotate && pos2b != pos2)
            {
                cam.gameObject.transform.RotateAround(pos1, Plane.normal, Vector3.SignedAngle(pos2 - pos1, pos2b - pos1b, Plane.normal));
                if (IsLandBuildTut.Ins != null && IsLandBuildTut.Ins.Rotate.gameObject.active)
                {
                    
                    StartCoroutine(WaitForRunAction(2, 1f));
                }
            }
        }
    }
    public BuildBridgerTut _IsLandTut;
    IEnumerator WaitForRunAction(int i, float time)
    {
        yield return Yielders.Get(time);
        switch (i)
        {
            case 0:
                IsLandBuildTut.Ins._Zoom();
                break;
            case 1:
                IsLandBuildTut.Ins._Rotate();
                break;
            case 2:
                IsLandBuildTut.Ins._isTutRuning = false;
                IsLandBuildTut.Ins.Rotate.gameObject.SetActive(false);

                /*_IsLandTut.gameObject.SetActive(true);
                _IsLandTut.step1();
                _IsLandTut.step = Mathf.Min(_IsLandTut._tutorial.Count, _IsLandTut.step + 1);*/
                break;
        }
    }
    
    protected Vector3 PlanePositionDelta(Touch touch)
    {
        //not moved
        if (touch.phase != TouchPhase.Moved)
            return Vector3.zero;

        //delta
        var rayBefore = Camera.ScreenPointToRay(touch.position - touch.deltaPosition);
        var rayNow = Camera.ScreenPointToRay(touch.position);
        if (Plane.Raycast(rayBefore, out var enterBefore) && Plane.Raycast(rayNow, out var enterNow))
            return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);

        //not on plane
        return Vector3.zero;
    }

    protected Vector3 PlanePosition(Vector2 screenPos)
    {
        //position
        var rayNow = Camera.ScreenPointToRay(screenPos);
        if (Plane.Raycast(rayNow, out var enterNow))
            return rayNow.GetPoint(enterNow);

        return Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.up);
    }
}
