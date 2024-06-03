using UnityEngine;
using static CKB.Utilities.CommonGameplayFacade;

namespace CKB
{
    public class OpponentSpawner : Spawner
    {
        private void Awake()
        {
            OpponentSpawn = this;
        }

        protected override void OnGameReady()
        {
            for (int x = -2; x <= 2; x++)
            {
                for (int z = -2; z <= 2; z++)
                {
                    if (x == 0) continue;

                    var position = new Vector3(x * 4, 0, z * 4);
                    StartSpawn(position);
                }
            }
        }

        public void ProgressSpawn()
        {
            for (int i = 0; i < 2; i++)
            {
                Spawn();
            }
        }

        public void StartSpawn(Vector3 position)
        {
            var opponentGroup = Instantiate(GameData.Opponent).GetComponent<OpponentGroup>();
            opponentGroup.SetPosition(position);
        }

        public void Spawn()
        {
            var opponentGroup = Instantiate(GameData.Opponent).GetComponent<OpponentGroup>();
            opponentGroup.SetPosition(RandomPosition(20));
            opponentGroup.Init();
        }
    }
}
