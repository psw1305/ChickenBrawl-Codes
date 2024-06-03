using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using CKB.Utilities;
using static CKB.Utilities.CommonGameplayFacade;

namespace CKB.Core
{
    public class SceneLoader : MonoBehaviour, ILazy
    {
        public static string CurrentSceneName => SceneManager.GetActiveScene().name;
        public static int CountOfLevelsInGame => SceneManager.sceneCountInBuildSettings - 1;

        private bool _isSceneLoading = false;

        public void ReloadCurrentScene()
        {
            LoadScene("Main");
        }

        private async void LoadScene(string sceneName)
        {
            if (_isSceneLoading)
                return;

            _isSceneLoading = true;

            StateMachine.Push(new SceneLoading());

            if (CoreData.nextLevelLoadDelay > 0)
            {
                await CoreData.nextLevelLoadDelay.Seconds();
            }

            if (CoreData.clearTweensOnSceneExit)
            {
                DOTween.Clear(true);
            }

            SceneManager.LoadScene(sceneName);
        }
    }
}
