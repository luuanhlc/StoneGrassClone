// #define FORCE_ROLE

using Firebase.Crashlytics;
using RocketTeam.Sdk.Services.Ads;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DG.Tweening;
using Sirenix.Serialization;
using Unicorn;
using UnityEngine;
//using UnityEngine.Rendering.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Cinemachine;
using MoreMountains.Feedbacks;

/***********************************************************


        ╭╮ ╭╮╭━╮ ╭╮╭━━╮╭━━━╮╭━━━╮╭━━━╮╭━╮ ╭╮
        ┃┃ ┃┃┃┃╰╮┃┃╰┫┣╯┃╭━╮┃┃╭━╮┃┃╭━╮┃┃┃╰╮┃┃
        ┃┃ ┃┃┃╭╮╰╯┃ ┃┃ ┃┃ ╰╯┃┃ ┃┃┃╰━╯┃┃╭╮╰╯┃
        ┃┃ ┃┃┃┃╰╮┃┃ ┃┃ ┃┃ ╭╮┃┃ ┃┃┃╭╮╭╯┃┃╰╮┃┃
        ┃╰━╯┃┃┃ ┃┃┃╭┫┣╮┃╰━╯┃┃╰━╯┃┃┃┃╰╮┃┃ ┃┃┃
        ╰━━━╯╰╯ ╰━╯╰━━╯╰━━━╯╰━━━╯╰╯╰━╯╰╯ ╰━╯
        ╭━━━━┳━━━╮╭━━━━┳╮ ╭┳━━━╮╭━━━━┳━━━┳━━━╮
        ┃╭╮╭╮┃╭━╮┃┃╭╮╭╮┃┃ ┃┃╭━━╯┃╭╮╭╮┃╭━╮┃╭━╮┃
        ╰╯┃┃╰┫┃ ┃┃╰╯┃┃╰┫╰━╯┃╰━━╮╰╯┃┃╰┫┃ ┃┃╰━╯┃
          ┃┃ ┃┃ ┃┃  ┃┃ ┃╭━╮┃╭━━╯  ┃┃ ┃┃ ┃┃╭━━╯
          ┃┃ ┃╰━╯┃  ┃┃ ┃┃ ┃┃╰━━╮  ┃┃ ┃╰━╯┃┃
          ╰╯ ╰━━━╯  ╰╯ ╰╯ ╰┻━━━╯  ╰╯ ╰━━━┻╯
                                            
                                                            
                                         .^^                
                                       ^?J~                 
                      :J5YJ?7~~!7?7.:7Y57.                  
                 .^7JY55P5YJ?J5GBP?YP5?:                    
             .~?5PPYJ?777!!77??7?YBBY~                      
          .!YGBGY?!!777!!!777777~??Y57                      
         ^?JP#GY?7777!!^~7777777~77?JPJ.                    
          :YGY7!777!^J5:!777777777???7JY!                   
         7BPYJ7777~:5#B~:~!!!!77777777!7YY!.                
        ?#GGG7777!.J#B#BY77777777!!!!777!7YP7.              
       !#BG#?!777:^B#BB###?!!!!!!!!7?7!7777YY5              
      .GP:YG7777!:~BBBBB#G          .!?!!!77YP              
      ^5. 5P7J77!:^B#BBB#B.           ^^~77!~^              
      ..  YGYG777^.J#BBBB#?                                 
          ~BG#Y!7!::5##BBB#?                   :.           
           Y###Y77!^:JB##B##P~               ^!:            
           .PPJBG?77~:~YB#####PJ~:.     .:^7J?.             
            .? :YGPJ7!~^^75GB####BGPP555PP5?:               
                 :75P5Y?7!~~!7?JY55555YY?~.                 
                    .~7JY5YYYJJJ????7!^.                    
                         ..:^^^^::.                                 

 ***********************************************************/


/// <summary>
/// Quản lý load scene và game state
/// </summary>
public class GameManager : SerializedMonoBehaviour
{
    PlayerController _pl;

    public static GameManager Instance;

    [Space] [SerializeField] private LevelConstraint levelConstraint;
    public int VSyncCount = 0;
    public int TargetFPS;
    public MMFeedbacks LoadingSenceOn;
    public MMFeedbacks LoadingSenceOff;
    public GameObject LoadingUi;
    public GameObject player;


#if UNITY_EDITOR
    private enum LevelLoadType
    {
        Normal,
        RepeatOneLevel,
        JumpToLevel
    }
    [SerializeField] private LevelLoadType levelLoadType;
    [HideIf(nameof(levelLoadType), LevelLoadType.Normal)]
    [SerializeField] private bool useBuildIndex;
    [HideIf("@levelLoadType == LevelLoadType.Normal || useBuildIndex")]
    [SerializeField] private LevelType forcedLevelType;
    [HideIf(nameof(levelLoadType), LevelLoadType.Normal)]
    [PropertyRange(1, "maxLevel")]
    [SerializeField] private int forcedLevel;

    private int maxLevel;

#endif

    [FoldoutGroup("Persistant Component", false)]
    [SerializeField] private UiController uiController;
    [FoldoutGroup("Persistant Component")]
    [SerializeField] private CameraController mainCamera;
    [FoldoutGroup("Persistant Component")]
    [SerializeField] private PlayerDataManager playerDataManager;
    [FoldoutGroup("Persistant Component")]
    [SerializeField] private IapController iap;
    
    private LevelManager currentLevelManager;
    
    private IDataLevel dataLevel;

    public event Action GamePaused;
    public event Action GameResumed;

    public bool _isIsland;

    public MeshRenderer basePlane;
    public bool IsLevelLoading { get; private set; }

    public ILevelInfo DataLevel => dataLevel;
    public int CurrentLevel => DataLevel.GetCurrentLevel();
    public GameFSM GameStateController { get; private set; }
    public PlayerDataManager PlayerDataManager => playerDataManager;
    public CameraController MainCamera => mainCamera;
    public UiController UiController => uiController;
    public LevelManager LevelManager
    {
        get => currentLevelManager;
        private set => currentLevelManager = value;
    }
    public IapController IapController => iap;
    public Profile Profile { get; private set; }

    private CinemachineVirtualCamera vcam;
    public int numberVideo;
    public ParticleSystem phaohoaFX;

    private void Awake()
    {
        Instance = this;
        GameStateController = new GameFSM(this);
        Profile = new Profile();
        QualitySettings.vSyncCount = VSyncCount;
        Application.targetFrameRate = TargetFPS;
        DOTween.Init().SetCapacity(200, 125);
#if !UNITY_EDITOR
       //Debug.unityLogger.logEnabled = false;
#endif

        dataLevel = PlayerDataManager.GetDataLevel(levelConstraint);
        dataLevel.LevelConstraint = levelConstraint;
    }

    private void Start()
    {
        _pl = PlayerController.Ins;
        Screen.SetResolution(720, 1480, true);

        AdManager.Instance.Init();
        UnicornAdManager.LoadBannerAds();
        //UiController.Init();
        LoadLevel();
        LoadScreen.Ins.OpenGame();
    }


    public void changeMaterial(Material planeMaterial)
    {
        var mat = basePlane.materials;
        mat[1] = planeMaterial;
        basePlane.materials = mat;
    }
    /// <summary>
    /// Load level mới và xóa level đang hiện hữu
    /// </summary>
    public void LoadLevel()
    {
        int buildIndex = dataLevel.GetBuildIndex();

#if UNITY_EDITOR
        buildIndex = GetForcedBuildIndex(buildIndex);
#endif

        bool isBuildIndexValid = buildIndex > gameObject.scene.buildIndex 
                                 && buildIndex < SceneManager.sceneCountInBuildSettings;
        if (!isBuildIndexValid)
        {
            Debug.LogError("No valid scene is found! \nFailed build index: " + buildIndex);
            GameStateController.ChangeState(GameState.LOBBY);
            return;
        }

        IsLevelLoading = true;
        if (CurrentLevel != 0 && SceneManager.sceneCount != 1)
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));
        SceneManager.LoadSceneAsync(PlayerDataManager.GetCrrLevel() + 1, LoadSceneMode.Additive);
        uiController.OpenLoading(true);
    }

    public void loadcount(int i)
    {
        StartCoroutine(load(UIMap.Ins.loadTime, i));
    }

    IEnumerator load(float time, int i)
    {
        yield return Yielders.Get(time);
        LoadLevelBtn(i + 1);
        UiController.Ins.UiInGame.gameObject.SetActive(false);
        CamController.Ins.LoobyCam.gameObject.SetActive(true);
        CamController.Ins.BaseCame.gameObject.SetActive(false);
        CamController.Ins.Room.SetActive(false);
        //UIMap.SetActive(false);
        UiController.Ins.UiInGame.ResetSlide();
        //UiController.Ins.UiInGame.gameObject.SetActive(true);
        IncreaseLevel();

        player.transform.position = new Vector3(-0.9f, -.2f, -1.5f);
        player.transform.rotation = Quaternion.Euler(0, 140.327f, 0);
        Profile.AddStar(InGame.Ins.pecentComplete);
        infor.Ins.tr.Clear();
        infor.Ins.tr2.Clear();
        infor.Ins.countProduct = 0;
        infor.Ins.speed = infor.Ins.originSpeed;
    }

    public void LoadLevelBtn(int buildIndex)
    {
        if(LobbyManager.Ins.weather != null)
            Destroy(LobbyManager.Ins.weather.gameObject);
        
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));
        SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive);
        UiController.Ins.UiInGame.gameObject.SetActive(true);
    }

#if UNITY_EDITOR
    private int GetForcedBuildIndex(int buildIndex)
    {
        if (levelLoadType != LevelLoadType.Normal)
        {
            if (useBuildIndex)
            {
                dataLevel.SetLevel(forcedLevel);
                buildIndex = forcedLevel;
            }
            else
            {
                dataLevel.SetLevel(forcedLevelType, forcedLevel);
                forcedLevelType = dataLevel.LevelType;
                forcedLevel = dataLevel.GetCurrentLevel();
                buildIndex = dataLevel.GetBuildIndex();
            }

            if (levelLoadType == LevelLoadType.JumpToLevel)
            {
                levelLoadType = LevelLoadType.Normal;
            }
        }

        return buildIndex;
    }
#endif


    /// <summary>
    /// Đưa game về state Lobby và khởi tạo lại các giá trị cần thiết cho mỗi level mới.
    /// <remarks>
    /// LevelManager ở mỗi scene khi được load sẽ gọi hàm này.
    /// </remarks>
    /// </summary>
    /// <param name="levelManager"></param>
    public void RegisterLevelManager(LevelManager levelManager)
    {
        LevelManager = levelManager;
        GameStateController.ChangeState(GameState.LOBBY);
        uiController.OpenLoading(false);
        IsLevelLoading = false;
    }

    GameObject pl;
    public void StartLevel()
    {
        Analytics.LogTapToPlay();
        GameStateController.ChangeState(GameState.IN_GAME);
        uiController.OpenUI(2);
    }

    public void DelayedEndgame(LevelResult result, float delayTime = 0.5f)
    {
        StartCoroutine(DelayedEndgameCoroutine(result, delayTime));
    }

    private IEnumerator DelayedEndgameCoroutine(LevelResult result, float delayTime)
    {
        yield return Yielders.Get(delayTime);
        EndLevel(result);
    }

    public void EndLevel(LevelResult result)
    {
        GameStateController.ChangeState(GameState.END_GAME);

        if (result == LevelResult.Win)
        {
            IncreaseLevel();
        }
    }

    public void IncreaseLevel()
    {
        dataLevel.IncreaseLevel();
    }

    public void Revive()
    {
        LevelManager.ResetLevelState();
        // TODO: Revive code
    }

    public void Pause()
    {
        Time.timeScale = 0;
        GamePaused?.Invoke();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        GameResumed?.Invoke();
    }

    private void Update()
    {
        if (currentLevelManager)
            GameStateController.Update();
    }

    private void FixedUpdate()
    {
        if (currentLevelManager)
            GameStateController.FixedUpdate();
        if((int)Time.time % 40 == 0 && (int)Time.time != 0)
            return;
            //UnicornAdManager.ShowInterstitial("in_game");
    }

    private void LateUpdate()
    {
        if (currentLevelManager)
            GameStateController.LateUpdate();
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        if (levelLoadType == LevelLoadType.Normal)
        {
            return;
        }
        
        int levelCount = 0;
        if (useBuildIndex)
        {
            levelCount = levelConstraint.GetLevelCount();
        }
        else 
        {
            levelCount = levelConstraint.GetLevelCount(forcedLevelType);
        }

        maxLevel = levelCount;
        forcedLevel = Mathf.Clamp(forcedLevel, 1, maxLevel);
    }
#endif
}
