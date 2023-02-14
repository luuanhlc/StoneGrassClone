using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;


public class UiTop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtGold;
    [SerializeField] private TextMeshProUGUI txtDimond;
    [SerializeField] private TextMeshProUGUI txtStar;

    [SerializeField] private Button btnBackHome;
    
    [SerializeField] private Image sanluongbar;
    [SerializeField] private TextMeshProUGUI product;

    [SerializeField] private Transform transCoin;

    [SerializeField] private GameObject goldView;
    public GameObject goldFornt;
    [SerializeField] private GameObject island;
    [SerializeField] private GameObject uiInGame;

    [SerializeField] public List<GameObject> building;
    public GameObject btnSetting;

    /* [SerializeField] private Button btnDat;
     [SerializeField] private Button btnXoay;*/

    public Button windmill;
    public Button wareHouse;

    public Animator _ani;

    public bool _isBuild;

    public Sprite CanBuyImg;
    public Sprite CantBuyImg;

    public static UiTop Ins;

    public Sprite videoBtn;
    public Sprite payBtn;
    public Sprite dimondBtn;
    public Sprite take;

    private void Awake()
    {
        Ins = this;
    }

    private void Start()
    {
        btnBackHome.onClick.AddListener(UnActiveIsLand);

        //GameManager.Instance.PlayerDataManager.actionUITop += UpdateUIHaveAnim;
        resetUI();

        UpdateUi(0);
        UpdateUi(1);
        UpdateUi(2);
    }

    private void Update()
    {
        if (uiInGame.active == true)
            return;

        updateSlide();
    }

    public void setButton()
    {
        if (_isBuild)
        {
            windmill.enabled = false;
            wareHouse.enabled = false;
            return;
        }
        else
            resetUI();
    }

    public void windMill()
    {
        if (_isBuild || PlayerDataManager.GetTotalStar() < 5)
            return;

        _isBuild = true;
        PlayerDataManager.SetTotalStar(PlayerDataManager.GetTotalStar() - 5);

        CamController.Ins.target = GameObject.FindGameObjectWithTag("WindMill");
        StartCoroutine(turnOff());

#if UNITY_ANDROID
        BuildingSystem.Ins.InitializeWithObject(building[0]);
#endif

        //CameraController.Ins._IsLandTut.step1();
        
        UpdateUi(2);
    }

    IEnumerator turnOff()
    {
        yield return Yielders.Get(2f);
        CamController.Ins.target = null;
        CameraController.Ins._IsLandTut.nextStep();
        CameraController.Ins._IsLandTut.PressAndHoldStep();
    }

    public void WareHouseClick()
    {
        if (_isBuild || PlayerDataManager.GetTotalStar() < 5)
            return;
        _isBuild = true;
        PlayerDataManager.SetTotalStar(PlayerDataManager.GetTotalStar() - 5);
        CamController.Ins.target = GameObject.FindGameObjectWithTag("WareHouse");
        StartCoroutine(turnOff());


#if UNITY_ANDROID
        BuildingSystem.Ins.InitializeWithObject(building[1]);
#endif
        UpdateUi(2);
    }

    public void resetUI()
    {
        if(PlayerDataManager.GetTotalStar() < 5)
        {
            windmill.enabled = false;
            wareHouse.enabled = false;
            windmill.gameObject.GetComponent<Image>().sprite = CantBuyImg;
            wareHouse.gameObject.GetComponent<Image>().sprite = CantBuyImg;
        }
        else
        {
            windmill.gameObject.GetComponent<Image>().sprite = CanBuyImg;
            wareHouse.gameObject.GetComponent<Image>().sprite = CanBuyImg;
            windmill.enabled = true;
            wareHouse.enabled = true;
        }
        UpdateUi(2);
    }

    private void updateSlide()
    {
        //sanluongbar.fillAmount = Timer.Isn.sanluongDimond / PlayerDataManager.GetSorageIsLand();
        //product.text = ((int)(Timer.Isn.sanluongDimond)).ToString() + " / " + PlayerDataManager.GetSorageIsLand().ToString();
    }

    private void OnClickSell()
    {
        if (Timer.Isn.sanluongDimond < 1f)
            return;
        GameManager.Instance.Profile.AddDimond((int)Mathf.Round(Timer.Isn.sanluongDimond));
        Timer.Isn.sanluongDimond = 0;
        UpdateUi(1);
        PlayerDataManager.SetLastDate(DateTime.Now);
    }

    public void ActiveIslandMode()
    {
        Camera.main.orthographic = true;
        CamController.Ins.IsLandCam.m_Lens.OrthographicSize = 60;
        GameManager.Instance._isIsland = true;
        UiController.Ins._IsLandPopUp.isLand.SetActive(true);
        CameraController.Ins._isISland = true;
        goldFornt.SetActive(false);
        goldView.SetActive(false);
        uiInGame.SetActive(false);
        btnSetting.SetActive(false);
        UiController.Ins._IsLandPopUp.activeMainCam(false);
        island.SetActive(true);
    }

    private void UnActiveIsLand()
    {
        LoadScreen.Ins.Changer(1.6f);
        Camera.main.orthographic = false;
        StartCoroutine(waitToOff());
    }

    IEnumerator waitToOff()
    {
        yield return Yielders.Get(.3f);
        UiController.Ins._IsLandPopUp._player.SetActive(true);
        if (LobbyManager.Ins.weather != null)
            LobbyManager.Ins.weather.Play();
        if (runTutorial.Ins != null)
        {
            Destroy(runTutorial.Ins.tut);
        }
        GameManager.Instance._isIsland = false;
        UiController.Ins._IsLandPopUp.isLand.SetActive(false);
        goldView.SetActive(true);
        goldFornt.SetActive(true);
        btnSetting.SetActive(true);

        UiController.Ins._IsLandPopUp.activeMainCam(true);
        UiController.Ins._IsLandPopUp.joystickCanvas.SetActive(true);
        CameraController.Ins._isISland = false;
        uiInGame.SetActive(true);
        _ani.SetTrigger("_enterPlayMode");
        island.SetActive(false);
    }

    #region resetUi
    public void UpdateUi(int _type)
    {
        switch (_type)
        {
            case 0:
                txtGold.text = ConverInt.Sign(GameManager.Instance.Profile.GetGold());
                break;
            case 1:
                txtDimond.text = ConverInt.Sign(PlayerDataManager.GetDimond());
                break;
            case 2:
                txtStar.text = ConverInt.Sign(PlayerDataManager.GetTotalStar());
                break;
        }
    }

    private void UpdateUIHaveAnim(TypeItem _type)
    {
        switch (_type)
        {
            case TypeItem.Coin:
                {
                    SetTextCoin(GameManager.Instance.Profile.GetGold());
                    PlayAnimationScale(transCoin);
                    break;
                }
        }
    }
    #endregion
    #region animation dotween
    private Tweener tweenCoin;
    private int tmpCoin;
    private void SetTextCoin(int _coin)
    {
        tweenCoin = tweenCoin ?? DOTween.To(() => tmpCoin, x =>
        {
            tmpCoin = x;
            txtGold.text = tmpCoin.ToString();
        }, _coin, 0.3f).SetAutoKill(false).OnComplete(() =>
        {
            tmpCoin = GameManager.Instance.Profile.GetGold();
            txtGold.text = tmpCoin.ToString();
        });
        tweenCoin.ChangeStartValue(tmpCoin);
        tweenCoin.ChangeEndValue(_coin);
        tweenCoin.Play();
    }

    private void PlayAnimationScale(Transform transformScale)
    {
        transformScale.DOScale(1.4f, 0.1f).OnComplete(() => { transformScale.DOScale(1, 0.05f); });
    }

#endregion
}

