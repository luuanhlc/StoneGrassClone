using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using DG.Tweening;
using MoreMountains.Tools;

public class Shape : MonoBehaviour
{
    #region Declare
    BoxCollider collider;
    private MaterialPropertyBlock _prop;

    public static bool _isBuilding;

    [SerializeField] public List<GameObject> _tileBlock;
    public bool _Done = false;

    private bool _isDragged = false;
    public bool _can = true;

    public MMFeedbacks cantFeedBack;
    public MMFeedbacks DoneFeedBack;

    private MMFeedbacks DestroyFeedBacks;

    public Vector3 _offsetPos;
    [SerializeField] private Vector3 _originPos;

    MaterialPropertyBlock _propBlock;
    public Pos[] _pos;
    public Vector2 sizeOfShape;
    #endregion

    #region singleton

    public static Shape Isn;

    private void Awake()
    {
        _originPos = this.gameObject.transform.position;
        Isn = this;
        DestroyFeedBacks = this.gameObject.GetComponent<MMFeedbacks>();
    }
    #endregion

    #region Start

    private void Start()
    {
        _pos = new Pos[_tileBlock.Count];
        canPlaceShape = new Vector2(GridMap.Isn.Rows - sizeOfShape.x, GridMap.Isn.Cols - sizeOfShape.y);
        _propBlock = new MaterialPropertyBlock();
        _propBlock.SetColor("_Color", Color.red);
        _originPos = transform.position;
        _offsetPos = transform.position;
        collider = GetComponent<BoxCollider>();
    }

    #endregion

    #region OnMouseState
    public Vector2 canPlaceShape;
    private void OnMouseDown()
    {
        if (BuildBridgerTut.Ins != null && this.gameObject != BuildBridgerTut.Ins._tutorial[BuildBridgerTut.Ins.step].pos1)
            return;
        if (_Done || !BuilBridgeView.Ins._isBuild)
            return;

        this.transform.DOKill();
        GridMap.Isn.swaping = true;
        _isDragged = true;
    }
    Vector3 worldPos;
    Vector3 pos;
    private void OnMouseDrag()
    {
        if (!_isDragged)
            return;

        pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(this.gameObject.transform.position).z);
        worldPos = Camera.main.ScreenToWorldPoint(pos);
        transform.position = new Vector3(worldPos.x, .5f, worldPos.z);

        if(Time.frameCount % 5 == 0)
        {
            if(_can)
                GridMap.Isn.ChangerColor(_pos);
            _tileBlock[0].GetComponent<Snap>().Find(canPlaceShape);
        }
    }
    Vector3 mouseDown = new Vector3(1.8f, 1.3f, 1.8f);
    Vector3 mouseUp = new Vector3(1.500001f, 1f, 1.500001f);
    private void OnMouseUp()
    {
        

            if (!_isDragged)
            return;
        _isDragged = false;
        
        
        if (!_can)
        {
            if(PlayerDataManager.GetVibraviton())
                cantFeedBack.PlayFeedbacks();
            MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins._snapFailBrick, MMSoundManager.MMSoundManagerTracks.Sfx, Camera.main.gameObject.transform.position);
            transform.position = _originPos;
            for(int i = 0; i < _tileBlock.Count; i++)
            {
                _tileBlock[i].GetComponent<Snap>()._reset();
            }
            GridMap.Isn.resetCOlor();
            if (BuildBridgerTut.Ins != null && this.gameObject == BuildBridgerTut.Ins._tutorial[BuildBridgerTut.Ins.step].pos1)
            {
                this.transform.DOPunchPosition(new Vector3(.1f, .1f, .1f), .5f).SetLoops(-1, LoopType.Yoyo);
            }
            return;
        }
        if(BuildBridgerTut.Ins != null) 
        {
            if (this.gameObject == BuildBridgerTut.Ins._tutorial[BuildBridgerTut.Ins.step].pos1)
            {
                BuildBridgerTut.Ins.hand.DOKill();
                BuildBridgerTut.Ins.hand.GetComponent<CanvasGroup>().DOFade(0, .2f);
                if (BuildBridgerTut.Ins.step != BuildBridgerTut.Ins._tutorial.Count - 1)
                {
                    BuildBridgerTut.Ins.step = Mathf.Min(BuildBridgerTut.Ins._tutorial.Count, BuildBridgerTut.Ins.step + 1);
                    BuildBridgerTut.Ins.step1();
                }
                else
                {
                    BuildBridgerTut.Ins.nextStep();
                    BuildBridgerTut.Ins.Popup.gameObject.SetActive(false);
                }
            }
        }
        Vector3 s = _tileBlock[0].GetComponent<Snap>().mt;
        transform.position = new Vector3(s.x, GridMap.Isn._frispos.position.y, s.z);
        for (int i = 0; i < _tileBlock.Count; i++)
            _tileBlock[i].GetComponent<Snap>().SnaptoGrid2();
        _Done = true;
        MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins._completeSnapBrick, MMSoundManager.MMSoundManagerTracks.Sfx, Camera.main.gameObject.transform.position);
        if(PlayerDataManager.GetVibraviton())
            DoneFeedBack.PlayFeedbacks();
        collider.enabled = false;
        //To do: Check grid map
        GridMap.Isn.CheckMap();
        StartCoroutine(offSwaping());
    }

    #endregion
    public Vector2 center;
    public void CheckLessTile(int i )
    {
        if (i < _tileBlock.Count)
            _tileBlock[i].GetComponent<Snap>().notCenterCheck(center, i);
        else
            _can = true;
    }
    IEnumerator offSwaping()
    {
        yield return Yielders.Get(.1f);
        GridMap.Isn.swaping = false;
    }
    public void _resetOffset()
    {
        transform.position = _offsetPos;
        _Done = false;
        collider.enabled = true;
        for(int i  = 0; i < _tileBlock.Count; i++)
        {
            _tileBlock[i].GetComponent<Snap>()._reset();
        }
    }

    public void DestroyCau()
    {
        this.gameObject.transform.DOShakePosition(.5f, .5f, 10, 2, true);
       
        StartCoroutine(turnDown(.2f));

    }
    public float time;
    IEnumerator turnDown(float time)
    {
        yield return Yielders.Get(time);
        this.gameObject.transform.DOShakePosition(1f, .5f, 10, 1, true);
        yield return Yielders.Get(.1f);
        this.gameObject.transform.DOMoveY(-20f, 1, false).OnComplete(destroy);
    }

    public void destroy()
    {
        Destroy(this.gameObject);
    }
}

public class Pos
{
    public int i, j;
}