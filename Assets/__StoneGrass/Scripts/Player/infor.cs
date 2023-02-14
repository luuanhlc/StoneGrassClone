using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
public class infor : MonoBehaviour
{
    XeDay train;
    vanOfTrain _trainvan;
    XepProduct set;
    public TrailRenderer tr;
    public TrailRenderer tr2;
    [Header("Thong so co ban")]
    public int Dame = 50;
    public int SoCua = 3;

    [HideInInspector] public int originSpeed;
    [HideInInspector] public int speed;
    public float sawSpeed;

    public int dimond = 100;
    public int gold = 0;
    public int star;

    public List<GameObject> nSaws;

    public static bool _cutgrass;

    public bool needcut;
    [HideInInspector] public string gn;

    private GameObject[] spawsPos = new GameObject[5];

    public bool NamChamEffect = false;

    public DataTruck data;

    public static infor Ins;
    GameObject trainoj;
    GameObject fillCau;

    FillCau _fillCau;
    private void Awake()
    {
        Ins = this;
        updateSpeed();
    }
    public void updateSpeed()
    {
        originSpeed = PlayerDataManager.GetPlayerWhell()/8 + 5;
    }
    private void Start()
    {
        trainoj = GameObject.Find("Train1");
        if (trainoj != null)
        {
            train = trainoj.GetComponent<XeDay>();
            _trainvan = trainoj.GetComponent<vanOfTrain>();
        }
       

        set = XepProduct.Isn;
        speed = originSpeed;

        changerSaw();
        int index = PlayerDataManager.GetTruckIndex();
        int wheels = PlayerDataManager.GetPlayerWhell();
        ChangerSkin.Ins.Change(data.dataTruck[index].idItem, data.dataTruck[index].body, data.dataTruck[index].bodyScale, data.dataTruck[index].bodyMaterial, data.dataTruck[index].whellList[wheels].wheel, data.dataTruck[index].whellList[wheels].mtr);
        Invoke("Anhxa", 1);
    }

    bool isRequest;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Sell"))
            return;
        set._selling = true;
        if (other.name == "SellFar" && _trainvan.takedN < _trainvan.SucChua && train._isSellAble)
        {
            if (!train._isActive)
                return;
            else
                set.sell(other);
        }
        else if(other.name == "SellBuilding")
        {
            set.sell(other);
        }
        fillCau = LobbyManager.Ins.fillcau;
        if (fillCau == null)
            return;

        _fillCau = fillCau.GetComponentInChildren<FillCau>();
        if (other.name == "FillCau" && _fillCau.SucChua > _fillCau.takedN)
        {
            set._fillBrigde = true;
            set.sell(other);
        }
    }

    public void ChangerSawScale(float size)
    {
        for (int i = 0; i < SoCua; i++)
            spawsPos[i].GetComponent<freeRotate>().ChangerSize(size);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sell"))
        {
            set._selling = false;
            set._fillBrigde = false;
            return;   
        }
    }
    string crrSocua;
    public void changerSaw()
    {
        SoCua = (int)PlayerDataManager.GetNumberOfPongs();
        for(int i = 0; i < nSaws.Count; i++)
        {
            if (nSaws[i].name == SoCua + "saw")
            {
                nSaws[i].SetActive(true);
                if(crrSocua != nSaws[i].name)
                    MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins.powerUp, MMSoundManager.MMSoundManagerTracks.UI, Camera.main.gameObject.transform.position);

                crrSocua = nSaws[i].name;
            }
            else
                nSaws[i].SetActive(false);
        }
        for (int i = 0; i < SoCua; i++)
            spawsPos[i] = GameObject.Find("saw" + i);
    }

    private void Update()
    {
        if (GameManager.Instance._isIsland)
            return;
        if (!needcut)
            speed = originSpeed;
    }
    PoolingObject poolObj;
    void Anhxa()
    {
        GameObject pool = GameObject.Find("Pool");
        if (pool != null)
            poolObj = pool.GetComponent<PoolingObject>();
    }

    public int countProduct;
}
