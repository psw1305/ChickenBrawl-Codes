using UnityEngine;
using CKB;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Opponent"))
        {
            TakeItem(other.GetComponent<Unit>().Group);
        }
    }

    protected virtual void TakeItem(Group group) { }
}
