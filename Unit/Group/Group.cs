using UnityEngine;
using CKB.Core;
using static CKB.Utilities.CommonGameplayFacade;

namespace CKB
{
    public class Group : GameStateMachineUser
    {
        public string Nickname { get; set; }
        public int Level { get; set; }
        public Vector3 MoveDirection { get; set; }

        [SerializeField] protected Unit unit;
        [SerializeField] protected Animator animator;
        [SerializeField] protected Transform flockBox;
        [SerializeField] protected Transform chickSpawn;
        [SerializeField] protected GameObject unitCanvas;
        [SerializeField] protected float moveSpeed = 1.5f;

        [Header("VFX")]
        [SerializeField] private GameObject crownModel;
        [SerializeField] private ParticleSystem growVFX;
        [SerializeField] private ParticleSystem dashVFX;
        [SerializeField] private ParticleSystem stunVFX;

        protected int chickCount = 0;

        protected bool isDash = false;
        protected float dashTime = -1;

        protected bool isStun = false;
        protected float stunTime = -1;

        public GameObject GetUnit()
        {
            return unit.gameObject;
        }

        public void SetCrownActive(bool isActive)
        {
            crownModel.SetActive(isActive);
        }

        public virtual void SetMoveSpeed(float moveSpeed) { }

        protected virtual void Start()
        {
            MoveDirection = unit.transform.forward;

            Rank.Groups.Add(this);
            unit.SetUnit();
            SetCrownActive(false);
        }

        protected virtual void FixedUpdate() 
        {
            if (!StateMachine.Last.Is<PlayingState>()) return;

            DashCheck();
            StunCheck();
            MoveChicken();
        }

        #region Chicken State Methods

        protected virtual void MoveChicken() 
        {
            if (isStun) return;

            if (isDash)
            {
                unit.transform.position += 2.0f * moveSpeed * Time.deltaTime * MoveDirection;
            }
            else
            {
                unit.transform.position += moveSpeed * Time.deltaTime * MoveDirection;
            }
        }

        public void StartDash()
        {
            isDash = true;
            dashTime = 5.0f;
            animator.SetBool("IsRunning", true);
            dashVFX.Play();
        }

        private void DashCheck()
        {
            if (dashTime >= 0.0f)
            {
                dashTime -= Time.deltaTime;
            }
            else
            {
                isDash = false;

                dashTime = -1;
                animator.SetBool("IsRunning", false);
                dashVFX.Stop();
            }
        }

        public void StartStun()
        {
            isStun = true;
            MoveDirection = -unit.transform.forward;

            stunTime = 1.0f;
            stunVFX.Play();
        }

        private void StunCheck()
        {
            if (stunTime >= 0.0f)
            {
                stunTime -= Time.deltaTime;
            }
            else
            {
                isStun = false;
                MoveDirection = unit.transform.forward;

                stunTime = -1;
                stunVFX.Stop();
            }
        }

        #endregion

        public virtual void AddChick() { }

        public void MinusChick()
        {
            chickCount--;
        }

        public void GrowVFX()
        {
            growVFX.Play();
        }

        public virtual void Retire() { }
    }
}
