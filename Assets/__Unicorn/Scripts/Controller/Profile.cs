using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profile
{
    #region Get_set_asset

    public void AddGold(int goldBonus)
    {
        int _count = Mathf.Max(0, GetGold() + goldBonus);
        PlayerDataManager.SetGold(_count);
    }

    public int GetGold()
    {
        return PlayerDataManager.GetGold();
    }

    public void AddDimond(int dimondBonus)
    {
        int _count = Mathf.Max(0, GetDimond() + dimondBonus);
        PlayerDataManager.SetDimond(_count);
    }

    public int GetDimond()
    {
        return PlayerDataManager.GetDimond();
    }

    public void AddStar(int count)
    {
        var playerdata = GameManager.Instance.PlayerDataManager;
        int _count = GetStars() + count;
        PlayerDataManager.SetTotalStar(_count);
    }

    public int GetStars()
    {
        return PlayerDataManager.GetTotalStar();
    }
    #endregion

    #region Get_set_infor_player
    public void AddSaw(int saw)
    {
        var playerdata = GameManager.Instance.PlayerDataManager;
        PlayerDataManager.SetNumberOfPongs(saw);
    }

    public float GetSaw()
    {
        return PlayerDataManager.GetNumberOfPongs();
    }

    /*public void AddSpeed(int speed)
    {
        PlayerDataManager.SetPlayerSpeed(speed);
    }

    public float GetSpeed()
    {
        return PlayerDataManager.GetPlayerSpeed();
    }*/

    #endregion

    public void AddKey(int amount, string _analytic)
    {
        var playerdata = GameManager.Instance.PlayerDataManager;

        PlayerDataManager.SetKey(GetKey() + amount);

        if (playerdata.actionUITop != null && amount == 1)
        {
            playerdata.actionUITop(TypeItem.Key);
        }
    }

    public int GetKey()
    {
        return PlayerDataManager.GetKey();
    }
}
