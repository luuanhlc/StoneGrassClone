using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FillCau : MonoBehaviour
{
    public int _height, _weight, _long;
    public int SucChua;
    public int takedN;
    [SerializeField] private GameObject[] Object;

    public GameObject wall;
    int x;
    public Vector3 OriginPos = new Vector3(-0.4f, 0.3f, .1f);

    public bool _Full;
    public bool need = true;
    #region singleton

    public static FillCau Isn;

    private void Awake()
    {
        if (Isn == null)
            Isn = this;
        else if (Isn != this)
            Destroy(gameObject);
        
        updateSucChua();
    }

    #endregion

    public void updateSucChua()
    {
        SucChua = _weight * _height * _long;
    }
    public void scaleOb()
    {
        int oneFloor = (_weight * _long);
        int h = (takedN / oneFloor) + 1;
        int i;
        if (takedN % oneFloor == 0)
        {
            i = oneFloor - 1;             
            h = takedN / oneFloor;
        }
        else
            i = (takedN % oneFloor) - 1;
        
        Object[i].transform.localScale = new Vector3(.6f, 0.4f * h, .4f);
        Object[i].transform.localPosition = new Vector3(Object[i].transform.localPosition.x, h * .1f + .4f, Object[i].transform.localPosition.z);
    }
}
