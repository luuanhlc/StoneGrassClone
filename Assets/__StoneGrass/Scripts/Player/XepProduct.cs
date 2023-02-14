using System.Collections;
using UnityEngine;

public class XepProduct : MonoBehaviour
{
    vanOfTrain _vantrain;
    public GameObject _nextpos;
    [HideInInspector] public Vector3 fristPos = new Vector3(-0.4f, 0.3f, .1f);
    [HideInInspector] public Vector3 LocalNextPos;
    [HideInInspector] public int numberofProduct = 0; // so luong product da lay
    [HideInInspector] public int totalProductTaked;
    [HideInInspector] public int Succhua;

    [HideInInspector] public int nx = 0;
    [HideInInspector] public int ny = 0;
    [HideInInspector] public int nz = 0;

    [HideInInspector] private int maxX = 4;
    [HideInInspector] private int maxZ = 2;

    public GameObject[] products;

    public GameObject[,] dObject = new GameObject[5, 3];

    public GameObject van;
    public GameObject truck;

    [SerializeField] private int soTang;

    public bool _selling = false;
    public GameObject sellPos;
    productMove pd;

    public GameObject recbin;


    #region Singleton
    public static XepProduct Isn;

    private void Awake()
    {
        Isn = this;
        for (int i = 0; i < 5; i++)
        {
            int a = i + 1;
            for (int j = 0; j < 3; j++)
            {
                int b = j + 1;
                dObject[i,j] = GameObject.Find(a + "" + b);
            }
        }
    }
    #endregion

    
    public void Reset()
    {
        products = new GameObject[1000];
        maxX = 4;
        maxZ = 2;
        nx = 0;
        ny = 0;
        nz = 0;
        totalProductTaked = 0;
        numberofProduct = 0;
        LocalNextPos = fristPos;
    }


    private void Start()
    {
        _vantrain = vanOfTrain.Isn;
        updateSucChua();
        products = new GameObject[1000];
        LocalNextPos = fristPos;

        _nextpos.transform.localPosition = LocalNextPos;
    }

    public void updateSucChua()
    {
        soTang = PlayerDataManager.GetPlayerStorage();
        //Debug.Log(soTang);
        Succhua = 15 * soTang;
    }

    public void taked(GameObject s)
    {
        if (numberofProduct == Succhua)
        {
            return;
        }
        float h = ny + 1f;
        scaleOb(h);
        products[numberofProduct] = s;
        totalProductTaked++;
        InGame.Ins.updateStrong(); // cap nhat slider suc chua
    }

    void scaleOb(float h)
    {
        dObject[nx, nz].transform.localScale = new Vector3(.4f, 0.2f * h, .2f);
        dObject[nx, nz].transform.localPosition = new Vector3(dObject[nx, nz].transform.localPosition.x, h * .1f + .2f, dObject[nx, nz].transform.localPosition.z);
    }

    bool changer = false;
    public float timeIn;
    private void m(int i, string name, GameObject other)
    {
        if (i < 0)
            return;
        if (changer)
        {
            changer = false;
        }
        if (name != "FillCau")
        {
            
            GameManager.Instance.Profile.AddGold((int)products[i].GetComponent<inforProduct>().vaule);
            if (PlayerController.Ins.iconmoney != null && Time.time >= timeIn)
            {
                timeIn = Time.time + .2f;
                GameObject ob = PlayerController.Ins.iconmoney[0].gameObject;
                PlayerController.Ins.iconmoney.Remove(ob);
                ob.transform.position = Camera.main.WorldToScreenPoint(sellPos.transform.position + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0));
                ob.gameObject.SetActive(true);
                moneyFly money = ob.GetComponent<moneyFly>();
                money.text.SetText(products[i].GetComponent<inforProduct>().vaule.ToString() + " <sprite name=money>");
                money.RunAnim();
            }
            UiTop.Ins.UpdateUi(0);
        }
        LocalNextPos = products[i].transform.localPosition;
        pd = products[i].GetComponent<productMove>();
        pd.selling = true;
        pd.take = true;

        pd.a = products[i].transform.position;
        pd.g = other.transform.Find("SellPos").transform;
        pd.t = 0;

        products[i].transform.SetParent(recbin.transform);
        products[i].SetActive(true);
        
        if (name == "SellFar")
        {
            _vantrain.takedN += 1;
            _vantrain.scaleOb();
        }
        if (name == "FillCau")
        {
            FillCau.Isn.takedN += 1;
            FillCau.Isn.scaleOb();
        }

        products[i] = null;
        numberofProduct--;
        InGame.Ins.updateStrong();
        if (nx > 0)
        {
            if (nz > 0)
                nz--;
            else
            {
                nx--;
                nz = maxZ;
            }
        }
        else if (nx == 0)
        {
            if (nz > 0)
                nz--;
            else if (ny >= 0)
            {
                ny--;
                nx = maxX;
                nz = maxZ;
            }
        }
        if (ny == 0)
            scaleOb(0f);
        else
            scaleOb(ny);
    }

    public bool _fillBrigde;

    IEnumerator go(float time, string name, GameObject other)
    {
        VanFlash.Ins.SetActive(false);
        switch (name)
        {
            case "SellBuilding":
                while (numberofProduct > 0)
                {
                    yield return Yielders.Get(time);
                    if (_selling)
                        m(numberofProduct - 1, name, other);
                }
                break;
            case "FillCau":
                while (numberofProduct > 0)
                {
                    yield return Yielders.Get(time);
                    if (FillCau.Isn.SucChua <= FillCau.Isn.takedN)
                    {
                        if(FillCau.Isn != null)
                        {
                            FillCau.Isn.wall.SetActive(false);
                            FillCau.Isn._Full = true;
                            UiController.Ins.popReturnHome.SetEnabled(true);
                        }
                    }
                    else if (_fillBrigde && FillCau.Isn.SucChua > FillCau.Isn.takedN)
                        m(numberofProduct - 1, name, other);
                   
                }
                break;
        }
       
    }

    public void sell(Collider other)
    {
        if (numberofProduct <= 0)
            return;
        if (other.name == "SellFar")
        {
            VanFlash.Ins.SetActive(false);
            GameObject train = GameObject.Find("Train1");
            _vantrain = train.GetComponent<vanOfTrain>();
            if (!train.GetComponent<XeDay>()._isSellAble)
                return;
        }
        StartCoroutine(go(.008f, other.name, other.gameObject));
    }
}
