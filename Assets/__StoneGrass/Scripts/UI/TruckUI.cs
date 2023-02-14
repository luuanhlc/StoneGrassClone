using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;
using System.Collections.Generic;
using Unicorn;

namespace Unicorn
{
    public class TruckUI : MonoBehaviour
    {
        [SerializeField] Button btnStorage;
        [SerializeField] Button btnWheels;

        [SerializeField] TextMeshProUGUI txtStorage;
        [SerializeField] TextMeshProUGUI txtWheels;

        [SerializeField] Image sldStorage;
        [SerializeField] Image sldWheels;

        [SerializeField] MMFeedbacks tbnStorageFB;
        [SerializeField] MMFeedbacks tbnWheelFB;

        private int priceStorage;
        private int priceWheel;

        public DataTruck data;

        public List<GameObject> btnPrice;
        public List<GameObject> btnAd;
        [SerializeField] private TypeEquipment typeEquipment;
        private TypePay strorageType;
        private TypePay wheelType;
        private void Start()
        {
            btnStorage.onClick.AddListener(OnClickStorage);
            btnWheels.onClick.AddListener(OnClickWheel);

            setSlide();
        }
        

        private void setSlide()
        {
            UiController.Ins.UiTop.UpdateUi(0);

            sldStorage.fillAmount = ((float)PlayerDataManager.GetPlayerStorage() / (float)PlayerDataManager.GetMaxPlayerStrong());
            sldWheels.fillAmount = ((float)PlayerDataManager.GetPlayerWhell() / 8f);

            priceStorage = PlayerDataManager.GetPlayerStorage() * 30 + 100 * (PlayerDataManager.GetPlayerStorage() + 1) / 2;
            priceWheel = PlayerDataManager.GetPlayerWhell() * 40 +100 * (PlayerDataManager.GetPlayerWhell()  + 1) / 2;

            setButton();
        }

        private void setButton()
        {
            if (sldStorage.fillAmount == 1)
            {
                txtStorage.text = "Max";
                btnStorage.enabled = false;

                btnStorage.gameObject.GetComponent<Image>().sprite = UiTop.Ins.take;
            }
            else
            {
                txtStorage.text = ConverInt.Sign(priceStorage) + " <sprite name=money>";
                if (PlayerDataManager.GetGold() < priceStorage)
                {
                    btnStorage.gameObject.GetComponent<Image>().sprite = UiTop.Ins.videoBtn;

                    strorageType = TypePay.VIDEO;
                    btnStorage.enabled = true;
                    btnPrice[0].SetActive(false);
                    btnAd[0].SetActive(true);
                }
                else
                {
                    btnPrice[0].SetActive(true);
                    btnAd[0].SetActive(false);
                    btnStorage.enabled = true;
                    strorageType = TypePay.Pay;
                    btnStorage.gameObject.GetComponent<Image>().sprite = UiTop.Ins.payBtn;

                }
            }

            if (sldWheels.fillAmount == 1)
            {
                txtWheels.text = "Max";
                btnWheels.enabled = false;
                btnWheels.gameObject.GetComponent<Image>().sprite = UiTop.Ins.take;
            }
            else
            {
                txtWheels.text = ConverInt.Sign(priceWheel) + " <sprite name=money>";
                if (PlayerDataManager.GetGold() < priceWheel)
                {
                    btnWheels.gameObject.GetComponent<Image>().sprite = UiTop.Ins.videoBtn;

                    btnWheels.enabled = true;
                    btnPrice[1].SetActive(false);
                    btnAd[1].SetActive(true);
                    wheelType = TypePay.VIDEO;
                }
                else
                {
                    btnWheels.gameObject.GetComponent<Image>().sprite = UiTop.Ins.payBtn;

                    btnPrice[1].SetActive(true);
                    btnAd[1].SetActive(false);
                    wheelType = TypePay.Pay;
                    btnWheels.enabled = true;
                }
            }
        }

        private void OnClickWheel()
        {
            switch (wheelType)
            {
                case TypePay.Pay:
                    GameManager.Instance.Profile.AddGold(-priceWheel);
                    break;
                case TypePay.VIDEO:
                    UnicornAdManager.ShowAdsReward(OnRewardedVideo, string.Format(Helper.video_shop_general, typeEquipment));
                    break;
            }
            PlayerDataManager.SetWheel(PlayerDataManager.GetPlayerWhell() + 1);
            ChangerSkin.Ins.ChangeWheel(data.dataTruck[PlayerDataManager.GetTruckIndex()].whellList[PlayerDataManager.GetPlayerWhell()].wheel, data.dataTruck[PlayerDataManager.GetTruckIndex()].whellList[PlayerDataManager.GetPlayerWhell()].mtr);
            infor.Ins.updateSpeed();
            setSlide();
            PlaySound();
            tbnWheelFB.PlayFeedbacks();
        }

        private void OnClickStorage()
        {
            switch (strorageType)
            {
                case TypePay.Pay:
                    GameManager.Instance.Profile.AddGold(-priceStorage);
                    break;
                case TypePay.VIDEO:
                    UnicornAdManager.ShowAdsReward(OnRewardedVideo, string.Format(Helper.video_shop_general, typeEquipment));
                    break;
            }
            PlayerDataManager.SetPlayerStorage(PlayerDataManager.GetPlayerStorage() + 5);
            XepProduct.Isn.updateSucChua();
            UiController.Ins.UiInGame.updateStrong();
            setSlide();
            PlaySound();
            VanFlash.Ins.changerTranform(PlayerDataManager.GetPlayerStorage());
            tbnStorageFB.PlayFeedbacks();
        }
        public void PlaySound()
        {
            MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins._btnClickSound, MMSoundManager.MMSoundManagerTracks.UI, Camera.main.gameObject.transform.position);
        }
        private void OnRewardedVideo(int x)
        {
            GameManager.Instance.numberVideo++;
        }
    }
    
}
