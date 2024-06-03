using UnityEngine;
using CKB.Core;
using static CKB.Utilities.CommonGameplayFacade;

public class ItemAttractor : MonoBehaviour
{
    [SerializeField] private float strength = 1.5f;
    [SerializeField] private float range = 1.0f;

    public void SetRange()
    {
        range += (DB.ItemUpgradeIncreaseLevel.Value * GameData.MagnetIncreaseRate);
    }

    public void ActiveMagnet(bool isActive)
    {
        if (isActive)
        {
            range += 3.0f;
        }
        else
        {
            range -= 3.0f;
        }
    }

    private void FixedUpdate()
    {
        if (!StateMachine.Last.Is<PlayingState>()) return;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

        foreach(var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("XP"))
            {
                MoveTowardsPlayer(hitCollider.transform);
            }
        }
    }

    private void MoveTowardsPlayer(Transform item)
    {
        item.transform.position = Vector3.Lerp(item.transform.position, transform.position, strength * Time.deltaTime);
    }
}
