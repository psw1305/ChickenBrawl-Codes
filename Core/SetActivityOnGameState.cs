using UnityEngine;
using CKB.Core;
using CKB.Utilities;

namespace CKB
{
    public class SetActivityOnGameState : GameStateMachineUser
    {
        [SerializeField] private bool toggleToActive = true;
        [SerializeField] private float delay;

        public GameObject[] OnPlaying;
        public GameObject[] OnClear;
        public GameObject[] OnOver;
        public GameObject[] OnFinish;

        protected override void OnGamePlay() => Show(OnPlaying);
        protected override void OnGameFinish() => Show(OnFinish);
        protected override void OnGameClear() => Show(OnClear);
        protected override void OnGameOver() => Show(OnOver);

        private void Show(GameObject[] parent)
        {
            if (parent.IsNullOrEmpty()) return;

            foreach (GameObject obj in parent)
            {
                float delay = this.delay;

                if (obj == null) continue;

                var window = obj.GetComponent<Window>();

                if (window != null)
                {
                    delay = window.OpenDelay;
                }

                if (toggleToActive)
                {
                    obj.On(delay);
                }
                else
                {
                    obj.Off(delay);
                }
            }
        }
    }
}
