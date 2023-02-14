using UnityEngine;
using DG.Tweening;
using System.Collections;
public class Room : MonoBehaviour
{
    public RectTransform returnHomeBtn;
    Vector2 inside = new Vector2(44.5f, -260.3f);
    Vector2 outsided = new Vector2(300f, -260.3f);
    bool on;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            returnHomeBtn.DOAnchorPos(outsided, 1, false).SetEase(Ease.InBack);
            if (!Shape._isBuilding)
            {
                CamController.Ins.BaseCame.gameObject.SetActive(true);
                CamController.Ins.MainCam.gameObject.SetActive(false);
            }
            CamController.Ins.crrCam = CamController.Ins.BaseCame;
            if (PlayerDataManager.GetCrrLevel() == 1 && !on && PlayerDataManager.GetStar("StatsLevel1") >= 1)
            {
                Debug.Log("VDL");
                on = true;
                CamController.Ins.crrCam.gameObject.SetActive(false);
                CamController.Ins.MapForcusCam.gameObject.SetActive(true);
                PlayerController.Ins._isInputAble = false;
                StartCoroutine(turnOffMapForcusCam());
                return;
            }
        }
    }

    IEnumerator turnOffMapForcusCam()
    {
        yield return Yielders.Get(2f);
        Debug.Log("vl");
        PlayerController.Ins._isInputAble = true;

        CamController.Ins.MapForcusCam.gameObject.SetActive(false);
        CamController.Ins.crrCam.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            returnHomeBtn.DOAnchorPos(inside, 1, false).SetEase(Ease.OutBack);

            if (!Shape._isBuilding)
            {
                CamController.Ins.BaseCame.gameObject.SetActive(false);
                CamController.Ins.MainCam.gameObject.SetActive(true);
            }
            CamController.Ins.crrCam = CamController.Ins.MainCam;
        }
    }
}
