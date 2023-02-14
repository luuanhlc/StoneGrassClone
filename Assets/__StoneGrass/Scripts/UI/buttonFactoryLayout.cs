using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unicorn;
using MoreMountains.Tools;

public class buttonFactoryLayout : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI tbPrice;
    [SerializeField] private TextMeshProUGUI tbSoldPrice;
    [SerializeField] private Image own;
    private TypePay type;
    [SerializeField] private TypeEquipment typeEquipment;

    public Image spriteBuy;

    [SerializeField] Button btnItem;

    int id;
    int price;

    private void Start()
    {
        btnItem.onClick.AddListener(OnClickBuyItem);
    }

    private void OnClickBuyItem()
    {
        MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins._btnClickSound, MMSoundManager.MMSoundManagerTracks.UI, Camera.main.gameObject.transform.position);
        switch (type)
        {
            case TypePay.VIDEO:
                UnicornAdManager.ShowAdsReward(OnRewardedVideo, string.Format(Helper.video_shop_general, typeEquipment));
                break;
            case TypePay.Pay:
                GameManager.Instance.Profile.AddDimond(-price);
                OnRewardedVideo(1);
                UiTop.Ins.UpdateUi(1);
                break;
        }
        btnItem.enabled = false;
    }

   public Image ad;
    public void SetBtn()
    {
        btnItem.enabled = true;
        if(PlayerDataManager.getItemsOwn(id) == 1)
        {
            ad.gameObject.SetActive(false);

            spriteBuy.sprite = UiTop.Ins.take;

            return;
        }

        if (PlayerDataManager.GetDimond() < price)
        {
            tbPrice.gameObject.SetActive(false);
            if(_own == 0)
                ad.gameObject.SetActive(true);
            type = TypePay.VIDEO;

            spriteBuy.sprite = UiTop.Ins.videoBtn;
        }
        else
        {
            tbPrice.gameObject.SetActive(true);
            spriteBuy.sprite = UiTop.Ins.dimondBtn;

            if (id == 0)
            {
                type = TypePay.Pay;
                tbSoldPrice.text = _soldPrice.ToString() + " <sprite name=money>/ item";
                return;
            }
            if (PlayerDataManager.getItemsOwn(id - 1) == 1)
                btnItem.enabled = true;
            else
                btnItem.enabled = false;
        }
    }

    private void OnRewardedVideo(int x)
    {
        GameManager.Instance.numberVideo++;

        btnItem.enabled = false;

        tbPrice.gameObject.SetActive(false);
        own.gameObject.SetActive(true);
        PlayerDataManager.setItemsOwn(id, 1);
        ProductFactory.Ins.ResetBtnList();
        ProductFactory.Ins.SetImg(id);
        if(UpdateFactory.ins != null)
            UpdateFactory.ins.ResetButton();
        ad.gameObject.SetActive(false);
    }
    int _soldPrice;
    int _own;
    public void RenBtn(int _id, Sprite _sprite, int _price, int _soldPrice, int _own)
    {
        id = _id;
        price = _price;
        _itemImage.sprite = _sprite;
        tbPrice.text = ConverInt.Sign(_price) + " <sprite name=dimond> ";
        this._own = _own;
        this._soldPrice = _soldPrice;
        tbSoldPrice.text = _soldPrice.ToString() + " <sprite name=money> / item";
        if (_own == 1)
        {
            own.gameObject.SetActive(true);
            ad.gameObject.SetActive(false);

            tbPrice.gameObject.SetActive(false);
        }
    }    
}
