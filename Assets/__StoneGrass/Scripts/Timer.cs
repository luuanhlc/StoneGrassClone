using UnityEngine;
using System;
public class Timer : MonoBehaviour
{
    [HideInInspector] public float sanluongDimond;
    [HideInInspector] public float sanluongFactory;
    public static Timer Isn;
    private void Awake()
    {
        Isn = this;    
    }

    private void Start()
    {
        PlayerDataManager.SetLastDateFactory(DateTime.Now);
    }

    private void Update()
    {
        if (!UiController.Ins.UiFactory._isFactory)
            return;
        SanLuong();
        SanLuongFactory();
    }

    private void SanLuong()
    {
        double second = (DateTime.Now - PlayerDataManager.GetLastTime()).TotalSeconds;
        sanluongDimond = Mathf.Min(((float)second / 60f) * PlayerDataManager.GetSpeedMineIsLand(), PlayerDataManager.GetSorageIsLand());
    }

    private void SanLuongFactory()
    {
        double second = (DateTime.Now - PlayerDataManager.GetLastTimeFactory()).TotalSeconds;
        sanluongFactory = Mathf.Min((float)second * PlayerDataManager.GetWorkSpeed() * PlayerDataManager.GetQuality(), PlayerDataManager.GetStorage());
    }

}
