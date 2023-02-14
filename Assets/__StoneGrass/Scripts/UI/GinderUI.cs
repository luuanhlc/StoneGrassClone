using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;
using Unicorn;

using System.Collections.Generic;

public class GinderUI : MonoBehaviour
{
    [SerializeField] private Image sldSawBlades;
    [SerializeField] private Image sldNumberOfPongs;
    [SerializeField] private Image sldRotationSpeed;

    [SerializeField] private Button btnSawBlades;
    [SerializeField] private Button btnNumberOfPongs;
    [SerializeField] private Button btnRotationSpeed;

    [SerializeField] private TextMeshProUGUI txtSawBlades;
    [SerializeField] private TextMeshProUGUI txtNumberOfPongs;
    [SerializeField] private TextMeshProUGUI txtRotationSpeed;

    [SerializeField] MMFeedbacks SawBladesFB;
    [SerializeField] MMFeedbacks NumberOfPongFB;
    [SerializeField] MMFeedbacks RotationSpeedFB;

    public List<GameObject> btnPrice;
    public List<GameObject> btnAd;

    private TypePay sawBladeType;
    private TypePay NumberOfPongType;
    private TypePay RotationType;
    
    private int priceSawBlade;
    private int priceNumberOfPongs;
    private int priceRotationSpeed;

    [SerializeField] private int maxSawBlade;
    [SerializeField] private int maxRotationSpeed;
    private int maxNumberOfPongs;
    [SerializeField] private TypeEquipment typeEquipment;


    public static GinderUI Ins;
    private void Awake()
    {
        Ins = this;
        maxNumberOfPongs = 5;
        btnSawBlades.onClick.AddListener(OnClickSawBlades);
        btnNumberOfPongs.onClick.AddListener(OnClickNumberOfPongs);
        btnRotationSpeed.onClick.AddListener(OnClickRotationSpeed);
        setSlide();
    }

    public void setSlide()
    {

        sldSawBlades.fillAmount = ((float)PlayerDataManager.GetSawBlade() / maxSawBlade);
        sldRotationSpeed.fillAmount = ((float)PlayerDataManager.GetSawSpeed() / maxRotationSpeed);
        sldNumberOfPongs.fillAmount = ((float)PlayerDataManager.GetNumberOfPongs() / maxNumberOfPongs);

        priceNumberOfPongs = (int)(PlayerDataManager.GetNumberOfPongs() * 450);
        priceRotationSpeed = (int)(PlayerDataManager.GetSawSpeed() * 500);
        priceSawBlade = (int)(PlayerDataManager.GetSawBlade() * 550);

        UiTop.Ins.UpdateUi(0);
        setButton();
    }

    
    public void setButton()
    {
        Debug.Log("Set");
        //btn Number ot Pongs
        if (sldNumberOfPongs.fillAmount == 1)
        {
            //btnNumberOfPongs.gameObject.SetActive(false);
            txtNumberOfPongs.text = "Max";
            btnNumberOfPongs.enabled = false;
            NumberOfPongType = TypePay.USE;

            btnNumberOfPongs.gameObject.GetComponent<Image>().sprite = UiTop.Ins.take;
        }
        else
        {
            txtNumberOfPongs.text = ConverInt.Sign(priceNumberOfPongs) + " <sprite name=money>";
            if (GameManager.Instance.Profile.GetGold() < priceNumberOfPongs)
            {
                btnNumberOfPongs.gameObject.GetComponent<Image>().sprite = UiTop.Ins.videoBtn;

                Debug.Log(GameManager.Instance.Profile.GetGold());
                Debug.Log(priceNumberOfPongs);
                btnNumberOfPongs.enabled = true;
                btnPrice[1].SetActive(false);
                btnAd[1].SetActive(true);
                NumberOfPongType = TypePay.VIDEO;
            }
            else
            {
                btnNumberOfPongs.gameObject.GetComponent<Image>().sprite = UiTop.Ins.payBtn;

                btnPrice[1].SetActive(true);
                btnAd[1].SetActive(false);
                btnNumberOfPongs.enabled = true;
                NumberOfPongType = TypePay.Pay;
            }
        }

        //btn RotationSpeed

        if (sldRotationSpeed.fillAmount == 1)
        {
            txtRotationSpeed.text = "Max";
            btnRotationSpeed.enabled = false;

            btnRotationSpeed.gameObject.GetComponent<Image>().sprite = UiTop.Ins.take;
        }
        else
        {
            txtRotationSpeed.text = ConverInt.Sign(priceRotationSpeed) + " <sprite name=money>";
            if (GameManager.Instance.Profile.GetGold() < priceRotationSpeed)
            {
                btnRotationSpeed.gameObject.GetComponent<Image>().sprite = UiTop.Ins.videoBtn;

                RotationType = TypePay.VIDEO;
                btnRotationSpeed.enabled = true;
                btnAd[2].SetActive(true);
                btnPrice[2].SetActive(false);
            }
            else
            {
                btnRotationSpeed.gameObject.GetComponent<Image>().sprite = UiTop.Ins.payBtn;

                btnAd[2].SetActive(false);
                btnPrice[2].SetActive(true);
                btnRotationSpeed.enabled = true;
                RotationType = TypePay.Pay;
            }
        }

        // btn Saw Blades

        if (sldSawBlades.fillAmount == 1)
        {
            txtSawBlades.text = "Max";
            btnSawBlades.gameObject.GetComponent<Image>().sprite = UiTop.Ins.take;

            btnSawBlades.enabled = false;
        }
        else
        {
            txtSawBlades.text = ConverInt.Sign(priceSawBlade) + " <sprite name=money>";
            if (GameManager.Instance.Profile.GetGold() < priceSawBlade)
            {
                btnSawBlades.gameObject.GetComponent<Image>().sprite = UiTop.Ins.videoBtn;

                btnSawBlades.enabled = true;
                btnAd[0].SetActive(true);
                btnPrice[0].SetActive(false);
                sawBladeType = TypePay.VIDEO;
            }
            else
            {
                btnSawBlades.gameObject.GetComponent<Image>().sprite = UiTop.Ins.payBtn;
                btnAd[0].SetActive(false);
                btnPrice[0].SetActive(true);
                btnSawBlades.enabled = true;
                sawBladeType = TypePay.Pay;
            }
        }
    }
    private void OnClickSawBlades()
    {
        switch (sawBladeType)
        {
            case TypePay.Pay:
                GameManager.Instance.Profile.AddGold(-priceSawBlade);
                PlayerDataManager.SetSawBlade(Mathf.Min(maxSawBlade, PlayerDataManager.GetSawBlade() + maxSawBlade / 20));
                PlayerDataManager.SetDamage((int)(PlayerDataManager.GetSawBlade() * .5) + PlayerDataManager.GetDamage());
                setSlide();
                infor.Ins.Dame = PlayerDataManager.GetDamage();
                break;
            case TypePay.VIDEO:
                index = 1;
                UnicornAdManager.ShowAdsReward(OnRewardedVideo, string.Format(Helper.video_shop_general, typeEquipment));
                break;
        }
        
     
        //play feed backs
        SawBladesFB.PlayFeedbacks();
        PlaySound();
    }
    private void OnClickNumberOfPongs()
    {
        switch (NumberOfPongType)
        {
            case TypePay.Pay:
                GameManager.Instance.Profile.AddGold(-priceNumberOfPongs);
                PlayerDataManager.SetNumberOfPongs(Mathf.Min(maxNumberOfPongs, PlayerDataManager.GetNumberOfPongs() + (float)(maxNumberOfPongs / 10f)));
                PlayerDataManager.SetDamage((int)(PlayerDataManager.GetNumberOfPongs() * .5) + PlayerDataManager.GetDamage());
                setSlide();
                infor.Ins.changerSaw();
                break;
            case TypePay.VIDEO:
                index = 0;
                UnicornAdManager.ShowAdsReward(OnRewardedVideo, string.Format(Helper.video_shop_general, typeEquipment));
                break;
        }
        
        //line.Isn.newLine(Mathf.Max(PlayerDataManager.GetNumberOfPongs() - 1.5f, 1f));
        //play feed backs

        NumberOfPongFB.PlayFeedbacks();
        PlaySound();
    }

    private void OnClickRotationSpeed()
    {
        switch (RotationType)
        {
            case TypePay.Pay:
                GameManager.Instance.Profile.AddGold(-priceRotationSpeed);
                PlayerDataManager.SetSawSpeed(Mathf.Min(maxRotationSpeed, PlayerDataManager.GetSawSpeed() + maxRotationSpeed / 25));
                PlayerDataManager.SetDamage((int)(PlayerDataManager.GetSawSpeed() * .5) + PlayerDataManager.GetDamage());
                setSlide();
                infor.Ins.sawSpeed = PlayerDataManager.GetSawSpeed();
                break;
            case TypePay.VIDEO:
                index = 2;
                UnicornAdManager.ShowAdsReward(OnRewardedVideo, string.Format(Helper.video_shop_general, typeEquipment));
                break;
        }
        
        RotationSpeedFB.PlayFeedbacks();
        PlaySound();
    }
    int index;
    private void OnRewardedVideo(int x)
    {
        GameManager.Instance.numberVideo++;

        switch (index)
        {
            case 0:
                GameManager.Instance.Profile.AddGold(-priceNumberOfPongs);
                PlayerDataManager.SetNumberOfPongs(Mathf.Min(maxNumberOfPongs, PlayerDataManager.GetNumberOfPongs() + (float)(maxNumberOfPongs / 10f)));
                PlayerDataManager.SetDamage((int)(PlayerDataManager.GetNumberOfPongs() * .5) + PlayerDataManager.GetDamage());
                setSlide();
                infor.Ins.changerSaw();
                break;
                break;
            case 1:
                PlayerDataManager.SetSawBlade(Mathf.Min(maxSawBlade, PlayerDataManager.GetSawBlade() + maxSawBlade / 20));
                PlayerDataManager.SetDamage((int)(PlayerDataManager.GetSawBlade() * .5) + PlayerDataManager.GetDamage());
                setSlide();
                infor.Ins.Dame = PlayerDataManager.GetDamage();
                break;
            case 2:
                GameManager.Instance.Profile.AddGold(-priceRotationSpeed);
                PlayerDataManager.SetSawSpeed(Mathf.Min(maxRotationSpeed, PlayerDataManager.GetSawSpeed() + maxRotationSpeed / 25));
                PlayerDataManager.SetDamage((int)(PlayerDataManager.GetSawSpeed() * .5) + PlayerDataManager.GetDamage());
                setSlide();
                infor.Ins.sawSpeed = PlayerDataManager.GetSawSpeed();
                break;
        }
    }
    public void PlaySound()
    {
        MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins._btnClickSound, MMSoundManager.MMSoundManagerTracks.UI, Camera.main.gameObject.transform.position);
    }
}

