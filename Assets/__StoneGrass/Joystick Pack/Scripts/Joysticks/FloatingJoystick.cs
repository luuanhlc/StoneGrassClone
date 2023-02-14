using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class FloatingJoystick : Joystick
{
    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(false);
    }
    bool isOff;
    public override void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("ld");

        if (PlayerDataManager.GetCrrLevel() == 1 && !isOff)
        {
            isOff = true;
            GameObject tut = GameObject.FindGameObjectWithTag("TutHand");
            tut.GetComponent<CanvasGroup>().DOFade(0, .3f).OnComplete(() => Destroy(tut));
            Destroy(tut);
        }
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }
}