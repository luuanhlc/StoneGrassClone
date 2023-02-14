using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "DataSkin", menuName = "ScriptableObjects/Data Skin", order = 1)]

public class DataSkinPlayer : SerializedScriptableObject
{
    public List<DataSkin> dataSkins;
}  
public class DataSkin
{
    public TypePay type;
    public int idItem;
    //public GameObject _truckSkin;
    public Sprite _itemIcon;
    public int price;
    public bool sold;
    //public Mesh body;
    //public Vector3 bodyScale;
    //public Material[] bodyMaterial;
}
