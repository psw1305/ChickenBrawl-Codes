using System.Collections;
using UnityEngine;

namespace CKB.Core
{
    public class PoolMember : SmartScript
    {
        private Coroutine hideRoutine;
        private bool isHiding;

        public void HideAfter(Pool pool, float delay)
        {
            if (isHiding)
            {
                StopCoroutine(hideRoutine);
            }

            hideRoutine = StartCoroutine(Hiding(pool, delay));
        }

        IEnumerator Hiding(Pool pool, float delay)
        {
            isHiding = true;

            yield return new WaitForSeconds(delay);

            isHiding = false;

            pool.HideEffect(this);
        }
    }
}
