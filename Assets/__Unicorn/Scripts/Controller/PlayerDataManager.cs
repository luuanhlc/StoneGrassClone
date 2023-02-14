using System;
using System.Collections;
using System.Collections.Generic;
using Unicorn;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;
    
    public Action<TypeItem> actionUITop;
    public DataTexture DataTexture;
    public DataTextureSkin DataTextureSkin;
    public DataRewardEndGame DataRewardEndGame;
    public DataLuckyWheel DataLuckyWheel;

    private static IDataLevel dataLevel;
    private static Dictionary<int, float> redLightTimes;

    private void Awake()
    {
        Instance = this;
        dataLevel = null;
    }

    public static void SetDataLevel(IDataLevel dataLevel)
    {
        PlayerDataManager.dataLevel = dataLevel;
        PlayerPrefs.SetString(Helper.DataLevel, Newtonsoft.Json.JsonConvert.SerializeObject(dataLevel));
    }

    public static IDataLevel GetDataLevel(LevelConstraint levelConstraint)
    {
        var dataLevelJson = PlayerPrefs.GetString(Helper.DataLevel, string.Empty);
        dataLevel = dataLevelJson == string.Empty
            ? new UnicornDataLevel(levelConstraint)
            : Newtonsoft.Json.JsonConvert.DeserializeObject<UnicornDataLevel>(dataLevelJson);

        return dataLevel ?? new UnicornDataLevel(levelConstraint);
    }

    public static void SetPlayerCurrent(int i)
    {
        PlayerPrefs.SetInt("CurrentPlayer", i);
    }

    public static int GetPlayerCurrent()
    {
        return PlayerPrefs.GetInt("CurrentPlayer", 0);
    }

    /*-----------------------------------------------------------------*/
    /*-----------------------------------------------------------------*/
    /*-----------------------------------------------------------------*/
    /*public static bool GetUnlockSkin(TypeEquipment type, int id)
    {
        return PlayerPrefs.GetInt(Helper.DataTypeSkin + type + id, 0) != 0;
    }

    public static void SetUnlockSkin(TypeEquipment type, int id)
    {
        PlayerPrefs.SetInt(Helper.DataTypeSkin + type + id, 1);
        SetIdEquipSkin(type, id);
    }

    public static int GetIdEquipSkin(TypeEquipment type)
    {
        return PlayerPrefs.GetInt(Helper.DataEquipSkin + type, -1);
    }

    public static void SetIdEquipSkin(TypeEquipment type, int id)
    {
        PlayerPrefs.SetInt(Helper.DataEquipSkin + type, id);
    }

    public static int GetNumberWatchVideoSkin(TypeEquipment type, int id)
    {
        return PlayerPrefs.GetInt(Helper.DataNumberWatchVideo + type + id, 0);
    }

    public static void SetNumberWatchVideoSkin(TypeEquipment type, int id, int number)
    {
        PlayerPrefs.SetInt(Helper.DataNumberWatchVideo + type + id, number);
    }*/
    /*-----------------------------------------------------------------*/
    /*-----------------------------------------------------------------*/
    /*-----------------------------------------------------------------*/

    #region Get_set_time

    public static DateTime GetLastTime()
    {
        DateTime crrTime = System.DateTime.Now;

        DateTime lastTime = new DateTime(PlayerPrefs.GetInt(Helper.YEAR, crrTime.Year),
            PlayerPrefs.GetInt(Helper.MONTH, crrTime.Month),
            PlayerPrefs.GetInt(Helper.DAY, crrTime.Day),
            PlayerPrefs.GetInt(Helper.HOURS, crrTime.Hour),
            PlayerPrefs.GetInt(Helper.MINS, crrTime.Minute),
            PlayerPrefs.GetInt(Helper.SECONDS, crrTime.Second));

        return lastTime;
    }

    public static void SetLastDate(DateTime time)
    {
        PlayerPrefs.SetInt(Helper.YEAR, time.Year);
        PlayerPrefs.SetInt(Helper.MONTH, time.Month);
        PlayerPrefs.SetInt(Helper.DAY, time.Day);
        PlayerPrefs.SetInt(Helper.HOURS, time.Hour);
        PlayerPrefs.SetInt(Helper.MINS, time.Minute);
        PlayerPrefs.SetInt(Helper.SECONDS, time.Second);
    }

    public static DateTime GetLastTimeFactory()
    {
        DateTime crrTime = System.DateTime.Now;

        DateTime lastTime = new DateTime(PlayerPrefs.GetInt(Helper.FYEAR, crrTime.Year),
            PlayerPrefs.GetInt(Helper.FMONTH, crrTime.Month),
            PlayerPrefs.GetInt(Helper.FDAY, crrTime.Day),
            PlayerPrefs.GetInt(Helper.FHOURS, crrTime.Hour),
            PlayerPrefs.GetInt(Helper.FMINS, crrTime.Minute),
            PlayerPrefs.GetInt(Helper.FSECONDS, crrTime.Second));

        return lastTime;
    }

    public static void SetLastDateFactory(DateTime time)
    {
        PlayerPrefs.SetInt(Helper.FYEAR, time.Year);
        PlayerPrefs.SetInt(Helper.FMONTH, time.Month);
        PlayerPrefs.SetInt(Helper.FDAY, time.Day);
        PlayerPrefs.SetInt(Helper.FHOURS, time.Hour);
        PlayerPrefs.SetInt(Helper.FMINS, time.Minute);
        PlayerPrefs.SetInt(Helper.FSECONDS, time.Second);
    }

    #endregion
    /*-----------------------------------------------------------------*/
    /*-----------------------------------------------------------------*/
    /*-----------------------------------------------------------------*/
    #region FactoryInFor
    public static float GetWorkSpeed()
    {
        return PlayerPrefs.GetFloat(Helper.workSpeed, 0);
    }

    public static int GetStorage()
    {
        return PlayerPrefs.GetInt(Helper.storage, 10);
    }

    public static float GetQuality()
    {
        return PlayerPrefs.GetFloat(Helper.qualityOfItems, 0f);
    }

    public static void SetWorkSpeed(float speed)
    {
        PlayerPrefs.SetFloat(Helper.workSpeed, speed);
    }

    public static void SetQuality(float quality)
    {
        PlayerPrefs.SetFloat(Helper.qualityOfItems, quality);
    }

    public static void SetStorage(int storage)
    {
        PlayerPrefs.SetInt(Helper.storage, storage);
    }
    #endregion

    /*-----------------------------------------------------------------*/
    /*-----------------------------------------------------------------*/
    /*-----------------------------------------------------------------*/

    #region FactoryData
    public static void setItemsOwn(int id, int i)
    {
        PlayerPrefs.SetInt(id.ToString(), i);
    }

    public static int getItemsOwn(int id)
    {
        return PlayerPrefs.GetInt(id.ToString(), 0);
    }

    #endregion

    public static void SetCrrLevel(int i)
    {
        PlayerPrefs.SetInt(Helper.CrrLevel, i);
    }

    public static int GetCrrLevel()
    {
        return PlayerPrefs.GetInt(Helper.CrrLevel, 1);
    }
    public static void SetMaxLevel(int i)
    {
        if (i < GetMaxLevel())
            return;

        PlayerPrefs.SetInt(Helper.MaxLevel, i);
    }

    public static int GetMaxLevel()
    {
        return PlayerPrefs.GetInt(Helper.MaxLevel, 1);
    }
    #region Get_set_infor_player
    public static int GetSawBlade()
    {
        return PlayerPrefs.GetInt(Helper.SAWBLADES, 4);
    }

    public static void SetSawBlade(int _count)
    {
        PlayerPrefs.SetInt(Helper.SAWBLADES, _count);
    }
    public static float GetNumberOfPongs()
    {
        return PlayerPrefs.GetFloat(Helper.SAWCOUNT, 3f);
    }

    public static void SetNumberOfPongs(float _count)
    {
        PlayerPrefs.SetFloat(Helper.SAWCOUNT, _count);
    }

    public static int GetSawSpeed()
    {
        return PlayerPrefs.GetInt(Helper.SAWSPEED, 900);
    }

    public static void SetSawSpeed(int speed)
    {
        PlayerPrefs.SetInt(Helper.SAWSPEED, speed);
    }

    public static int GetDamage()
    {
        return PlayerPrefs.GetInt(Helper.DAMAGE, 50);
    }

    public static void SetDamage(int _count)
    {
        PlayerPrefs.SetInt(Helper.DAMAGE, _count);
    }

    public static int GetTruckIndex()
    {
        return PlayerPrefs.GetInt(Helper.TruckIndex, 0);
    }

    public static void SetTruckIndex(int i)
    {
        PlayerPrefs.SetInt(Helper.TruckIndex, i);
    }

    public static void SetDataTruckPreView(int i)
    {
        PlayerPrefs.SetInt("PreViewTrruck", i);
    }

    public static int GetDataTruckPreView()
    {
        return PlayerPrefs.GetInt("PreViewTrruck", -1);
    }

    public static void SetDataPlayerPreView(int i)
    {
        PlayerPrefs.SetInt("PreViewPlayer", i);
    }

    public static int GetDataPlayerPreView()
    {
        return PlayerPrefs.GetInt("PreViewPlayer", -1);
    }

    public static int GetPlayerWhell()
    {
        return PlayerPrefs.GetInt(Helper.PlayerWheel, 0);
    }
    
    public static void SetWheel(int wheel)
    {
        PlayerPrefs.SetInt(Helper.PlayerWheel, wheel);
    }

    public static int GetPlayerStorage()
    {
        return PlayerPrefs.GetInt(Helper.PLAYERSTRONG, 10);
    }

    public static void SetPlayerStorage(int tang)
    {
        PlayerPrefs.SetInt(Helper.PLAYERSTRONG, tang);
    }

    public static void SetTutorialDone(int v)
    {
         PlayerPrefs.SetInt("MapTutorial", v);
    }

    public static int GetTutorialDone()
    {
        return PlayerPrefs.GetInt("MapTutorial", 0);
    }

    public static void SetTutorialMap(int v)
    {
        PlayerPrefs.SetInt("TutorialMap", v);
    }

    public static int GetTutorialMap()
    {
        return PlayerPrefs.GetInt("TutorialMap", 0);
    }

    #endregion
    /*-----------------------------------------------------------------*/
    /*-----------------------------------------------------------------*/
    /*-----------------------------------------------------------------*/
    #region Get_Set_infor_asset
    public static int GetGold()
    {
        return PlayerPrefs.GetInt(Helper.GOLD, 100);
    }

    public static void SetGold(int _count)
    {
        PlayerPrefs.SetInt(Helper.GOLD, _count);
    }

    public static int GetDimond()
    {
        return PlayerPrefs.GetInt(Helper.DIMOND, 100);
    }

    public static void SetDimond(int _count)
    {
        PlayerPrefs.SetInt(Helper.DIMOND, _count);
    }

    public static int GetKey()
    {
        return PlayerPrefs.GetInt(Helper.KEY, 0);
    }

    public static void SetKey(int _count)
    {
        PlayerPrefs.SetInt(Helper.KEY, _count);
    }

    public static int GetStar(String name)
    {
        //Debug.Log(PlayerPrefs.GetInt(Helper.STAR + dataLevel.DisplayLevel, 0));
        return PlayerPrefs.GetInt(name, 0);
    }

    public static void SetStar(string name, int _count)
    {
        PlayerPrefs.SetInt(name, _count) ;
    }
    public static void SetTotalStar(int _count)
    {
        PlayerPrefs.SetInt(Helper.STAR, Math.Max(0, _count));
    }
    public static int GetTotalStar()
    {
        return PlayerPrefs.GetInt(Helper.STAR, 5);
    }
    #endregion
    /*-----------------------------------------------------------------*/
    /*-----------------------------------------------------------------*/
    /*-----------------------------------------------------------------*/
    #region Get_max_infor
    public static int GetMaxPlayerStrong()
    {
        // Lay suc chua cua lon nhat co the update dc neu ko co thi tra ve 200
        return PlayerPrefs.GetInt(Helper.PLAYERMAXSTRONG, 200);
    }

    public static int GetMaxSawBlade()
    {
        return PlayerPrefs.GetInt(Helper.PLAYERMAXSAWBLADES, 20);
    }

    public static int GetMaxSaw()
    {
        return PlayerPrefs.GetInt(Helper.PLAYERMAXNUMBEROFPONGS, 5);
    }

    public static int GetMaxSpeed()
    {
        return PlayerPrefs.GetInt(Helper.PLAYERMAXSAWROTATIONSPEED, 1000);
    }

    public static int GetMaxSawSpeed()
    {
        return PlayerPrefs.GetInt(Helper.PLAYERMAXSAWROTATIONSPEED, 500);
    }
    #endregion
    /*-----------------------------------------------------------------*/
    /*-----------------------------------------------------------------*/
    /*-----------------------------------------------------------------*/
    #region Get_set_infor_train
    public static float GetTrainSpeed()
    {
        return PlayerPrefs.GetFloat(Helper.TRAINSPEED, 5f);
    }

    public static void SetTrainSpeed(float speed)
    {
        Debug.Log(speed);
        PlayerPrefs.SetFloat(Helper.TRAINSPEED, speed);
    }

    public static int GetTrainStorage()
    {
        return PlayerPrefs.GetInt(Helper.TrainStorage, 10);
    }

    public static void SetTrainStorage(int strong)
    {
        PlayerPrefs.SetInt(Helper.TrainStorage, strong);
    }
    #endregion
    /*-----------------------------------------------------------------*/
    /*-----------------------------------------------------------------*/
    /*-----------------------------------------------------------------*/

    #region mine
    public static void SetSpeedMineIsLand(float x)
    {
        PlayerPrefs.SetFloat(Helper.MineSpeedIsLand, x);
    }

    public static float GetSpeedMineIsLand()
    {
        return PlayerPrefs.GetFloat(Helper.MineSpeedIsLand, 0);
    }

    public static void SetStorageIsLand(int i)
    {
        PlayerPrefs.SetInt(Helper.StorageIsLand, i);
    }
    
    public static int GetSorageIsLand()
    {
        return PlayerPrefs.GetInt(Helper.StorageIsLand, 0);
    }
    #endregion
    public static int GetCurrentIndexRewardEndGame()
    {
        return PlayerPrefs.GetInt(Helper.CurrentRewardEndGame, 0);
    }

    public static void SetCurrentIndexRewardEndGame(int index)
    {
        PlayerPrefs.SetInt(Helper.CurrentRewardEndGame, index);
    }

    public static int GetProcessReceiveRewardEndGame()
    {
        return PlayerPrefs.GetInt(Helper.ProcessReceiveEndGame, 0);
    }

    public static void SetProcessReceiveRewardEndGame(int number)
    {
        PlayerPrefs.SetInt(Helper.ProcessReceiveEndGame, number);
    }


    public int GetNumberWatchDailyVideo()
    {
        return PlayerPrefs.GetInt("NumberWatchDailyVideo", DataLuckyWheel.NumberSpinDaily);
    }

    public static void SetNumberWatchDailyVideo(int number)
    {
        PlayerPrefs.SetInt("NumberWatchDailyVideo", number);
    }

    public static bool GetFreeSpin()
    {
        return PlayerPrefs.GetInt("FreeSpin", 1) > 0 ? true : false;
    }

    public static void SetFreeSpin(bool isFree)
    {
        int free = isFree ? 1 : 0;
        PlayerPrefs.SetInt("FreeSpin", free);
    }

    public static int GetNumberWatchVideoSpin()
    {
        return PlayerPrefs.GetInt("NumberWatchVideoSpin", 0);

    }

    public static void SetNumberWatchVideoSpin(int count)
    {
        PlayerPrefs.SetInt("NumberWatchVideoSpin", count);
    }

    public static string GetTimeLoginSpinFreeWheel()
    {
        return PlayerPrefs.GetString("TimeSpinFreeWheel", "");
    }

    public static void SetTimeLoginSpinFreeWheel(string time)
    {
        PlayerPrefs.SetString("TimeSpinFreeWheel", time);
    }

    public static string GetTimeLoginSpinVideo()
    {
        return PlayerPrefs.GetString("TimeLoginSpinVideo", "");
    }

    public static void SetTimeLoginSpinVideo(string time)
    {
        PlayerPrefs.SetString("TimeLoginSpinVideo", time);
    }

    public static void SetSoundSetting(bool isOn)
    {
        PlayerPrefs.SetInt(Helper.SoundSetting, isOn ? 1 : 0);
    }
    public static void SetVibration(bool isOn)
    {
        PlayerPrefs.SetInt(Helper.Vibration, isOn ? 1 : 0);
    }

    public static bool GetSoundSetting()
    {
        return PlayerPrefs.GetInt(Helper.SoundSetting, 1) == 1;
    }

    public static bool GetVibraviton()
    {
        return PlayerPrefs.GetInt(Helper.Vibration, 1) == 1;
    }

    public static void SetMusicSetting(bool isOn)
    {
        PlayerPrefs.SetInt(Helper.MusicSetting, isOn ? 1 : 0);
    }

    public static bool GetMusicSetting()
    {
        return PlayerPrefs.GetInt(Helper.MusicSetting, 1) == 1;

    }

    public static bool IsNoAds()
    {
        return PlayerPrefs.GetInt("NoAds", 0) == 1;
    }

    public static void SetNoAds()
    {
        PlayerPrefs.SetInt("NoAds", 1);
    }

    public static void SetNumberPlay(int num)
    {
        PlayerPrefs.SetInt("NumberPlay", num);
    }

    public static int GetNumberPlay()
    {
        return PlayerPrefs.GetInt("NumberPlay", 0);
    }

    public static string GetTimeLoginOpenGift()
    {
        return PlayerPrefs.GetString("TimeLoginOpenGift", "");
    }

    public static void SetTimeLoginOpenGift(string time)
    {
        PlayerPrefs.SetString("TimeLoginOpenGift", time);
    }
}
