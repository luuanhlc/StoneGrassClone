using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerSkin : MonoBehaviour
{
    [SerializeField] DataSkinPlayer _dataskin;

    [SerializeField] private GameObject _btnLayout;
    [SerializeField] private GameObject Panel;

    private List<GameObject> btn;

    #region singleton
    public static PlayerSkin Ins;
    private void Awake()
    {
        Ins = this;
        Init();
    }
    #endregion

    public void Init()
    {
        btn = new List<GameObject>(_dataskin.dataSkins.Count);

        for (int i = 0; i < 3; i++)
        {
            btn.Add(Instantiate(_btnLayout, Panel.transform));
            Debug.Log(_dataskin.dataSkins[i]);
            btn[i].GetComponent<btnSkinPlayer>().RenBtnSkin(_dataskin.dataSkins[i]);
        }
    }

    private void OnEnable()
    {
        On();
    }
    public void ResetBtn()
    {
        for (int i = 0; i < 3; i++)
        {
            btn[i].GetComponent<btnSkinPlayer>().RenBtnSkin(_dataskin.dataSkins[i]);
        }
    }
    public void On()
    {
        StartCoroutine(Visible());  
    }
    public IEnumerator Visible()
    {
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

