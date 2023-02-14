using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.AI;

public class InGame : UICanvas
{
    XepProduct _set;

    public Animator _ani;

    public Image strongSlide;
    public Image completeSlide;

    public TextMeshProUGUI strorage;

    [SerializeField] public List<GameObject> star = new List<GameObject>(3);
    
    [SerializeField] private Sprite unComplete;

    private Color red => Color.red;
    private Color white => Color.white;

    private bool _isFull;

    public int pecentComplete;
    int starBefor;

    public static InGame Ins;

    private string succhua;

    public GameObject moneyIcon;
    public GameObject dimondIcon;
    public GameObject outlineBtnReturnHome;
    public GameObject BtnReturnHomeGroup;

    private void Awake()
    {
        Ins = this;
    }

    private void Start()
    {
        starBefor = PlayerDataManager.GetTotalStar();
        updateStrong();
        UpdateComplete();
        Debug.Log(PlayerDataManager.GetPlayerStorage().ToString());
        strorage.text = "0 / " + (PlayerDataManager.GetPlayerStorage() * 15).ToString(); 
    }

    private void Update()
    {
        if (!_isFull)
        {
            _ani.SetBool("_isFull", false);
            return;
        }
        _ani.SetBool("_isFull", true);
    }

    public void updateStrong()
    {
        _set = XepProduct.Isn;
        strongSlide.fillAmount = ((float)_set.numberofProduct / (float)_set.Succhua);
        if(_set.numberofProduct == _set.Succhua - 1)
            strorage.text = (_set.numberofProduct + 1).ToString() + " / " + _set.Succhua.ToString();
        else
            strorage.text = _set.numberofProduct.ToString() + " / " + _set.Succhua.ToString(); 
        if (Items.Ins._isMaxStorage)
            succhua = "Infinity";
        else
            succhua = (_set.numberofProduct + 1).ToString() + "/" +_set.Succhua.ToString();
        if (_set.numberofProduct + 1 == _set.Succhua)
            _isFull = true;
        else
            _isFull = false;
    }

    private void OnDisable()
    {
        _ani.SetBool("_out", true);
    }
    private void OnEnable()
    {
        _ani.SetBool("_out", false);
    }

    bool[] isOn = new bool[3];
    public float totalProduct;


    public void inAnimation()
    {
        _ani.SetBool("_out", true);
    }
    bool done;
    public GameObject LinePerfab;
    public GameObject MapTarget;
    public GameObject Outline;
    public int starOn = 0;
    public void UpdateComplete()
    {
        var dataLevel = GameManager.Instance.DataLevel;
        _set = XepProduct.Isn;
        completeSlide.fillAmount = ((float)infor.Ins.countProduct / (float)totalProduct);
        
        pecentComplete = Mathf.Max(0, (int)((int)(completeSlide.fillAmount * 100) / 25) - 1);
        if ((int)completeSlide.fillAmount * 100 % 25 == 0 && pecentComplete > 0 && completeSlide.fillAmount >= 0.5)
        {
            if(PlayerDataManager.GetCrrLevel() == 1 && !done)
            {
                outlineBtnReturnHome.SetActive(true);
                Instantiate(LinePerfab, PlayerController.Ins.truckBody.transform);
                done = true;
            }
            star[pecentComplete - 1].gameObject.SetActive(true);
            star[pecentComplete - 1].GetComponent<completeEffect>().RunAnim(pecentComplete - 1);
            if (!isOn[pecentComplete - 1])
            {
                if(GameManager.Instance.phaohoaFX != null)
                    GameManager.Instance.phaohoaFX.Play();
                isOn[pecentComplete - 1] = true;
                starOn++;
            }
        }
        PlayerDataManager.SetStar(Helper.Stats + SceneManager.GetSceneAt(1).name, Mathf.Min(pecentComplete, 3));
        PlayerDataManager.SetTotalStar(starBefor + Mathf.Min(pecentComplete, 3));
        //UiTop.Ins.UpdateUi(2);
    }
    private List<Vector3> point;
    public void ResetSlide()
    {
        for(int i = 0; i < 3; i++)
        {
            star[i].gameObject.SetActive(false);
        }

        completeSlide.fillAmount = 0;
        starBefor = PlayerDataManager.GetTotalStar();
    }

    public override void Show(bool _isShown, bool isHideMain = true)
    {
        base.Show(_isShown, isHideMain);
        if (!isShow)
            return;
        _ani.SetTrigger("_enterPlayMode");
    }
    public override void OnBackPressed()
    {
        Show(false);
    }
}

