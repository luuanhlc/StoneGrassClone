using System;
using System.Collections;
using System.Collections.Generic;
using RocketTeam.Sdk.Services.Ads;
using Unicorn;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ItemSkinShop : MonoBehaviour
{
    [SerializeField] private TypeEquipment typeEquipment;
    [SerializeField] protected Image icon;
    [SerializeField] protected List<Button> listBtnBehavior;
    [SerializeField] private Button btnView;
    [SerializeField] protected Text txtVideo;
    [SerializeField] private Text txtCoin;

    protected DataShop dataShop;
    protected ShopCharacter shopCharacter;
    private PlayerDataManager playerData;
    private bool isAwakeCalled;

    protected Image ImgBg { get; set; }

    public TypeEquipment TypeEquipment => typeEquipment;

    private void Awake()
    {
        if (isAwakeCalled) return;
        isAwakeCalled = true;
        ImgBg = GetComponent<Image>();
        shopCharacter = GameManager.Instance.UiController.ShopCharater;
        playerData = GameManager.Instance.PlayerDataManager;
        
        btnView.onClick.AddListener(OnClickBtnView);
        for (int i = 0; i < listBtnBehavior.Count; i++)
        {
            int id = i;
            listBtnBehavior[i].onClick.AddListener(() => OnClickBtnBehaviour(id));
        }
    }

    public virtual void Init(DataShop data)
    {
        Awake();
        dataShop = data;
        for (int i = 0; i < listBtnBehavior.Count; i++)
        {
            listBtnBehavior[i].gameObject.SetActive(false);
        }

        txtCoin.text = data.numberCoinUnlock.ToString();

        
        icon.sprite = playerData.DataTextureSkin.GetIcon(typeEquipment, data.idSkin);
        ImgBg.sprite = playerData.DataTexture.GetBackgroundIcon(false);
        
        /*if (PlayerDataManager.GetUnlockSkin(typeEquipment, data.idSkin))
        {
            InitUnlocked();
        }
        else
        {
            InitLocked();
        }*/
    }

    private void InitUnlocked()
    {
        /*if (PlayerDataManager.GetIdEquipSkin(typeEquipment) == dataShop.idSkin)
        {
            listBtnBehavior[(int)TypeButtonBehavior.REMOVE].gameObject.SetActive(true);
            ImgBg.sprite = GameManager.Instance.PlayerDataManager.DataTexture.GetBackgroundIcon(true);
        }
        else
        {
            listBtnBehavior[(int)TypeButtonBehavior.USE].gameObject.SetActive(true);
        }*/
    }

    private void InitLocked()
    {
        if (dataShop.typeUnlock.HasFlag(TypeUnlockSkin.SPIN))
        {
            listBtnBehavior[(int)TypeButtonBehavior.SPIN].gameObject.SetActive(true);
        }

        if (dataShop.typeUnlock.HasFlag(TypeUnlockSkin.COIN))
        {
            listBtnBehavior[(int)TypeButtonBehavior.UNLOCK_BY_COIN].gameObject.SetActive(true);
        }

        if (dataShop.typeUnlock.HasFlag(TypeUnlockSkin.VIDEO))
        {
            listBtnBehavior[(int)TypeButtonBehavior.UNLOCK_BY_VIDEO].gameObject.SetActive(true);
            
            //int numberWatchVideo = PlayerDataManager.GetNumberWatchVideoSkin(typeEquipment, dataShop.idSkin);
            //txtVideo.text = $"{numberWatchVideo}/{dataShop.numberVideoUnlock}";
        }
    }
    
    protected virtual void OnClickBtnView()
    {
        SoundManager.Instance.PlaySoundButton();
        Apply();
    }

    protected virtual void OnClickBtnBehaviour(int idBehaviour)
    {
        SoundManager.Instance.PlaySoundButton();
        
        switch ((TypeButtonBehavior)idBehaviour)
        {
            case TypeButtonBehavior.SPIN:
                {
                    shopCharacter.OnBackPressed();
                    //GameManager.Instance.UiController.OpenLuckyWheel();
                }
                break;
            case TypeButtonBehavior.UNLOCK_BY_VIDEO:
                {
                    UnicornAdManager.ShowAdsReward(OnRewardedVideo, string.Format(Helper.video_shop_general, typeEquipment));
                }
                break;
            case TypeButtonBehavior.UNLOCK_BY_COIN:
                {
                    if (GameManager.Instance.Profile.GetGold() < dataShop.numberCoinUnlock)
                    {
                        //shopCharacter.NotifyNotEnoughGold(this.transform);

                        return;
                    }

                    /*PlayerDataManager.SetUnlockSkin(typeEquipment, dataShop.idSkin);
                    GameManager.Instance.Profile.AddGold(-dataShop.numberCoinUnlock, "shop_skin_" + typeEquipment);*/
                    Apply();
                    //shopCharacter.ReloadLayout(typeEquipment);
                }
                break;
            case TypeButtonBehavior.USE:
                {
                    //PlayerDataManager.SetIdEquipSkin(typeEquipment, dataShop.idSkin);
                    OnClickBtnView();
                    //shopCharacter.ReloadLayout(typeEquipment);
                }
                break;
            case TypeButtonBehavior.REMOVE:
                {
                    /*PlayerDataManager.SetIdEquipSkin(typeEquipment, -1);
                    Apply(false);
                    shopCharacter.ReloadLayout(typeEquipment);*/
                }
                break;
            default:
                break;
        }
    }
    
    private void OnRewardedVideo(int x)
    {
        //int numberWatchVideo = PlayerDataManager.GetNumberWatchVideoSkin(typeEquipment, dataShop.idSkin);
        /*numberWatchVideo++;
        if (numberWatchVideo >= dataShop.numberVideoUnlock)
        {
            //PlayerDataManager.SetUnlockSkin(typeEquipment, dataShop.idSkin);
        }*/

        //PlayerDataManager.SetNumberWatchVideoSkin(typeEquipment, dataShop.idSkin, numberWatchVideo);
        Apply();
        //GameManager.Instance.UiController.ShopCharater.ReloadLayout(typeEquipment);
    }

    private void Apply(bool isApply = true)
    {
        /*if (typeEquipment == TypeEquipment.Pet)
        {
            //shopCharacter.ChangePet(isApply ? dataShop.idSkin : -1);
            return;
        }*/
        
        //shopCharacter.SkinChanger.Change(typeEquipment, isApply ? dataShop.idSkin : -1);
    }
    
}


public enum TypeButtonBehavior
{
    SPIN = 0,
    UNLOCK_BY_VIDEO = 2,
    UNLOCK_BY_COIN = 1,
    USE = 4,
    REMOVE = 3
}
