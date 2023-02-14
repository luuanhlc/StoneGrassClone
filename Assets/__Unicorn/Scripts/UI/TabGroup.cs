using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace Unicorn
{
    public class TabGroup : MonoBehaviour
    {
        public List<ButtonTab> buttonTab;
        
        public Color idleColor;
        public Color enterColor;
        public Color clickColor;

        public Sprite onImg;
        public Sprite offImg;

        public Color onText;
        public Color offText;

        [HideInInspector]public ButtonTab selectedTab;
        public List<GameObject> objectToSawp;

        private void Start()
        {
            OnTabSelect(buttonTab[0]);
        }

        public void Subscribe(ButtonTab button)
        {
            if(buttonTab == null)
            {
                buttonTab = new List<ButtonTab>();
            }
            buttonTab.Add(button);
        }

        public void OnTabEnter(ButtonTab button)
        {
            ResetTab();
            if (selectedTab != null || button == selectedTab)
            {
                Debug.Log(this.gameObject.name + " On");
                button.type.sprite = onImg;
                button.text.color = onText;
            }
        }

        public void OnTabExit(ButtonTab button)
        {
            ResetTab();
        }

        public void OnTabSelect(ButtonTab button)
        {
            selectedTab = button;
            ResetTab();
            button.type.sprite = onImg;

            int index = button.transform.GetSiblingIndex();
            for(int i = 0; i < objectToSawp.Count; i++)
            {
                if (i == index)
                {
                    objectToSawp[i].SetActive(true);
                    objectToSawp[i].GetComponent<CanvasGroup>().alpha = 0;
                    objectToSawp[i].GetComponent<CanvasGroup>().DOFade(1, .2f);
                }
                else
                    objectToSawp[i].SetActive(false);
            }
        }

        public void ResetTab()
        {
            foreach(ButtonTab button in buttonTab)
            {
                if(selectedTab != null && button == selectedTab) { continue; }
                button.type.sprite = offImg;
                button.text.color = offText;
            }
        }
    }
}
