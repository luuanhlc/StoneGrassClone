using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public Camera cam;
    public Canvas cv;

    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        Debug.Log(cam.name);
        cv.worldCamera = cam;
    }
}
