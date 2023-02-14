using UnityEngine;
public class isLandPark : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        UiController.Ins.OpenUI(0);
        PlayerController.Ins._isInputAble = false;
        PlayerController.Ins.HumanMode(false);
    }

    private void OnTriggerExit(Collider other)
    {

        if (!other.CompareTag("Player"))
            return;
        //UiController.Ins._IsLandPopUp.Show(false);
        PlayerController.Ins.HumanMode(true);
    }
}
