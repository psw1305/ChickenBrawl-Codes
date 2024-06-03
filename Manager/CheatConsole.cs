using Sirenix.OdinInspector;
using UnityEngine;
using CKB.Core;
using CKB.Database;
using static CKB.Utilities.CommonGameplayFacade;

namespace CKB
{
    public class CheatConsole : SmartScript
    {
        [SerializeField] private float money;
        [SerializeField] private int levelPassed;

        [Button("Update Money")]
        private void SetMoney()
        {
            DB.Money.Value = money;
        }

        [Button("Update Level")]
        private void LevelPassed()
        {
            DB.PassedLevel.Value = levelPassed;
        }

        [Button("Data Clear")]
        private void DataClear()
        {
            GameDatabase.Clear();
        }

        [Button("Game Clear")]
        private void PushGameClear()
        {
            GroupPlayer.Level = 9999;
            StateMachine.Push(new GameClearState());
        }

        [Button("Game Over")]
        private void PushGameOver()
        {
            StateMachine.Push(new GameOverState());
        }

        [Button("Game Revive")]
        private void PushRevive()
        {
            GroupPlayer.Retire();
        }

        [Button("Game Revive")]
        private void ReloadScene()
        {
            LoaderScene.ReloadCurrentScene();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PushGameClear();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                PushGameOver();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                PushRevive();
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                ReloadScene();
            }
        }
    }
}
