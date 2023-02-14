using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using MoreMountains.Tools;
using DG.Tweening;

public class ProductFactory : MonoBehaviour
{
    public DataFactory data;
    [SerializeField] private GameObject _buttonLayout;
    [SerializeField] private GameObject Panel;
    [SerializeField] private Image crrItems;
    [HideInInspector] public List<GameObject> btnList;
    [SerializeField] private Button btnSell;
    [SerializeField] private TextMeshProUGUI sellPrice;
    [SerializeField] private Image sanluong;
    int index;
    private float Value;

    #region singleton
    public static ProductFactory Ins;
    private void Awake()
    {
        Ins = this;

        btnSell.onClick.AddListener(SellClick);
        btnList = new List<GameObject>(data.dataFactory.Count);
        for (int i = 0; i < data.dataFactory.Count; i++)
        {
            btnList.Add(Instantiate(_buttonLayout, Panel.transform));
            btnList[i].GetComponent<buttonFactoryLayout>().RenBtn(data.dataFactory[i].idItem, data.dataFactory[i]._itemIcon, data.dataFactory[i].price, data.dataFactory[i].value, PlayerDataManager.getItemsOwn(data.dataFactory[i].idItem));
            btnList[i].GetComponent<buttonFactoryLayout>().SetBtn();

            if (i != 0)
            {
                crrItems.color = new Color(1, 1, 1, 1);
                if (PlayerDataManager.getItemsOwn(data.dataFactory[i].idItem) == 0)
                {
                    if (PlayerDataManager.getItemsOwn(data.dataFactory[i - 1].idItem) == 1)
                    {
                        crrItems.sprite = data.dataFactory[i - 1]._itemIcon;
                        index = i;
                    }
                }
                else if (PlayerDataManager.getItemsOwn(data.dataFactory[i].idItem) == 1)
                {
                    index = i;
                    crrItems.sprite = data.dataFactory[i]._itemIcon;
                }
            }
            else if (PlayerDataManager.getItemsOwn(data.dataFactory[i].idItem) == 1)
            {
                index = i;
                crrItems.sprite = data.dataFactory[i]._itemIcon;
            }
            if (PlayerDataManager.getItemsOwn(data.dataFactory[0].idItem) == 0)
                crrItems.color = new Color(1, 1, 1, 0);
        }
    }
    #endregion

    private void Update()
    {
        ResetIf();
    }

    private void OnEnable()
    {
        On();
    }
    public void On()
    {
        StartCoroutine(Visible());
    }
    public IEnumerator Visible()
    {
        for (int i = 0; i < btnList.Count; i++)
        {
            btnList[i].gameObject.transform.localScale = Vector3.zero;
        }

        for (int i = 0; i < btnList.Count; i++)
        {
            btnList[i].gameObject.transform.DOScale(1, UiController.Ins.InTime).SetEase(Ease.OutBounce);
            yield return Yielders.Get(.25f);
        }
    }
    private void SellClick()
    {
        GameManager.Instance.Profile.AddGold((int)Value);
        UiController.Ins.UiTop.UpdateUi(0);
        Value = 0;
        ResetIf();
        PlayerDataManager.SetLastDateFactory(DateTime.Now);
        MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins.moneySound, MMSoundManager.MMSoundManagerTracks.Sfx, Camera.main.gameObject.transform.position);
    }

    private void ResetIf()
    {
        float numberProduct = Timer.Isn.sanluongFactory;
        Value = (int)(numberProduct * data.dataFactory[index].value);
        sellPrice.text = Value.ToString() + " <sprite name=money>";
        sanluong.fillAmount = numberProduct / PlayerDataManager.GetStorage();
        if (numberProduct <= 0)
            btnSell.enabled = false;
        else
            btnSell.enabled = true;
    }

    public void SetImg(int id)
    {
        crrItems.color = new Color(1, 1, 1, 1);
        crrItems.sprite = data.dataFactory[id]._itemIcon;
    }

    public void ResetBtnList()
    {
        for (int i = 0; i < data.dataFactory.Count; i++)
        {
            btnList[i].GetComponent<buttonFactoryLayout>().SetBtn();
        }
    }
}
