using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class Helper
{
    public const string Path_Lucky_Wheel = "UI/LuckyWheel";
    public const string Path_Shop_Skin = "UI/ShopCharacter";
    
    public const string DataLevel = "DataLevel";
    public const string DataStarLevel = "DataStarLevel";
    public const string DataMaxLevelReached = "DataMaxLevelReached";
    public const string RedLightTimes = "RedLightTimes";
    public const string GlassCount = "GlassCount";
    
    public const string Stats = "Stats";
    public const string DataTypeHat = "DataTypeHat";
    public const string DataTypeSkin = "DataTypeSkin";
    public const string DataTypePet = "DataTypePet";
    public const string DataEquipHat = "DataEquipHat";
    public const string DataEquipSkin = "DataEquipSkin";
    public const string DataEquipPet = "DataEquipPet";
    public const string DataNumberWatchVideo = "DataNumberWatchVideo";

    public const string MaxLevel = "MaxLevel";
    public const string CrrLevel = "CurrentLevel";
    #region DimondTime
    public const string DAY = "DDAY";
    public const string MONTH = "DMONTH";
    public const string YEAR = "DYEAR";
    public const string HOURS = "DHOURS";
    public const string MINS = "DMINS";
    public const string SECONDS = "DSECONDS";
    #endregion

    #region Island
    public const string StorageIsLand = "IL_StorageIsLand";
    public const string MineSpeedIsLand = "IL_MineSpeed";
    #endregion

    #region FactoryTime
    public const string FDAY = "FDAY";
    public const string FMONTH = "FMONTH";
    public const string FYEAR = "FYEAR";
    public const string FHOURS = "FHOURS";
    public const string FMINS = "FMINS";
    public const string FSECONDS = "FSECONDS";
    #endregion

    #region FactoryInfor
    public const string workSpeed = "FWORKSPEED";
    public const string qualityOfItems = "FQUALITYOFITEMS";
    public const string storage = "FSTORAGE";
    #endregion

    #region Miner
    public const string DIMONDPERSECOND = "MDIMONDPERSECOND";
    public const string BUILDINGCOUNT = "MBUILDINGCOUNT";
    #endregion

    #region PlayerInfor
    public const string GOLD = "IGOLD";
    public const string KEY = "IKEY";
    public const string DIMOND = "IDIMOND";
    public const string STAR = "ISTAR";

    public const string SAWCOUNT = "P_SAWCOUNT";
    public const string DAMAGE = "P_DAMAGE";
    public const string PlayerWheel = "P_PLAYERWHEEL";
    public const string PLAYERSTRONG = "P_PLAYERSTRONG";
    public const string SAWSPEED = "P_SAWSPEED";
    public const string SAWBLADES = "P_SAWBLADES";

    public const string PLAYERMAXSTRONG = "PLAYERMAXSTRONG";
    public const string PLAYERMAXSPEED = "PLAYERMAXSPEED";
    public const string PLAYERMAXNUMBEROFPONGS = "PLAYERMAXNUMBEROFPONGS";
    public const string PLAYERMAXSAWBLADES = "PLAYERMAXSAWBLADES";
    public const string PLAYERMAXSAWROTATIONSPEED = "PLAYERMAXSAWROTATIONSPEED";
    #endregion

    #region trainInfor
    public const string TRAINSPEED = "TRAIN_SPEED";
    public const string TrainStorage = "TRAIN_STORAGE";
    #endregion

    public const string CurrentRewardEndGame = "CurrentRewardEndGame";
    public const string ProcessReceiveEndGame = "ProcessReceiveEndGame";

    public const string video_shop_general = "video_shop_{0}";
    public const string video_shop_pet = "video_shop_pet";
    public const string video_shop_skin = "video_shop_skin";
    public const string video_shop_hat = "video_shop_hat";
    public const string video_reward_end_game = "video_reward_end_game";
    public const string video_reward_chest_key = "video_reward_chest_key";
    public const string video_reward_lucky_wheel = "video_reward_lucky_wheel";
    public const string video_reward_revive = "video_reward_revive";
    public const string video_reward_x3_gold_end_game = "video_x3_gold_end_game";
    public const string video_reward_choose_role = "video_reward_choose_role";
    public const string video_reward_gift_box = "video_reward_gift_box";
    public const string video_reward_upgrade = "video_reward_upgrade_{0}";
    public const string video_reward_power_up = "video_reward_power_up";
    public const string video_reward_choose_pos_game_tug = "video_reward_choose_pos_game_tug";
    public const string video_reward_glass_stepping_tip = "video_reward_glass_stepping_tip";
    public const string video_reward_get_more_marbles = "video_reward_get_more_marbles";
    public const string video_reward_get_more_time = "video_reward_get_more_time";
    public const string video_reward_explode_doll = "video_reward_explode_doll";
    public const string video_reward_tug_freeze = "video_reward_tug_freeze";
    public const string video_reward_ntf_snap = "video_reward_ntf_snap";
    public const string video_reward_paper_slap = "video_reward_paper_slap";
    public const string video_reward_candy_lick = "video_reward_candy_lick";


    public const string TruckIndex = "TRUCKINDEX";


    public const string SoundSetting = "SoundSetting";
    public const string Vibration = "VibrationSetting";
    public const string MusicSetting = "MusicSetting";

    public static string FormatTime(int minute, int second, bool isSpaceSpecial = false)
    {
        StringBuilder sb = new StringBuilder();
        if (minute < 10)
        {
            sb.Append("0");
        }

        sb.Append(minute);
        if (isSpaceSpecial)
        {
            sb.Append("M");
            sb.Append(" ");
        }
        else
        {
            sb.Append(":");
        }

        if (second < 10)
        {
            sb.Append("0");
        }
        sb.Append(second);
        if (isSpaceSpecial)
        {
            sb.Append("S");
        }
        return sb.ToString();
    }
    public static string FormatTimeIgnoreSecond(int hour, int minute, bool isSpaceSpecial = false)
    {
        StringBuilder sb = new StringBuilder();
        if (hour < 10)
        {
            sb.Append("0");
        }

        sb.Append(hour);

        if (isSpaceSpecial)
        {
            sb.Append("H");
            sb.Append(" ");
        }
        else
        {
            sb.Append(":");
        }
        if (minute < 10)
        {
            sb.Append("0");
        }

        sb.Append(minute);
        if (isSpaceSpecial)
        {
            sb.Append("M");
        }

        return sb.ToString();
    }

    public static int GetRandomGoldReward()
    {
        return UnityEngine.Random.Range(100, 250);
    }

    public static bool CheckNewDay(string stringTimeCheck, bool isUnbiasedTime)
    {
        if (string.IsNullOrEmpty(stringTimeCheck))
        {
            return true;
        }
        try
        {
            DateTime timeNow = DateTime.Now;
            DateTime timeOld = DateTime.Parse(stringTimeCheck);
            DateTime timeOldCheck = new DateTime(timeOld.Year, timeOld.Month, timeOld.Day, 0, 0, 0);
            long tickTimeNow = timeNow.Ticks;
            long tickTimeOld = timeOldCheck.Ticks;

            long elapsedTicks = tickTimeNow - tickTimeOld;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
            double totalDay = elapsedSpan.TotalDays;

            if (totalDay >= 1)
            {
                return true;
            }
        }
        catch
        {
            return true;
        }

        return false;
    }
}
