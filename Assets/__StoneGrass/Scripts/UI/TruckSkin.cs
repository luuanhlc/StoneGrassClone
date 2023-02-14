using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TruckSkin : MonoBehaviour
{
    [SerializeField] DataTruck _dataTruck;

    [SerializeField] private GameObject _btnLayout;
    [SerializeField] private GameObject Panel;

    private List<GameObject> btn;

    #region singleton
    public static TruckSkin Ins;
    private void Awake()
    {
        Ins = this;
        Init();
    }
    #endregion

    public void Init()
    {
        btn = new List<GameObject>(_dataTruck.dataTruck.Count);

        for (int i = 0; i < 3; i++)
        {
            btn.Add(Instantiate(_btnLayout, Panel.transform));
            btn[i].GetComponent<btnSkinLayout>().RenBtnSkin(_dataTruck.dataTruck[i]);
        }
    }

    private void OnEnable()
    {
        On();
        PlayerDataManager.SetDataTruckPreView(-1);
        for (int i = 0; i < 3; i++)
        {
            btn[i].GetComponent<btnSkinLayout>().RenBtnSkin(_dataTruck.dataTruck[i]);
        }
    }
    public void On()
    {
        StartCoroutine(Visible());
    }

    public void ResetBtn()
    {
        for(int i = 0; i < 3; i++)
        {
            btn[i].GetComponent<btnSkinLayout>().RenBtnSkin(_dataTruck.dataTruck[i]);
        }
    }

    public IEnumerator Visible()
    {
        Debug.Log(btn.Count);
        for(int i = 0; i < btn.Count; i++)
        {
            btn[i].gameObject.transform.localScale = Vector3.zero;
        }

        for(int i = 0; i < btn.Count; i++)
        {
            btn[i].gameObject.transform.DOScale(1, UiController.Ins.InTime).SetEase(Ease.OutBounce);
            yield return Yielders.Get(.1f);
        }
    }
}

