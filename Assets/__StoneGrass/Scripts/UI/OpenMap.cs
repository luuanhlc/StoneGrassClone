using UnityEngine;
using MoreMountains.Tools;

namespace Unicorn
{
    public class OpenMap : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;
            OpenUI(this.gameObject.name.ToString());
        }

        public void OpenUI(string name)
        {
            Debug.Log("clic");
            MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins._btnClickSound, MMSoundManager.MMSoundManagerTracks.UI, Camera.main.gameObject.transform.position);

            switch (name)
            {
                case "Factory":
                    UiController.Ins.OpenUI(1);
                    break;
                case "Map":

                    UiController.Ins.OpenUI(0);
                    break;
                case "UpdateBuilding":
                    if (!PlayerController.Ins._showAble)
                        return;
                    PlayerController.Ins.UpdateEnter(0);
                    UiController.Ins.OpenUI(3);
                    break;
                case "ShopBuilding":
                    UiController.Ins.OpenUI(5);
                    PlayerController.Ins.UpdateEnter(1);
                    break;
                case "Setting":
                    UiController.Ins.OpenUI(6);
                    break;
                case "IsLand":
                    UiController.Ins.OpenUI(7);
                    break;
            }
        }
    }
}
