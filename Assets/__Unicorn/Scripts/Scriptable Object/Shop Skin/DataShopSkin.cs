using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "DataShopSkin", menuName = "ScriptableObjects/Data Shop Skin", order = 1)]
public class DataShopSkin : SerializedScriptableObject
{
    [SerializeField]
    [TableList(ShowIndexLabels = true, DrawScrollView = true, MaxScrollViewHeight = 400, MinScrollViewHeight = 200)]
    private Dictionary<TypeEquipment, List<DataShop>> dataShop = new Dictionary<TypeEquipment, List<DataShop>>();
    
    public List<DataShop> GetDataShop(TypeEquipment typeEquipment)
    {
        if (!dataShop.ContainsKey(typeEquipment))
        {
            throw new NullReferenceException(
                $"Value of type {typeEquipment} has not been assigned to {nameof(DataShopSkin)}");
        }
        return dataShop[typeEquipment];
    }
}

public class DataShop
{
    public int idSkin;
    public TypeUnlockSkin typeUnlock;
    public int numberVideoUnlock;
    public int numberCoinUnlock;
    public int levelUnlock;
}


