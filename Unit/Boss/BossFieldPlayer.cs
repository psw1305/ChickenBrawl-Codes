using UnityEngine;
using CKB.Utilities;
using static CKB.Utilities.CommonGameplayFacade;

namespace CKB
{
    public class BossFieldPlayer : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float acceleration = 5f;

        private Rigidbody rb;
        private bool isRunning = false;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (isRunning)
            {
                rb.AddForce(Vector3.forward * acceleration, ForceMode.Acceleration);
            }
        }

        public void Run()
        {
            animator.SetBool("IsWalking", true);
            animator.speed = 2.0f;
            isRunning = true;
        }

        public void Stop()
        {
            isRunning = false;
            rb.velocity = Vector3.zero;
        }

        public void Die()
        {
            Audio.PlayOneShot(GameData.UnitDeathClip, 0.7f);
            CHaptic.HapticHeavy();

            gameObject.SetActive(false);
        }
    }
}
