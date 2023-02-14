using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vanOfTrain : MonoBehaviour
{
    public int SucChua;
    public int takedN;
    [SerializeField] private GameObject[] Object = new GameObject[15];
    int x;
    public Vector3 OriginPos = new Vector3(-0.4f, 0.3f, .1f);
    #region singleton

    public static vanOfTrain Isn;

    private void Awake()
    {
        if (Isn == null)
            Isn = this;
        else if (Isn != this)
            Destroy(gameObject);
        for (int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                Object[x] = GameObject.Find(i + "" + j + this.gameObject.name);
                //Debug.Log(i + "" + j + this.gameObject.name);
                x++;
            }
        }
        updateSucChua();
    }

    #endregion

    public void updateSucChua()
    {
        SucChua = 15 * PlayerDataManager.GetTrainStorage();
    }
    public void scaleOb()
    {
        // 15 la so khoi tren cung mot tang
        // h la chieu cao cua khoi
        // i la vi tri cua khoi tren ma tran
        int h = (takedN / 15) + 1;
        int i;
        if (takedN % 15 == 0)
        {
            i = 14;             // 15/15 = 0 => vi tri trong ma tran 14;
            h = takedN / 15;
        }
        else
            i = (takedN % 15) - 1;
        
        Object[i].transform.localScale = new Vector3(.4f, 0.2f * h, .2f);
        Object[i].transform.localPosition = new Vector3(Object[i].transform.localPosition.x, h * .1f + .2f, Object[i].transform.localPosition.z);
    }

    public void resetScale()
    {
        for(int i = 0; i < 15; i++)
        {
            Object[i].transform.localScale = Vector3.zero;
            Object[i].transform.localPosition = new Vector3(Object[i].transform.localPosition.x, .2f, Object[i].transform.localPosition.z);
        }
        takedN = 0;
    }
}

