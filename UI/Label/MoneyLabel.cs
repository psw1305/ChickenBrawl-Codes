using UnityEngine;
using CKB.Database;
using CKB.Utilities;

namespace CKB.UI
{
    public class MoneyLabel : TrackableValueUI<float>
    {
        [SerializeField] private bool useCoinPostfix = true;

        public override TrackableValue<float> FindTrackable() => this.FindLazy<GameDatabase>().Money;

        protected override string ValueToText(float value)
        {
            if (useCoinPostfix)
            {
                return CMath.ConvertPostfix(value);
            }
            else
            {
                return value.ToString();
            }
        }
    }
}
