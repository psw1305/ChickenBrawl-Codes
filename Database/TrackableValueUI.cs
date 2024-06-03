using System;
using UnityEngine;
using TMPro;
using DG.Tweening;
using CKB.Database;
using CKB.Utilities;

namespace CKB.UI
{
    public abstract class TrackableValueUI<T> : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;
        [SerializeField] private Transform icon;
        [SerializeField] private float numericAnimationDuration = -1;

        private TrackableValue<T> trackable;
        private float lastDesired;

        private void OnEnable()
        {
            trackable = FindTrackable();
            trackable.Changed += Redraw;

            Redraw(trackable.Value);
        }

        private void OnDisable()
        {
            trackable.Changed -= Redraw;
        }

        private void Redraw(T newValue)
        {
            if (numericAnimationDuration > 0)
            {
                PlayNumericAnimation(newValue);
            }
            else
            {
                label.text = ValueToText(newValue);
            }
        }

        private void PlayNumericAnimation(T newValue)
        {
            float desired = newValue switch
            {
                double d => (float)d,
                float f => f,
                int i => i,
                _ => throw new Exception("If you want numeric animation, label also should be float or int!")
            };

            float current = lastDesired;
            lastDesired = desired;

            var tween = DOTween.To(
                () => current,
                x =>
                {
                    current = x;
                    label.text = FloatToText(current);
                },
                desired,
                numericAnimationDuration
            );
        }

        /// <summary>
        /// Called only once.
        /// </summary>
        public abstract TrackableValue<T> FindTrackable();

        /// <summary>
        /// Custom convert value to text.
        /// </summary>
        protected virtual string ValueToText(T value)
        {
            return value.ToString();
        }

        /// <summary>
        /// Custom convert float to text (for numerics).
        /// </summary>
        protected virtual string FloatToText(float value)
        {
            return value.Round().ToString();
        }
    }
}
