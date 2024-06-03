using CKB.Core;
using UnityEngine;
using static CKB.Utilities.CommonGameplayFacade;

namespace CKB
{
    public class Opponent : Unit
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!StateMachine.Last.Is<PlayingState>()) return;

            if (other.CompareTag("Opponent"))
            {
                var opponent = other.GetComponent<Opponent>();

                if (group.Level >= opponent.Level)
                {
                    Grow(GameData.PointChick);
                    opponent.Group.Retire();
                }
            }
            else if (other.CompareTag("Player"))
            {
                var player = other.GetComponent<Player>();

                if (group.Level > player.Level)
                {
                    Grow(GameData.PointChicken);
                    player.Group.Retire();
                }
            }

            if (other.CompareTag("OpponentChick") || other.CompareTag("PlayerChick"))
            {
                var chick = other.GetComponent<Chick>();
                if (chick.Group == Group) return;

                Grow(GameData.PointChick);
                chick.Die();
            }
        }
    }
}