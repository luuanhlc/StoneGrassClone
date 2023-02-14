using Common.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Unicorn.FSM
{
    public class EndgameAction : UnicornFSMAction
    {
        public EndgameAction(GameManager gameManager, FSMState owner) : base(gameManager, owner)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
        
            SoundManager.Instance.StopFootStep();

            int gold = 0;
            if (GameManager.LevelManager.Result == LevelResult.Win)
            {
                gold += Constants.GOLD_WIN;
            }

            ProcessWinLose(gold);

            SoundManager.Instance.PlayFxSound(GameManager.LevelManager.Result);
        }

        private void ProcessWinLose(int gold)
        {
            Debug.Log("CurrentLevel_" + GameManager.Instance.CurrentLevel);

            switch (GameManager.LevelManager.Result)
            {
                case LevelResult.Win:

                    GameManager.UiController.OpenUiWin(gold);
                    Analytics.LogEndGameWin(GameManager.Instance.CurrentLevel);
                    PrefabStorage.Instance.fxWinPrefab.SetActive(true);
                    break;
                case LevelResult.Lose:
                    GameManager.UiController.OpenUiLose();
                    Analytics.LogEndGameLose(GameManager.Instance.CurrentLevel);
                    break;
                default:
                    break;
            }
        }
        public override void OnExit()
        {
            base.OnExit();
            SoundManager.Instance.StopSound(GameManager.LevelManager.Result);
            PrefabStorage.Instance.fxWinPrefab.SetActive(false);
        }
    }
}