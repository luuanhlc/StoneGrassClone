﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemChest : MonoBehaviour
{
    [SerializeField] private Button btnChest;
    [SerializeField] private Image imgIconReward;
    [SerializeField] private GameObject objRewardCoin;
    [SerializeField] private Text txtAmountCoin;
    private PopupChestKey popupChestKey;
    private RewardEndGame _reward;

    // Start is called before the first frame update
    void Start()
    {
        btnChest.onClick.AddListener(OnClickBtnChest);
    }

    public void Init(PopupChestKey _popupChestKey, RewardEndGame reward)
    {
        _reward = reward;
        popupChestKey = _popupChestKey;
        btnChest.gameObject.SetActive(true);
        objRewardCoin.SetActive(false);
        popupChestKey.IsOpenPrize = false;
        txtAmountCoin.gameObject.SetActive(false);
        imgIconReward.gameObject.SetActive(false);
    }

    private void OnClickBtnChest()
    {
        if (GameManager.Instance.Profile.GetKey() <= 0)
        {
            PopupDialogCanvas.Instance.Show("Not Enough Key");
            return;
        }
           

        SoundManager.Instance.PlaySoundReward();


        btnChest.gameObject.SetActive(false);
        if (popupChestKey.NumberWatchVideo >= 1 && !popupChestKey.IsOpenPrize)
        {

            objRewardCoin.SetActive(false);
            popupChestKey.IsOpenPrize = true;
            /*if (PlayerDataManager.GetUnlockSkin(_reward.Type, _reward.Id))
            {
                imgIconReward.sprite = GameManager.Instance.PlayerDataManager.DataTexture.IconCoin;
                txtAmountCoin.text = string.Format("+{0}", _reward.NumberCoinReplace);
                txtAmountCoin.gameObject.SetActive(true);
                GameManager.Instance.Profile.AddGold(_reward.NumberCoinReplace, Helper.video_reward_chest_key);
            }
            else
            {
                txtAmountCoin.gameObject.SetActive(false);
                switch (_reward.Type)
                {
                    case TypeEquipment.Hat:
                        {
                            imgIconReward.sprite = GameManager.Instance.PlayerDataManager.DataTexture.GetIconHat(_reward.Id);
                        }
                        break;
                    case TypeEquipment.Skin:
                        {
                            imgIconReward.sprite = GameManager.Instance.PlayerDataManager.DataTexture.GetIconSkin(_reward.Id);
                        }
                        break;
                    case TypeEquipment.Pet:
                        {
                            imgIconReward.sprite = GameManager.Instance.PlayerDataManager.DataTexture.GetIconPet(_reward.Id);
                        }
                        break;
                    case TypeEquipment.Mask:
                        {
                            imgIconReward.sprite = GameManager.Instance.PlayerDataManager.DataTexture.GetIconSkill(_reward.Id);
                        }
                        break;

                }

                PlayerDataManager.SetUnlockSkin(_reward.Type, _reward.Id);
            }*/

            imgIconReward.gameObject.SetActive(true);
            imgIconReward.SetNativeSize();

            var indexReward = PlayerDataManager.GetCurrentIndexRewardEndGame();
            //GameManager.Instance.PlayerDataManager.SetProcessReceiveRewardEndGame(0);
            indexReward++;
            PlayerDataManager.SetCurrentIndexRewardEndGame(indexReward);
        }
        else
        {
            int gold = Helper.GetRandomGoldReward();
            objRewardCoin.SetActive(true);
            txtAmountCoin.text = string.Format("+{0}", gold);
            txtAmountCoin.gameObject.SetActive(true);
            imgIconReward.gameObject.SetActive(false);

            GameManager.Instance.Profile.AddGold(gold);
        }

        GameManager.Instance.Profile.AddKey(-1, Helper.video_reward_chest_key);

        popupChestKey.SetLayoutKey();
    }
}
