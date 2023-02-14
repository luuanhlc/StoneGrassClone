using Common.FSM;
using UnityEngine;

namespace Unicorn.FSM
{
    public class LobbyAction : UnicornFSMAction
    {
        public LobbyAction(GameManager gameManager, FSMState owner) : base(gameManager, owner)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            //Debug.Log("Enter");
            GameManager.UiController.UiMainLobby.Show(true);
            if(SoundManager.Instance != null)
                SoundManager.Instance.PlayFxSound(soundEnum: SoundManager.GameSound.Lobby);
            ShowChestKey();
        }

        public override void OnExit()
        {
            //Debug.Log("exit");
            base.OnExit();
            GameManager.UiController.UiMainLobby.Show(false);
            if(SoundManager.Instance != null)
                SoundManager.Instance.StopSound(SoundManager.GameSound.Lobby);
        }

        private void ShowChestKey()
        {
            if (GameManager.Profile.GetKey() < 3) return;
        
            var playerData = GameManager.Instance.PlayerDataManager;
            int indexReward = PlayerDataManager.GetCurrentIndexRewardEndGame();
            if (indexReward >= playerData.DataRewardEndGame.Datas.Count)
            {
                indexReward = playerData.DataRewardEndGame.Datas.Count - 1;
            }

            var reward = playerData.DataRewardEndGame.Datas[indexReward];

            GameManager.Instance.UiController.OpenPopupChestKey(reward);

        }
    }
}