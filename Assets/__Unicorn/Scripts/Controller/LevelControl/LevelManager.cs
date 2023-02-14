using System;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using Random = UnityEngine.Random;



public abstract class LevelManager : MonoBehaviour
{
    private static LevelManager instance;

    [BoxGroup("UI")] [SerializeField] private UiWin uiWin;

    private List<int> numbers;

    public static LevelManager Instance => instance;

    public LevelResult Result { get; set; }

    public string PlacementRevive = "video_reward_";
    public UiWin UIWin => uiWin;

    protected virtual void Awake()
    {
        instance = this;
    }

    protected virtual void Start()
    {
        //Debug.Log(this.gameObject.name);
        SetUpLevelEnvironment();
        GameManager.Instance.RegisterLevelManager(this);
    }
    private void Update()
    {
        if(infor.Ins.gold == 10000)
        {
            EndLevel();
        }
    }
    protected virtual void SetUpLevelEnvironment()
    {
        ResetLevelState();
    }

    public virtual void ResetLevelState()
    {
        Result = LevelResult.NotDecided;
    }

    public virtual void OpenUpdateUI() {
        GameManager.Instance.UiController.OpenLuckyWheel();
    }

    public abstract void StartLevel();

    public abstract void EndLevel();

    protected virtual void EndGame(LevelResult levelResult)
    {
        if (Result != LevelResult.NotDecided)
        {
            Debug.LogError($"Level has already ended with result ${Result} but another request for result ${levelResult} is still being sent!");
            return;
        }
        
        Result = levelResult;
        GameManager.Instance.DelayedEndgame(levelResult);
    }

    public bool IsRevivable()
    {
        return true;
    }
}
