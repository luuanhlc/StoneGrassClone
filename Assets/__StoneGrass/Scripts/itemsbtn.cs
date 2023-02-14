using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unicorn;

public class itemsbtn : MonoBehaviour
{
    [SerializeField] private TypeEquipment typeEquipment;

    string name;
    public void OnClick(string name)
    {
        this.name = name;
        
        UnicornAdManager.ShowAdsReward(click, string.Format(Helper.video_shop_general, typeEquipment));
        
    }
    void click(int x)
    {
        Items.Ins.UseItems(name);
    }
}
