using System.Collections;
using System.Threading;
using UnityEngine;

namespace CKB.Utilities
{
    public class FPSBooster : MonoBehaviour
    {
        [Header("Frame Settings")]
        private readonly int MaxRate = 9999;
        public float TargetFrameRate = 60.0f;
        private float currentFrameTime;

        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = MaxRate;
            currentFrameTime = Time.realtimeSinceStartup;
            StartCoroutine(WaitForeNextFrame());
        }

        private IEnumerator WaitForeNextFrame()
        {
            yield return new WaitForEndOfFrame();
            currentFrameTime += 1.0f / TargetFrameRate;
            
            var t = Time.realtimeSinceStartup;
            var sleepTime = currentFrameTime - t - 0.01f;
            
            if (sleepTime > 0)
            {
                Thread.Sleep((int)(sleepTime * 1000));
            }

            while (t < currentFrameTime) 
            {
                t = Time.realtimeSinceStartup;
            }
        }
    }
}
