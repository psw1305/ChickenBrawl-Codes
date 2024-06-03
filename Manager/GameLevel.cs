using System.Linq;
using UnityEngine;
using CKB.Core;
using CKB.Database;
using UnityEngine.SceneManagement;

namespace CKB.Gameplay
{
    public class GameLevel : GameStateMachineUser
    {
        [Tooltip("Use -1 for non-level scene like menu, etc")]
        [SerializeField] private int _sceneNumber = -1;

        public int SceneNumber => _sceneNumber;
        private bool IsLast => _sceneNumber == SceneLoader.CountOfLevelsInGame;

        public readonly TrackableValue<float> RewardMultiplier = new TrackableValue<float>(1f);

        public int GetNextSceneNumber()
        {
            return SceneNumber + 1;
        }

        public bool IsValid => ActiveSceneNumber != -1;

        private int ActiveSceneNumber
        {
            get
            {
                var activeSceneName = SceneManager.GetActiveScene().name;
                var splits = activeSceneName.Split('_');

                if (int.TryParse(splits.Last(), out int n))
                    return n;

                return -1;
            }
        }

        private string ActiveScenePath => SceneManager.GetActiveScene().path;

    }
}
