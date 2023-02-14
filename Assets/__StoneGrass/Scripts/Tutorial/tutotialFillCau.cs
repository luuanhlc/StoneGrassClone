/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class tutotialFillCau : MonoBehaviour
{
    public static tutotialFillCau Ins;
    private void Awake()
    {
        Ins = this;
    }
    public GameObject canvas;
    private void OnCollisionEnter(Collision collision)
    {
        if (!canvas.active)
            canvas.SetActive(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (canvas.active)
            StartCoroutine(turnOff());
    }

    IEnumerator turnOff()
    {
        yield return Yielders.Get(1f);
        canvas.SetActive(false);
    }
    
}
*/