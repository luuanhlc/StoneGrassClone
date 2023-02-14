using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using TMPro;


namespace Unicorn
{
    [RequireComponent(typeof(Image))]
    public class ButtonTab : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        MMFeedbacks clickFeedBack;
        public TabGroup tabgroup;
        
        public Image type;
        public TextMeshProUGUI text;

        private void Start()
        {
            clickFeedBack = gameObject.GetComponent<MMFeedbacks>();
            type = GetComponent<Image>();
            tabgroup.Subscribe(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            clickFeedBack.PlayFeedbacks();
            MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins._tabClickSound, MMSoundManager.MMSoundManagerTracks.UI, Camera.main.gameObject.transform.position);
            tabgroup.OnTabSelect(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            tabgroup.OnTabEnter(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tabgroup.OnTabExit(this);
        }
    }
}
