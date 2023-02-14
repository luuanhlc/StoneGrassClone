using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using DG.Tweening;


public class BuildBridgerTut : MonoBehaviour
{
    [Header("Popup var")]
    public RectTransform Popup;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] public RectTransform hand;
    [SerializeField] private Button _btn;

    [Header("UI var")]
    public Canvas can;
    public Camera cam;

    RectTransform CanvasRect;

    [Header(" Tut steps")]
    public List<tutorialStep> _tutorial;   
        
    public int step = 0;

    public float Speed = 1f;

    public static BuildBridgerTut Ins;

    void Start()
    {
        if(can == null)
            can = GameObject.FindGameObjectWithTag("UICanvas").GetComponent<Canvas>();
        cam = Camera.main;
        CanvasRect = can.GetComponent<RectTransform>();
    }

    private void Awake()
    {
        Ins = this;
        if (_tutorial.Count != 0)
            _btn.onClick.AddListener(addEvent);
    }

    private void addEvent()
    {
        if(_tutorial[step]._event != null)
            _tutorial[step]._event.Invoke();

        step += Mathf.Min(_tutorial.Count - 1, step++);
    }
    RectTransform _target2;
    public void step1()
    {
        hand.GetComponent<CanvasGroup>().alpha = 0;
        Popup.gameObject.SetActive(true);
        hand.gameObject.SetActive(true);
        hand.GetComponent<CanvasGroup>().DOFade(1, .2f);
        if (_tutorial[step].pos1 == null)
            return;
        RectTransform _target1 = _tutorial[step].pos1.GetComponent<RectTransform>();
        if (_tutorial[step].pos1.CompareTag("Block"))
        {
            _tutorial[step].pos1.transform
                .DOPunchPosition(new Vector3(.1f, .1f, .1f), .5f).SetLoops(-1, LoopType.Yoyo);

        }
        if (_tutorial[step].pos2 != null)
        {
            _target2 = _tutorial[step].pos2.GetComponent<RectTransform>();
        }
        if (_target1 == null)
        {
            Vector2 ViewportPosition1 = cam.WorldToViewportPoint(_tutorial[step].pos1.transform.position);
            Vector2 WorldObject_ScreenPosition1 = new Vector2(
            ((ViewportPosition1.x * CanvasRect.sizeDelta.x) * cam.rect.width - (CanvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition1.y * CanvasRect.sizeDelta.y) * cam.rect.height - (CanvasRect.sizeDelta.y * 0.5f))
            );

            if (_tutorial[step].pos2 == null)
            {
                hand.anchoredPosition = WorldObject_ScreenPosition1;
                return;
            }
            
            Vector2 ViewportPosition2 = cam.WorldToViewportPoint(_tutorial[step].pos2.transform.position);

            Vector2 WorldObject_ScreenPosition2 = new Vector2(
            ((ViewportPosition2.x * CanvasRect.sizeDelta.x) * cam.rect.width - (CanvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition2.y * CanvasRect.sizeDelta.y) * cam.rect.height - (CanvasRect.sizeDelta.y * 0.5f))
            );
            hand.anchoredPosition = WorldObject_ScreenPosition1;
            hand.DOAnchorPos(WorldObject_ScreenPosition2, Speed, false).SetLoops(-1, LoopType.Restart);
            Debug.Log(WorldObject_ScreenPosition1);
            Debug.Log(WorldObject_ScreenPosition2);
        }
        else
        {
            if(_tutorial[step].pos2 == null)
            {
                Debug.Log("Vl");
                hand.anchoredPosition = _target1.anchoredPosition;
                return;
            }
            hand.anchoredPosition = _target1.anchoredPosition;
            hand.DOAnchorPos(_target2.anchoredPosition, Speed, false).SetLoops(-1, LoopType.Restart);
        }
    }

    public void PressAndHoldStep()
    {
        IsLandBuildTut.Ins.DragTut();
    }

    public void nextStep()
    {
        hand.DOKill();
        hand.gameObject.SetActive(false);
    }

    private void RunTut()
    {
        /*Popup.gameObject.SetActive(true);
        text.SetText(_tutorial[step].Lable);

        RectTransform _target = _tutorial[step].targetOj.GetComponent<RectTransform>();

        if(_target == null)
        {
            Vector2 ViewportPosition = cam.WorldToViewportPoint(_tutorial[step].targetOj.transform.position);
            Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * CanvasRect.sizeDelta.x) * cam.rect.width - (CanvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * CanvasRect.sizeDelta.y) * cam.rect.height - (CanvasRect.sizeDelta.y * 0.5f))
            );

            _btn.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;
            hand.anchoredPosition = WorldObject_ScreenPosition;
            text.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;
        }
        else
        {
            _btn.GetComponent<RectTransform>().anchoredPosition = _target.anchoredPosition;
            hand.anchoredPosition = _target.anchoredPosition;
            text.GetComponent<RectTransform>().anchoredPosition = _target.anchoredPosition;
        }*/
    }

}
[System.Serializable]
public class tutorialStep
{
    public string nameStep;
    public string Lable;
    //public GameObject targetOj;
    public UnityEvent _event;
    public GameObject pos1, pos2;
}