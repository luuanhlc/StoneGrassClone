using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class BuildingData : MonoBehaviour
{
    public static BuildingData Ins;

    public string perfabID;
    public int Storage;
    public float SpeedMine;
    public Vector3Int start;

    private void Awake()
    {
        Ins = this;
    }

    private void Start()
    {
        //Apply();
    }
    public void Apply()
    {
        switch (this.gameObject.tag)
        {
            case "WareHouse":
                PlayerDataManager.SetStorageIsLand(PlayerDataManager.GetSorageIsLand() + Storage);
                IsLand.Ins.setText();
                break;
            case "WindMill":
                
                PlayerDataManager.SetSpeedMineIsLand(PlayerDataManager.GetSpeedMineIsLand() + SpeedMine);
                PlayerDataManager.SetStorageIsLand(PlayerDataManager.GetSorageIsLand() + Storage);
                IsLand.Ins.setText();
                GameManager.Instance.Profile.AddDimond((int)Mathf.Round(Timer.Isn.sanluongDimond));
                PlayerDataManager.SetLastDate(DateTime.Now);
                break;
        }

        
    }        
}
