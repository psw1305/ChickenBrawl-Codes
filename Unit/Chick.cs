using UnityEngine;
using CloudFine.FlockBox;

namespace CKB
{
    public class Chick : MonoBehaviour
    {
        [SerializeField] private GameObject xpPrefab;
        [SerializeField] private Animator animator;
        private Rigidbody rb;
        private PhysicsSteeringAgent agent;
       
        public Group Group { get; set; }

        public void Awake()
        {
            rb = GetComponent<Rigidbody>();
            agent = GetComponent<PhysicsSteeringAgent>();
            animator.SetBool("IsWalking", true);
        }

        public void ChickIdle(Transform parent)
        {
            transform.parent = parent;
            rb.velocity = Vector3.zero;
            agent.enabled = false;
            animator.SetBool("IsWalking", false);
        }

        public void Die()
        {
            Group.MinusChick();
            Destroy(gameObject);
        }
    }
}
