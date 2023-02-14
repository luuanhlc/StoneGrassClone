using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unicorn;
using MoreMountains.Tools;
using DG.Tweening;

public class btnSkinLayout : MonoBehaviour
{
    [SerializeField] private TypeEquipment typeEquipment;

    DataTruckItem data;
    public GameObject _truckSkin;
    public Image img;
    public Button btnBuy;
    public TextMeshProUGUI txt;
    public Image background;
    public Sprite spriteactiveBg;
    public Sprite spriteUnAciveBg;
    public Sprite spritePreView;

    public Sprite usedBtn;
    public Sprite useBtn;

    public Image ad;

    public Button Pay;
    public Button btnAd;
    public Button btnPreView;

    private void Start()
    {
        Pay.onClick.AddListener(OnClickBuy);
        btnBuy.onClick.AddListener(OnClickBuy);
        btnAd.onClick.AddListener(OnClickAd);
        btnPreView.onClick.AddListener(OnClickPreView);
    }
    void OnClickAd()
    {
        UnicornAdManager.ShowAdsReward(OnRewardedVideo, string.Format(Helper.video_shop_general, typeEquipment));
    }
    private void OnClickBuy()
    {
        MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins._btnClickSound, MMSoundManager.MMSoundManagerTracks.UI, Camera.main.gameObject.transform.position);
        switch (data.type)
        {
            case TypePay.Pay:
                GameManager.Instance.Profile.AddGold(-data.price);
                UiController.Ins.UiTop.UpdateUi(0);
                data.type = TypePay.USE;
                RenBtnSkin(data);
                break;
            case TypePay.USE:
                PlayerDataManager.SetTruckIndex(data.idItem);
                ShopSkin.Ins.data = data;
                ChangerSkin.Ins.Change(data.idItem ,data.body, data.bodyScale, data.bodyMaterial, data.whellList[PlayerDataManager.GetPlayerWhell()].wheel, data.whellList[PlayerDataManager.GetPlayerWhell()].mtr);
                PlayerDataManager.SetDataTruckPreView(-1);
                TruckSkin.Ins.ResetBtn();

                break;
        }
    }

    private void OnClickPreView()
    {
        if(data.type == TypePay.USE)
        {
            OnClickBuy();
            return;
        }
        PlayerDataManager.SetDataTruckPreView(data.idItem);
        ChangerSkin.Ins.Change(data.idItem, data.body, data.bodyScale, data.bodyMaterial, data.whellList[PlayerDataManager.GetPlayerWhell()].wheel, data.whellList[PlayerDataManager.GetPlayerWhell()].mtr);
        TruckSkin.Ins.ResetBtn();
    }

    private void OnRewardedVideo(int x)
    {
        GameManager.Instance.numberVideo++;
        GameManager.Instance.Profile.AddGold(-data.price);
        UiController.Ins.UiTop.UpdateUi(0);
        data.type = TypePay.USE;
        RenBtnSkin(data);
    }

    public void RenBtnSkin(DataTruckItem data)
    {
        this.data = data;
        this.img.sprite = data._itemIcon;
        float aspectRatio = img.GetComponent<RectTransform>().rect.width / img.GetComponent<RectTransform>().rect.height;
        var fitter = img.GetComponent<UnityEngine.UI.AspectRatioFitter>();
        fitter.aspectRatio = aspectRatio;
        /*GameObject _truck3D = Instantiate(data._truckSkin, _canvas3D.parentTransform.gameObject.transform);
        _canvas3D.rendererToScale = new Renderer[3];
        for(int i  = 0; i < 3; i++)
        {
            _canvas3D.rendererToScale[i] = _truck3D.transform.GetChild(i).GetComponent<MeshRenderer>();
        }*/
        if (data.type == TypePay.USE)
        {
            if(data.idItem == PlayerDataManager.GetTruckIndex())
            {
                ShopSkin.Ins.data = data;
                btnBuy.enabled = false;
                txt.text = "Used";
                Pay.GetComponent<Image>().sprite = usedBtn;
                background.sprite = spriteactiveBg;

            }
            else
            {
                btnBuy.enabled = true;
                txt.text = "Use";
                Pay.GetComponent<Image>().sprite = useBtn;
                background.sprite = spriteUnAciveBg;

            }
            btnAd.gameObject.SetActive(false);
            return;
        }
        if (data.price > PlayerDataManager.GetDimond())
        {
            btnBuy.enabled = true;
            data.type = TypePay.VIDEO;
            //txt.gameObject.SetActive(false);
            txt.text = ConverInt.Sign(data.price) + " <sprite name=dimond>";


        }
        else
        {
            btnBuy.enabled = true;
            data.type = TypePay.Pay;
            txt.gameObject.SetActive(true);
            txt.text = ConverInt.Sign(data.price) + " <sprite name=dimond>";
        }


        if(PlayerDataManager.GetDataTruckPreView() == data.idItem)
        {
            background.sprite = spritePreView;
            return;
        }
        background.sprite = spriteUnAciveBg;
    }
}

