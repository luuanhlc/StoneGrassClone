using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "DataTruck", menuName = "ScriptableObjects/Data Truck", order = 1)]

public class DataTruck : SerializedScriptableObject
{
    public List<DataTruckItem> dataTruck;
}  
public class DataTruckItem
{
    public TypePay type;
    public int idItem;
    public GameObject _truckSkin;
    public Sprite _itemIcon;
    public int price;
    public bool sold;
    public Mesh body;
    public Vector3 bodyScale;
    public Material[] bodyMaterial;

    public List<wheelData> whellList;
}

public class wheelData
{
    public Mesh wheel;
    public GameObject mtr;
}