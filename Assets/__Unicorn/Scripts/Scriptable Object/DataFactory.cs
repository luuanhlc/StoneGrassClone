using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

[CreateAssetMenu(fileName = "DataFactory", menuName = "ScriptableObjects/Data Factory", order = 1)]

public class DataFactory : SerializedScriptableObject
{
    public List<DataFactoryItem> dataFactory;
}


public class DataFactoryItem
{
    public int idItem;
    public Sprite _itemIcon;
    public int value;
    public int price;
}