using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
public class PlayerController : MonoBehaviour
{
    #region Declare
    public bool _isReturnHome;

    public MMFeedbacks haptics;
    public Joystick joystick;
    Rigidbody _rb;
    Animator _ani;

    GameObject ui;
    Texture test;
    public Texture2D tes;
    public Material _mainMaterial;

    CamController _camCtrl;
    [HideInInspector] public freeRotate _sawCtrl;
    public infor _infor;

    public bool _isInputAble = true;

    [Header("Items")]
    public static bool _namcham = false;
    public static bool _infinityDame = false;
    public static bool _infinityStronge = false;
    public static bool _bigerSaw = false;

    public static bool _isMove;
    public static bool _isShop;
    public Transform grassPlace;

    [SerializeField] private float avalibleItemsTime = 25f;

    [Header("UI")]
    public bool _showAble = true;

    public Transform parkUpdate;
    public Transform parkShop;

    [Header("VFX")]
    public ParticleSystem sawFX;
    public ParticleSystem truckUpdateFX;

    [Header("TouchGrass")]
    public Transform touchPos;

    public GameObject truckBody;
    public GameObject takePos;
    public GameObject van;

    [Header("Grass Touch")]
    [SerializeField] private Material material;
    [SerializeField] [Range(0, 10)] private float radius;
    [SerializeField] [Range(-1, 5)] private float heightOffset;
    private readonly int grassTrampleProperty = Shader.PropertyToID("_Trample");

    public List<HumanController> _humanController;
    
    // check xay cau
    // mo rong vung dat cau
    // bug xay nha
    // han che mau do

    public List<GameObject> iconmoney = new List<GameObject>();
    public GameObject perfab;
    public int size;
    public GameObject UIingame;

    #endregion

    #region Singleton
    public static PlayerController Ins;

    private void Awake()
    {
        Ins = this;
        for(int i = 0; i < size; i++)
        {
            GameObject ob = Instantiate(perfab, UIingame.transform);
            ob.SetActive(false);
            iconmoney.Add(ob);
        }
    }
    #endregion

    RenderTexture renderTexture;

    #region Defaul
    void Start()
    {
        _sawCtrl = freeRotate.Ins;
        _infor = infor.Ins;
        _camCtrl = CamController.Ins;
        _isInputAble = true;

        _ani = gameObject.GetComponent<Animator>();
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (GameManager.Instance._isIsland)
            return;
        if (_isMove && Time.frameCount % 3 == 0 && _mainMaterial != null)
            SetUp();
        if (!_isInputAble)
        {
            _rb.velocity = Vector3.zero;
            return;
        }

        Move();
        if (_isMove && _mainMaterial != null && grassPlace != null)
        {
            var position = touchPos.position;
            Vector2 pixelPos;
            pixelPos.x = (((grassPlace.position.x - this.transform.position.x) / grassPlace.localScale.x) + 5) * 0.1f;
            pixelPos.y = (((grassPlace.position.z - this.transform.position.z) / grassPlace.localScale.z) + 5) * 0.1f;
            _mainMaterial.SetVector(grassTrampleProperty, new Vector3(pixelPos.x, pixelPos.y + heightOffset, pixelPos.y));
        }
        
    }
    #endregion
    public void SetUp()
    {
        test = _mainMaterial.GetTexture("_NoGrassTex");

        tes = new Texture2D(test.width, test.height, TextureFormat.RGBA32, false);
        if(renderTexture == null)
            renderTexture = new RenderTexture(test.width, test.height, 32);

        //RenderTexture currentRT = RenderTexture.active;


        Graphics.Blit(test, renderTexture);
        tes.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        //tes.Apply();
    }

    private void Move()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;
        Vector3 move = new Vector3(horizontalInput * _infor.speed, 0, verticalInput * _infor.speed);
        _rb.velocity = move;

        if (horizontalInput != 0 || verticalInput != 0)
        {
            for (int i = 0; i < _humanController.Count; i++)
            {
                _humanController[i].SetRunning(true);
            }
            _isMove = true;
            if (_rb.velocity != Vector3.zero)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(_rb.velocity), 800f * Time.deltaTime);
        }
        else if(!_isReturnHome)
        {
            _isMove = false;
            for (int i = 0; i < _humanController.Count; i++)
            {
                _humanController[i].SetRunning(false);
            }
        }
    }

    public void UpdateEnter(int i)
    {
        switch (i)
        {
            case 0:
                _camCtrl.crrCam.gameObject.SetActive(false);
                _camCtrl.UpdateCam.gameObject.SetActive(true);
                transform.DOMove(parkUpdate.position, 1, false);
                transform.DORotate(new Vector3(0, 128, 0), 1, RotateMode.Fast); // 128 la vi tri tuong doi tu dat
                break;
            case 1:
                _camCtrl.crrCam.gameObject.SetActive(false);
                _camCtrl.ShopCam.gameObject.SetActive(true);
                transform.DOMove(parkShop.position, 1, false);
                transform.DORotate(new Vector3(0, 134, 0), 1, RotateMode.Fast); // 134 la vi tri tuong doi tu dat
                break;
        }
        _isInputAble = false;
        _showAble = false;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Building"))
        {
            _showAble = true;
        }
    }

    public void HumanMode(bool isHuman)
    {
        truckBody.gameObject.SetActive(isHuman);
        if (isHuman)
        {
            for(int i = 0; i < _humanController.Count; i++)
            {
                _humanController[i].SetState(true);
            }
        }
        else
        {
            for(int i = 0; i < _humanController.Count; i++)
            {
                _humanController[i].SetState(false);
            }
        }
    }
}
