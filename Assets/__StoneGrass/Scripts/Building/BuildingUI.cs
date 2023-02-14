using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUI : MonoBehaviour
{
    public GameObject originCam;
    public GameObject buidingCam;

    private void Update()
    {
        if (this.gameObject.active)
        {
            originCam.SetActive(false);
            buidingCam.SetActive(true);
        }
    }

    public void CloseClick()
    {
        this.gameObject.SetActive(false);
        originCam.SetActive(true);
        buidingCam.SetActive(false);
    }
}
