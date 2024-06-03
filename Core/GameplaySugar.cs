using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace CKB.Utilities
{
    public static class GameplaySugar
    {
        public static UniTask Seconds(this float f)
        {
            return UniTask.Delay(TimeSpan.FromSeconds(f));
        }

        public static void On(this Transform target, bool canBeNull = true) => target.gameObject.On();
        public static void On(this Transform target, float after, bool canBeNull = true) => target.gameObject.On();
        public static void On(this GameObject target, bool canBeNull = true)
        {
            if (!IsObjectValid(target, canBeNull)) return;

            target.SetActive(true);
        }

        public static void On(this GameObject target, float after, bool canBeNull = true)
        {
            if (!IsObjectValid(target, canBeNull))
                return;

            DOVirtual.DelayedCall(after, () => On(target, canBeNull));
        }

        public static void Off(this Transform target, bool canBeNull = true) => target.gameObject.Off();
        public static void Off(this Transform target, float after, bool canBeNull = true) => target.gameObject.Off();
        public static void Off(this GameObject target, bool canBeNull = true)
        {
            if (!IsObjectValid(target, canBeNull))
                return;

            target.SetActive(false);
        }

        public static void Off(this GameObject target, float after, bool canBeNull = true)
        {
            if (!IsObjectValid(target, canBeNull))
                return;

            DOVirtual.DelayedCall(after, () => Off(target, canBeNull));
        }

        public static void Reactivate(this GameObject target, bool canBeNull = true)
        {
            if (!IsObjectValid(target, canBeNull))
                return;

            target.SetActive(false);
            target.SetActive(true);
        }

        public static void Reactivate(this GameObject target, float delay, bool canBeNull = true)
        {
            if (!IsObjectValid(target, canBeNull))
                return;

            target.SetActive(false);
            DOVirtual.DelayedCall(delay, () => target.SetActive(true));
        }

        public static void SwitchActivity(this GameObject target, bool canBeNull = true)
        {
            if (!IsObjectValid(target, canBeNull))
                return;

            target.SetActive(!target.activeSelf);
        }

        private static bool IsObjectValid(GameObject target, bool canBeNull)
        {
            if (target == null)
            {
                if (canBeNull)
                    return false;

                throw new ArgumentNullException("GameObject you want to change activity");
            }

            return true;
        }
    }
}
