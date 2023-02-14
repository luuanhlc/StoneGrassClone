using System.Collections;
using UnityEngine;

namespace Unicorn
{
    public class SellFar : MonoBehaviour
    {
        public XeDay xe;
        vanOfTrain _trainVan;

        UiController uiCtl;
        GameObject ui;

        PlayerController _player;

        private bool can =  true;
        private void Start()
        {
            _player = PlayerController.Ins;
            xe = XeDay.Isn;
            _trainVan = vanOfTrain.Isn;

            ui = GameObject.Find("UiController");
            uiCtl = ui.GetComponent<UiController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (xe._isActive)
                return;
            uiCtl.OpenPopupUnlockTrain(xe);
        }

        private void OnTriggerStay(Collider other)
        {
            if (!xe._isActive || !other.CompareTag("Player"))
                return;

        }
        private void OnTriggerExit(Collider other)
        {
            uiCtl.ClosePopupUnlockTrain();
            if (!xe._isActive || !other.CompareTag("Player"))
                return;
            if (_trainVan.takedN == 0 || !can)
                return;
            xe.go = true;
            can = false;
            StartCoroutine(time());
        }
        
        IEnumerator time()
        {
            yield return new WaitForSeconds(1f);
            can = true;
        }
    }
}
