using UnityEngine;
using CKB;
using static CKB.Utilities.CommonGameplayFacade;

public class Xp : MonoBehaviour
{
    [SerializeField] private int level;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TakeXp(other);
        }
        else if (other.CompareTag("Opponent"))
        {
            TakeXp(other);
        }
    }

    private void TakeXp(Collider collider)
    {
        collider.GetComponent<Unit>().Grow(level);
        EggSpawn.Spawn();
        Destroy(gameObject);
    }
}
