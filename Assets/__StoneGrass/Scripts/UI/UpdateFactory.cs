using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using System.Collections.Generic;
using Unicorn;

public class UpdateFactory : MonoBehaviour
{
    [SerializeField] private Image WorkSpeedSl;
    [SerializeField] private Image StorageSl;
    [SerializeField] private Image QualitySl;

    [SerializeField] private Button btnWorkSpeed;
    [SerializeField] private Button btnStorage;
    [SerializeField] private Button btnQuality;

    [SerializeField] private TextMeshProUGUI tvWorkSpeed;
    public Image moneyIconWorkSpeed;
    [SerializeField] private TextMeshProUGUI tvStorage;
    [SerializeField] private TextMeshProUGUI tvQuality;

    [SerializeField] private int maxSpeed;
    [SerializeField] private int maxStorage;
    [SerializeField] private int maxQuality;

    private int priceWorkSpeed;
    private int priceStorage;
    private int priceQuality;

    [SerializeField] MMFeedbacks worSpeed;
    [SerializeField] MMFeedbacks storage;
    [SerializeField] MMFeedbacks quality;

    public static UpdateFactory ins;

    private TypePay StrorageType;
    private TypePay WorkSpeedType;
    private TypePay QualityType;

    [SerializeField] private TypeEquipment typeEquipment;

    public List<GameObject> btnPrice;
    public List<GameObject> btnAd;
    private void Awake()
    {
        ins = this;
    }
    private void Start()
    {
        btnWorkSpeed.onClick.AddListener(WorkSpeedClick);
        btnStorage.onClick.AddListener(StorageClick);
        btnQuality.onClick.AddListener(QualityClick);

        ResetSlide();
    }

    private void WorkSpeedClick()
    {
        switch (WorkSpeedType)
        {
            case TypePay.Pay:
                GameManager.Instance.Profile.AddGold(-priceWorkSpeed);
                break;
            case TypePay.VIDEO:
                UnicornAdManager.ShowAdsReward(OnRewardedVideo, string.Format(Helper.video_shop_general, typeEquipment));
                break;
        }
        PlayerDataManager.SetWorkSpeed((float)Mathf.Min(maxSpeed, PlayerDataManager.GetWorkSpeed() + (float)maxSpeed / 80f));
        worSpeed.PlayFeedbacks();
        PlaySound();
        ResetSlide();
    }

    private void StorageClick()
    {
        switch (StrorageType)
        {
            case TypePay.Pay:
                GameManager.Instance.Profile.AddGold(-priceStorage);
                break;
            case TypePay.VIDEO:
                UnicornAdManager.ShowAdsReward(OnRewardedVideo, string.Format(Helper.video_shop_general, typeEquipment));
                break;
        }
        PlayerDataManager.SetStorage((int)Mathf.Min(maxStorage, (int)(PlayerDataManager.GetStorage() + maxStorage / 25)));
        storage.PlayFeedbacks();
        PlaySound();
        ResetSlide();
    }

    private void QualityClick()
    {
        switch (QualityType)
        {
            case TypePay.Pay:
                GameManager.Instance.Profile.AddGold(-priceQuality);
                break;
            case TypePay.VIDEO:
                UnicornAdManager.ShowAdsReward(OnRewardedVideo, string.Format(Helper.video_shop_general, typeEquipment));
                break;
        }
        PlayerDataManager.SetQuality((float)Mathf.Min(maxQuality, PlayerDataManager.GetQuality() + (float)maxQuality / 15f));
        quality.PlayFeedbacks();
        PlaySound();
        ResetSlide();
    }
    private void OnRewardedVideo(int x)
    {
        GameManager.Instance.numberVideo++;
    }
    private void ResetSlide()
    {
        WorkSpeedSl.fillAmount = PlayerDataManager.GetWorkSpeed() / (float)maxSpeed;
        QualitySl.fillAmount = PlayerDataManager.GetQuality() / (float)maxQuality;
        StorageSl.fillAmount = PlayerDataManager.GetStorage() / (float)maxStorage;

        priceStorage = PlayerDataManager.GetStorage() * 30 + 100 * ( 1 + PlayerDataManager.GetStorage()) / 2;
        priceQuality = (int)(PlayerDataManager.GetQuality() * 30 + 100 * ( 1 + PlayerDataManager.GetQuality())/ 2);
        priceWorkSpeed = (int)(PlayerDataManager.GetWorkSpeed() * 30 + 100 * ( 1 + PlayerDataManager.GetWorkSpeed()) / 2) + 2000;


        UiTop.Ins.UpdateUi(0);
        ResetButton();
    }

    public void ResetButton()
    {
        UiController.Ins.UiTop.UpdateUi(0);
        int ownGold = PlayerDataManager.GetGold();
        //work speed
        if (WorkSpeedSl.fillAmount == 1f)
        {
            tvWorkSpeed.text = "Max";
            btnWorkSpeed.enabled = false;

        }
        else if(PlayerDataManager.getItemsOwn(0) == 0)
        {
            btnWorkSpeed.enabled = false;
            tvWorkSpeed.text = "Nedd A Product";
        }
        else
        {
            tvWorkSpeed.text = ConverInt.Sign(priceWorkSpeed) + " <sprite name=money>";
            if (PlayerDataManager.GetGold() < priceWorkSpeed)
            {
                btnWorkSpeed.gameObject.GetComponent<Image>().sprite = UiTop.Ins.videoBtn;
                btnPrice[0].SetActive(false);
                btnAd[0].SetActive(true);
                WorkSpeedType = TypePay.VIDEO;
                btnWorkSpeed.enabled = true;
            }
            else
            {
                btnWorkSpeed.gameObject.GetComponent<Image>().sprite = UiTop.Ins.payBtn;
                btnPrice[0].SetActive(true);
                btnAd[0].SetActive(false);
                WorkSpeedType = TypePay.Pay;
                btnWorkSpeed.enabled = true;
            }
        }


        //quality
        if (QualitySl.fillAmount == 1f)
        {
            tvQuality.text = "Max";
            btnQuality.enabled = false;
            btnQuality.gameObject.GetComponent<Image>().sprite = UiTop.Ins.take;

        }
        else
        {
            tvQuality.text = ConverInt.Sign(priceQuality) + " <sprite name=money>";
            if (PlayerDataManager.GetGold() < priceQuality)
            {
                btnPrice[1].SetActive(false);
                btnAd[1].SetActive(true);
                QualityType = TypePay.VIDEO;
                btnQuality.enabled = true;
                btnQuality.gameObject.GetComponent<Image>().sprite = UiTop.Ins.videoBtn;
            }
            else
            {
                btnPrice[1].SetActive(true);
                btnAd[1].SetActive(false);
                QualityType = TypePay.Pay;
                btnQuality.enabled = true;
                btnQuality.gameObject.GetComponent<Image>().sprite = UiTop.Ins.payBtn;


            }
        }

        //storage
        if (StorageSl.fillAmount == 1f)
        {
            tvStorage.text = "Max";
            btnStorage.enabled = false;
            btnStorage.gameObject.GetComponent<Image>().sprite = UiTop.Ins.take;
        }
        else
        {
            Debug.Log(priceStorage);
            tvStorage.text =ConverInt.Sign(priceStorage) + " <sprite name=money>";
            if (PlayerDataManager.GetGold() < priceStorage)
            {
                btnStorage.gameObject.GetComponent<Image>().sprite = UiTop.Ins.videoBtn;

                btnStorage.enabled = true;
                btnPrice[2].SetActive(false);
                btnAd[2].SetActive(true);
                StrorageType = TypePay.VIDEO;

            }
            else
            {
                btnStorage.gameObject.GetComponent<Image>().sprite = UiTop.Ins.payBtn;

                btnPrice[2].SetActive(true);
                btnAd[2].SetActive(false);
                StrorageType = TypePay.Pay;
                btnStorage.enabled = true;
            }
        }
    }
    public void PlaySound()
    {
        MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins._btnClickSound, MMSoundManager.MMSoundManagerTracks.UI, Camera.main.gameObject.transform.position);
    }
}
