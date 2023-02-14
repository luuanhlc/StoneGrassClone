using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;
public class levelSelect : MonoBehaviour
{

    public MMFeedbacks LoadSenceFeedBacks;

    private bool unlocked;
    
    public Sprite completeStar;
    public Image unlockImage;
    public GameObject[] star;
    public GameObject MapUi;
    public static levelSelect Ins;

    [SerializeField] private List<GameObject> van;
    [SerializeField] private List<Vector3> pos = new List<Vector3>();
    public UIOutline _uiOutline;

    private Image land;
    public Sprite UnLockLand;
    public PathMap pathmap;
    private void Awake()
    {
        Ins = this;
        land = this.gameObject.GetComponent<Image>();
        UpdateLevelStatus();
        
    }
    private void Start()
    {
        for (int i = 0; i < van.Count; i++)
        {
            pos.Add(van[i].transform.position);
        }
    }

    public void UpdateLevelStatus()
    {
        int i = PlayerDataManager.GetMaxLevel();
        if (PlayerDataManager.GetStar(Helper.Stats + "Level" + (int.Parse(this.gameObject.name) - 1).ToString()) >= 1)
            i += 1;
        if(i >= int.Parse(this.gameObject.name))
        {
            unlocked = true;
        }
        UpdateLeveImage();
    }
    bool _isHere;
    public void UpdateLeveImage()
    {
        string name = "Level" + this.gameObject.name;
        if (!unlocked)
        {
            unlockImage.gameObject.SetActive(true);
            this.gameObject.GetComponent<Button>().enabled = false;
            for (int i = 0; i < star.Length; i++)
            {
                star[i].gameObject.SetActive(false);
            }
        }
        else
        {
            if(int.Parse(this.gameObject.name) <= PlayerDataManager.GetMaxLevel())
            {
                if (pathmap != null)
                    pathmap.Unlocked();
                else
                    Load();
            }
            else if(pathmap != null)
            {
                StartCoroutine(pathmap.UnLock());
            }
        }
        if(name == SceneManager.GetSceneAt(1).name.ToString())
        {
            _uiOutline.enabled = true;
            _isHere = true;
            return;
        }
        _isHere = false;
        _uiOutline.enabled = false;

    }

    public void Load()
    {
        land.sprite = UnLockLand;
        unlockImage.gameObject.SetActive(false);
        int j = PlayerDataManager.GetStar(Helper.Stats + "Level" + this.gameObject.name.ToString());
        this.gameObject.GetComponent<Button>().enabled = true;
        for (int i = 0; i < star.Length; i++)
            star[i].gameObject.SetActive(true);
        for (int i = 0; i < j; i++)
            star[i].GetComponent<Image>().sprite = completeStar;
    }
    private void Update()
    {
        if (!_isHere)
            return;
    }
    Vector3 origin = new Vector3(0.4f, 0, .2f);
    public void Onclick()
    {
        string name = "Level" + this.gameObject.name;

        if (name == SceneManager.GetSceneAt(1).name.ToString())
        {
            UiController.Ins.UiMap.OnClickClose();
            return;
        }
        if(this.gameObject.name == 2.ToString())
        {
            UIMap.Ins._tut.nextStep();
        }
        PlayerController.Ins._isInputAble = true;
        StartCoroutine(Items.Ins.coutdown(0f));
        PlayerDataManager.SetCrrLevel(int.Parse(this.gameObject.name));
        PlayerDataManager.SetMaxLevel(int.Parse(this.gameObject.name));
        InGame.Ins._ani.SetBool("_out", true);
        UiController.Ins.UiMap.Show(false);
        UIMap.Ins.FlyToIsLandFeedBack.PlayFeedbacks();
        MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins._btnClickSound, MMSoundManager.MMSoundManagerTracks.UI, Camera.main.gameObject.transform.position);
        for(int i = 0; i < InGame.Ins.star.Count; i++)
        {
            InGame.Ins.star[i].GetComponent<completeEffect>().ran = false;
            InGame.Ins.star[i].GetComponent<completeEffect>()._Reset();
        }
        GameManager.Instance.loadcount(int.Parse(this.gameObject.name));
        UiController.Ins.UiInGame.Show(false);
    }
}

