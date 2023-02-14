using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unicorn;
using MoreMountains.Tools;

public class btnSkinPlayer : MonoBehaviour
{
    [SerializeField] private TypeEquipment typeEquipment;

    DataSkin data;
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
                GameManager.Instance.Profile.AddDimond(-data.price);
                UiController.Ins.UiTop.UpdateUi(0);
                data.type = TypePay.USE;
                RenBtnSkin(data);
                break;
            case TypePay.USE:
                ShopSkin.Ins._dataSkin = data;
                PlayerDataManager.SetDataPlayerPreView(-1);
                changerPlayer.Ins.ChangerCharacter(data.idItem);
                PlayerDataManager.SetPlayerCurrent(data.idItem);
                PlayerSkin.Ins.ResetBtn();

                break;

//        UnicornAdManager.ShowInterstitial("end_game_lose");

        }
    }
    private void OnClickPreView()
    {
        if (data.type == TypePay.USE)
        {
            OnClickBuy();
            return;
        }
        PlayerDataManager.SetDataPlayerPreView(data.idItem);
        changerPlayer.Ins.ChangerCharacter(data.idItem);

        PlayerSkin.Ins.ResetBtn();

    }

    private void OnRewardedVideo(int x)
    {
        GameManager.Instance.numberVideo++;
        GameManager.Instance.Profile.AddGold(-data.price);
        UiController.Ins.UiTop.UpdateUi(0);
        data.type = TypePay.USE;
        RenBtnSkin(data);
    }

    public void RenBtnSkin(DataSkin data)
    {
        this.data = data;
        this.img.sprite = data._itemIcon;
        
        if (data.type == TypePay.USE)
        {
            if (data.idItem == PlayerDataManager.GetPlayerCurrent())
            {
                ShopSkin.Ins._dataSkin = data;
                btnBuy.enabled = false;
                Pay.GetComponent<Image>().sprite = usedBtn;
                txt.text = "Used";
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

        if (PlayerDataManager.GetDataPlayerPreView() == data.idItem)
        {
            background.sprite = spritePreView;
            return;
        }
        background.sprite = spriteUnAciveBg;
    }
}

