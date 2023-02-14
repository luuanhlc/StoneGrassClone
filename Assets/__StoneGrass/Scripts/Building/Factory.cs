using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Factory : MonoBehaviour
{
    [HideInInspector] public float sanluongFactory;

    public static Factory Ins;
    private void Awake()
    {
        Ins = this;
    }

    private void Start()
    {
        PlayerDataManager.SetLastDateFactory(DateTime.Now);
    }

    private void Update()
    {
        SanLuong();
    }

    private void SanLuong()
    {
        double second = (DateTime.Now - PlayerDataManager.GetLastTime()).TotalSeconds;
    }
}
