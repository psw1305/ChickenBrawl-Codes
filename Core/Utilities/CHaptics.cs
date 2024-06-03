using UnityEngine.Events;
using CandyCoded.HapticFeedback;
using System.Diagnostics;

namespace CKB.Utilities
{
    public static class CHaptic
    {
        private static bool isActive = true;

        public static void SetHapticsActive(bool active)
        {
            isActive = active;
        }

        public static void HapticLight() => Haptic(HapticFeedback.LightFeedback);
        public static void HapticMedium() => Haptic(HapticFeedback.MediumFeedback);
        public static void HapticHeavy() => Haptic(HapticFeedback.HeavyFeedback);

        public static void Haptic(UnityAction vibrateStrength)
        {
            if (isActive) return;

            vibrateStrength.Invoke();
        }
    }
}
