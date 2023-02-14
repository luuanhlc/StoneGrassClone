using UnityEngine;
using DG.Tweening;
using MoonAntonio.UI;
using System.Collections;
using UnityEngine.UI;
using Exoa.TutorialEngine;

public class IsLandBuildTut : MonoBehaviour
    {
    [Header("Tutorial Icon")]
    public static IsLandBuildTut Ins;
    public RectTransform PressAndHold;
    public RectTransform Swipe;
    public RectTransform Rotate;
    public RectTransform Zoom;

    [Header("Target and panel")]

    public RectTransform Parent;

    public GameObject tarGetBuildBtn;

    public GameObject targetDrag;

    private GameObject hand;

    public bool _isTutRuning;

    [Header("Time")]
    public float swipeTime;
    public float zoomTime;
    public float rotateTime;
    public float buildTime;
    public float dragTime;
    public float choseTime;

    [Header("Mask")]
    public GameObject Panel;

    public CanvasGroup Outline;
    public RectTransform Mask;

    [Header("animator")]
    public Animator _aniTouchPress;

    [Header("UI var")]
    public Canvas can;
    public Camera cam;
    RectTransform CanvasRect;


    private void Awake()
    {
        Ins = this;
    }

    private void Start()
    {
        tarGetBuildBtn = GameObject.FindGameObjectWithTag("Target");
        if (can == null)
            can = GameObject.FindGameObjectWithTag("UICanvas").GetComponent<Canvas>();
        cam = Camera.main;
        CanvasRect = can.GetComponent<RectTransform>();
    }
    bool _isRan;
    /*private void FixedUpdate()
    {
        windMill = GameObject.FindGameObjectWithTag("WindMill");
        if(windMill != null && !_isRan)
        {
            run();
            _isRan = true;
        }
    }*/

    public void run()
    {
        StartCoroutine(_runCountDown(.6f));
    }

    IEnumerator _runCountDown(float time)
    {
        yield return Yielders.Get(time);
        TutorialLoader.instance.Load("PressHold");
        TutorialEvents.OnTutorialComplete += _TutorialIsOver;
        Destroy(this.gameObject);
    }
    public void _TutorialIsOver()
    {
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.S))
        {
            SwipeTut();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _Zoom();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            _Rotate();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            BuyAndDrag();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DragTut();
        }
#endif
        if (_isTutRuning)
        {
            StartCoroutine(TurnOfTut());
        }
    }
    float time;
    IEnumerator TurnOfTut()
    {
        yield return Yielders.Get(time);
        Panel.gameObject.SetActive(false);
    }
    
    
    public void SwipeTut()
    {
        time = swipeTime;
        _isTutRuning = true;
        Swipe.gameObject.SetActive(true);
    }

    public void _Zoom()
    {
        time = zoomTime;
        _isTutRuning = true;
        Zoom.gameObject.SetActive(true);
    }

    public void _Rotate()
    {
        time = rotateTime;
        _isTutRuning = true;
        Rotate.gameObject.SetActive(true);
    }
    public void BuyAndDrag()
    {
        time = buildTime;
        _isTutRuning = true;
        Mask.gameObject.SetActive(true);
        Mask.transform.position = tarGetBuildBtn.transform.position;
        PressAndHold.gameObject.SetActive(true);
        PressAndHold.transform.position = tarGetBuildBtn.transform.position;
        _aniTouchPress.SetTrigger("touch");

        //PressAndHold.transform.DOMove(this.transform.position, .75f, false).OnComplete(SetAnim);
    }
    int i = 0;
    /*void SetAnim()
    {
        if (i >= 1)
        {
            PressAndHold.gameObject.SetActive(false);
            Mask.gameObject.SetActive(false);
            Mask.anchoredPosition = new Vector3(157f, -10000.5f, 0f);
            return;
        }
        i++;
        _aniTouchPress.SetTrigger("touch");
        PressAndHold.transform.position = tarGetBuildBtn.transform.position;
        PressAndHold.transform.DOMove(this.transform.position, .75f, false).OnComplete(SetAnim);
    }*/
    Interactivo _menu;
    int dragTutLoadTime = 0;
    GameObject windMill;
    public void DragTut()
    {
        if(windMill == null)
            windMill = GameObject.FindGameObjectWithTag("WindMill");

        if (dragTutLoadTime >= 2)
        {
            windMill.transform.DOKill();
            windMill.transform.DOScale(.6f, 1f).SetAutoKill();
  
            return;
        }
        dragTutLoadTime++;
        time = buildTime;
        _isTutRuning = true;
        PressAndHold.gameObject.SetActive(true);
        Vector2 ViewportPosition1 = cam.WorldToViewportPoint(windMill.transform.position);
        Vector2 WorldObject_ScreenPosition1 = new Vector2(
        ((ViewportPosition1.x * CanvasRect.sizeDelta.x) * cam.rect.width - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition1.y * CanvasRect.sizeDelta.y) * cam.rect.height - (CanvasRect.sizeDelta.y * 0.5f))
        );
        windMill.transform.DOScale(.65f, .6f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        PressAndHold.anchoredPosition = WorldObject_ScreenPosition1;
        _menu = windMill.GetComponent<Interactivo>();
        _menu._tutPos = PressAndHold.gameObject.transform.position;

        StartCoroutine(eventWait());
        //PressAndHold.transform.DOMove(targetDrag.transform.position, 1f, false).SetDelay(1f).OnComplete(SetAnim);
    }
    IEnumerator eventWait()
    {
        yield return Yielders.Get(.5f);
        _menu.EventTut();
        //Destroy(this.gameObject);
    }

}
