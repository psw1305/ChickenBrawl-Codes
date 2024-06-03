using CKB.Core;
using CKB.Utilities;
using UnityEngine;
using static CKB.Utilities.CommonGameplayFacade;

namespace CKB
{
    public class Player : Unit
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!StateMachine.Last.Is<PlayingState>()) return;

            if (other.CompareTag("Opponent"))
            {
                var opponent = other.GetComponent<Opponent>();

                if (group.Level >= opponent.Level)
                {
                    Audio.PlayOneShot(GameData.UnitDeathClip, 0.7f);
                    CHaptic.HapticHeavy();
                    
                    Grow(GameData.PointChick);
                    opponent.Group.Retire();
                }
            }

            if (other.CompareTag("OpponentChick"))
            {
                var chick = other.GetComponent<Chick>();

                Grow(GameData.PointChick);
                chick.Die();
            }
        }

        public override void Grow(int addValue)
        {
            Audio.PlayOneShot(GameData.UnitGrowClip, 0.5f);

            base.Grow(addValue);

            if (Level >= 200)
            {
                Camera.main.ToOrthoSize(9);
                Group.SetMoveSpeed(4.0f);
            }
            else if (Level >= 50)
            {
                Camera.main.ToOrthoSize(6.5f);
                Group.SetMoveSpeed(3.0f);
            }
        }
    }
}
