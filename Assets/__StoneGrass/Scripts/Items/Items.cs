using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    #region Declare
    PlayerController _plctrl;

    [HideInInspector] public bool _isMagnet;
    [HideInInspector] public bool _isMaxStorage;
    [HideInInspector] public bool _isIfDame;

    [SerializeField] private List<TextMeshProUGUI> countDown;

    [SerializeField] private DOTweenAnimation _namchamDT;
    [SerializeField] private DOTweenAnimation _ifnDameDt;
    [SerializeField] private DOTweenAnimation _ifnStorage;

    [SerializeField] private List<Image> _actveImg;

    float time = 30f;

    private bool _isUseItems;
    List<int> itemId = new List<int>();

    public DOTweenAnimation ScaleStorage;
    public DOTweenAnimation LerpColor;

    public SphereCollider arena;

    private int originStorage;
    #endregion

    #region singleton
    public static Items Ins;
    private void Awake()
    {
        Ins = this;
    }

    #endregion

    #region Defaul
    private void Start()
    {
        
        _plctrl = PlayerController.Ins;
    }

    private void Update()
    {
        if (GameManager.Instance._isIsland)
            return;
        
        if (!_isUseItems)
        {
            for (int i = 0; i < countDown.Count; i++)
                countDown[i].text = "";
            time = 30f;
            return;
        }
        time = Mathf.Max(0, (time - Time.deltaTime));
        for (int i = 0; i < itemId.Count; i++)
        {
            if(itemId[i] != null)
                setCountDownText(itemId[i], (int)time);
        } 
    }
    #endregion
    private void setCountDownText(int itemId, int time)
    {
        countDown[itemId].text = time.ToString() + "s";
        _actveImg[itemId].fillAmount = (float)time / 30f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Items"))
            return;
        //Debug.Log("use");
        string name = other.gameObject.name;
        UseItems(name);
        Destroy(other.gameObject);
    }
    public void UseItems(string name)
    {
        switch (name)
        {
            case "NamCham":
                originStorage = XepProduct.Isn.Succhua;
                arena.radius = 4f; 
                StartCoroutine(coutdown(30f));
                _isMagnet = true;
                itemId.Add(0);
                _isUseItems = true;
                _namchamDT.DOPlay();
                _actveImg[0].fillAmount = 1;
                break;
            case "IfnStorage":
                originStorage = XepProduct.Isn.Succhua;

                StartCoroutine(coutdown(30f));
                _isUseItems = true;
                itemId.Add(1);
                _actveImg[1].fillAmount = 1;
                _ifnStorage.DOPlay();
                LerpColor.DOPlay();
                ScaleStorage.DOPlay();
                _isMaxStorage = true;
                XepProduct.Isn.Succhua = 1000;
                break;
            case "IfnDame":
                originStorage = XepProduct.Isn.Succhua;
                StartCoroutine(coutdown(30f));
                itemId.Add(2);
                _isIfDame = true;
                _isUseItems = true;
                _actveImg[2].fillAmount = 1;
                _ifnDameDt.DOPlay();
                break;

            
            case "Dimond":
                GameManager.Instance.Profile.AddDimond(1);
                UiTop.Ins.UpdateUi(1);
                break;
        }
    }

    public IEnumerator coutdown(float time)
    {
        yield return Yielders.Get(time);
        arena.radius = 2.1f;
        _plctrl._infor.ChangerSawScale(_plctrl._sawCtrl.orsawScale);
        _isMagnet = false;
        _namchamDT.DORewind();
        _ifnStorage.DOKill();
        LerpColor.DOKill();
        ScaleStorage.DOKill();
        XepProduct.Isn.Succhua = originStorage;
        _isUseItems = false;
    }

}
