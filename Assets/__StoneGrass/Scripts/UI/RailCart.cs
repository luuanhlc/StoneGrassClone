using UnityEngine;
using TMPro;
using UnityEngine.UI;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;

public class RailCart : MonoBehaviour
{
    [SerializeField] Button btnStorage;
    [SerializeField] Button btnWheels;

    [SerializeField] TextMeshProUGUI txtStorage;
    [SerializeField] TextMeshProUGUI txtWheels;

    [SerializeField] Image sldStorage;
    [SerializeField] Image sldWheels;

    private int priceStorage;
    private int priceWheel;

    [SerializeField] private int maxStorage;
    [SerializeField] private int maxSpeed;

    [SerializeField] MMFeedbacks _StorageFB;
    [SerializeField] MMFeedbacks _WheelFB;

    private void Start()
    {
        btnStorage.onClick.AddListener(OnClickStorage);
        btnWheels.onClick.AddListener(OnClickSpeed);
        setSlide();
    }
    private void playSound()
    {
    }
    private void setSlide()
    {
        sldStorage.fillAmount = ((float)PlayerDataManager.GetTrainStorage() / (float)maxStorage); 
        sldWheels.fillAmount = ((float)PlayerDataManager.GetTrainSpeed() / (float)maxSpeed);
        priceStorage = PlayerDataManager.GetTrainStorage() * 30 + 100 * PlayerDataManager.GetTrainStorage() / 2;
        priceWheel = (int)(PlayerDataManager.GetTrainSpeed() * 40 + 100 * PlayerDataManager.GetTrainSpeed() / 2);

        setButton();
    }

    private void setButton()
    {
        if (sldStorage.fillAmount == 1)
        {
            txtStorage.text = "Max";
            btnStorage.enabled = false;
        }
        else
        {
            txtStorage.text = "Buy " + ConverInt.Sign(priceStorage);
            if (PlayerDataManager.GetGold() < priceStorage)
                btnStorage.enabled = false;
            else
                btnStorage.enabled = true;
        }

        if (sldWheels.fillAmount == 1)
        {
            txtWheels.text = "Max";
            btnWheels.enabled = false;
        }
        else
        {
            txtWheels.text = "Buy " + ConverInt.Sign(priceWheel);
            if (PlayerDataManager.GetGold() < priceWheel)
                btnWheels.enabled = false;
            else
                btnWheels.enabled = true;
        }
    }

    private void OnClickSpeed()
    {
        GameManager.Instance.Profile.AddGold(-priceWheel);
        UiController.Ins.UiTop.UpdateUi(0);
        if(XeDay.Isn != null)
            XeDay.Isn.updateSpeed();
        PlayerDataManager.SetTrainSpeed(Mathf.Min(PlayerDataManager.GetTrainSpeed() + 3f, (float)maxSpeed));
        
        setSlide();
        playSound();
    }

    private void OnClickStorage()
    {
        GameManager.Instance.Profile.AddGold(-priceStorage);
        UiController.Ins.UiTop.UpdateUi(0);
        Debug.Log("Click");
        PlayerDataManager.SetTrainStorage(Mathf.Min(PlayerDataManager.GetTrainStorage() + 5, maxStorage));
        setSlide();
        playSound();
    }
}
