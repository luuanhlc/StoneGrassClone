using UnityEngine;
using System.Collections;
public class Rail : MonoBehaviour
{
    private bool isShow;
    private void OnTriggerEnter(Collider other)
    {
        if (PlayerController.Ins._isReturnHome)
            return;
        if (other.CompareTag("Player") && !isShow)
        {
            isShow = true;
            UiController.Ins.OpenUI(6);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (PlayerController.Ins._isReturnHome)
            return;
        if (other.CompareTag("Player"))
        {
            isShow = false;
            StartCoroutine(setOff(1f));
        }    
    }

    IEnumerator setOff(float time)
    {
        yield return Yielders.Get(time);
        UiController.Ins.ClosePopUpReturnHome();
    }
}
