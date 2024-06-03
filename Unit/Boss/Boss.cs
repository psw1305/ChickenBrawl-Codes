using CKB.Utilities;
using UnityEngine;
using static CKB.Utilities.CommonGameplayFacade;

namespace CKB
{
    public class Boss : MonoBehaviour
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
                rb.AddForce(Vector3.back * acceleration, ForceMode.Acceleration);
            }
        }

        public void Run()
        {
            animator.SetBool("IsRunning", true);
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                BossBattle.BossBattleEnd();
            }
        }
    }
}
