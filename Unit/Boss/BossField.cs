using System.Collections;
using UnityEngine;
using static CKB.Utilities.CommonGameplayFacade;

namespace CKB
{
    public class BossField : MonoBehaviour
    {
        [Header("Camera")]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Camera bossCamera;
        [SerializeField] private CameraManager cameraManager;

        [Header("Animator")]
        [SerializeField] private Animator playerAnimator;

        [Header("UI")]
        [SerializeField] private GameObject gameClearWindow;
        [SerializeField] private GameObject gameResultWindow;

        private bool isClear = false;

        public void Init()
        {
            mainCamera.gameObject.SetActive(false);
            bossCamera.gameObject.SetActive(true);
        }

        public void BossBattleStart()
        {
            if (GroupPlayer.Level >= GameData.BossLevel)
            {
                DB.PassedLevel.Value++;
                isClear = true;
            }

            BossPlayer.Run();
            BossChicken.Run();
        }

        public void BossBattleEnd()
        {
            if (isClear)
            {
                BossChicken.Die();
            }
            else
            {
                cameraManager.SetTarget(BossChicken.gameObject);
                BossPlayer.Die();
            }

            StartCoroutine(BattleEndCoroutine());
        }

        private IEnumerator BattleEndCoroutine()
        {
            yield return new WaitForSeconds(1.0f);

            gameClearWindow.SetActive(false);
            gameResultWindow.SetActive(true);

            yield return new WaitForSeconds(3.0f);

            if (isClear)
            {
                BossPlayer.Stop();
            }
            else
            {
                BossChicken.Stop();
            }
        }
    }
}
