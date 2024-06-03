using UnityEngine;
using CKB;

public class Wall : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Opponent"))
        {
            var unit = other.GetComponent<Unit>();
            unit.Group.StartStun();

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 knockbackDirection = unit.Group.MoveDirection.normalized;
                rb.AddForce(knockbackDirection * 20, ForceMode.Impulse);
            }
        }
    }
}
