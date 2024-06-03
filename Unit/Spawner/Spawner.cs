using CKB.Core;
using UnityEngine;
using static CKB.Utilities.CommonGameplayFacade;

namespace CKB
{
    public class Spawner : GameStateMachineUser
    {
        [SerializeField] protected int maxAmount = 10;

        protected void SpawnObject(GameObject spawnObject, Vector3 spawnPosition)
        {
            var clone = Instantiate(spawnObject, spawnPosition, Quaternion.identity, transform);

            if (GroupPlayer.Level >= 200)
            {
                clone.transform.localScale = new Vector3(2f, 2f, 2f);
            }
            else if (GroupPlayer.Level >= 50)
            {
                clone.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
        }

        protected Vector3 RandomPosition(float range)
        {
            var xPos = Random.Range(-range, range);
            var zPos = Random.Range(-range, range); ;
            return new Vector3(xPos, 0, zPos);
        }
    }
}

