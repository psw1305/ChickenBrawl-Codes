using UnityEngine;
using CKB.Core;
using static CKB.Utilities.CommonGameplayFacade;
using CKB.Utilities;

namespace CKB
{
    public class PlayerGroup : Group
    {
        [Header("Player Group")]
        [SerializeField] private ItemAttractor itemAttractor;
        [SerializeField] private ParticleSystem magnetVFX;
        [SerializeField] private ParticleSystem levelUpVFX;

        private Joystick joystick;
        private bool isMagnet = false;
        private float magnetTime = -1;

        public Player Player => unit.GetComponent<Player>();

        public override void SetMoveSpeed(float moveSpeed)
        {
            this.moveSpeed = moveSpeed + (DB.SpeedUpIncreaseLevel.Value * GameData.SpeedIncreaseRate);
        }

        private void UpgradeVFX()
        {
            Audio.PlayOneShot(GameData.UpgradeClip);
            levelUpVFX.Play();
        }

        #region Player Upgrade
        
        public void LevelUp()
        {
            UpgradeVFX();

            Level = DB.StartIncreaseLevel.Value;
            unit.SetLevelText();
        }

        public void SpeedUp()
        {
            UpgradeVFX();

            SetMoveSpeed(1.5f);
        }

        public void ItemUp()
        {
            UpgradeVFX();
        }

        #endregion

        #region Player Boost

        public void MagnetBoost()
        {
            StartMagnet(10.0f);
        }

        public void SpeedBoost()
        {
            moveSpeed *= 2.0f;
        }

        public void LevelBoost()
        {
            Level += 10;
            unit.SetLevelText();
        }

        public void CoinsBoost(float coins)
        {
            DB.Money.Value += coins;
        }

        #endregion

        protected override void Start()
        {
            Nickname = "Player";

            Level = DB.StartIncreaseLevel.Value;
            SetMoveSpeed(moveSpeed);
            itemAttractor.SetRange();

            base.Start();
        }

        protected override void FixedUpdate()
        {
            if (!Progress.IsStarted) return;

            base.FixedUpdate();

            if (isMagnet)
            {
                MagnetCheck();
            }
        }

        #region State

        protected override void OnGamePlay()
        {
            joystick = FindObjectOfType<Joystick>();
            animator.SetBool("IsWalking", true);
        }

        protected override void OnGameFinish()
        {
            unitCanvas.SetActive(false);
        }

        #endregion

        public void StartMagnet(float time)
        {
            if (isMagnet) return;

            isMagnet = true;
            magnetTime = time;
            magnetVFX.Play();
            itemAttractor.ActiveMagnet(true);
        }

        private void MagnetCheck()
        {
            if (magnetTime >= 0.0f)
            {
                magnetTime -= Time.deltaTime;
            }
            else
            {
                isMagnet = false;
                magnetTime = -1;
                magnetVFX.Stop();
                itemAttractor.ActiveMagnet(false);
            }
        }

        protected override void MoveChicken()
        {
            base.MoveChicken();

            if (joystick.Direction != Vector3.zero)
            {
                unit.transform.rotation = Quaternion.LookRotation(joystick.Direction);
            }
        }

        public override void AddChick()
        {
            if (chickCount >= GameData.ChickMax) return;
            chickCount++;

            var chick = Instantiate(GameData.Chick, chickSpawn.position, Quaternion.identity, flockBox).GetComponent<Chick>();
            chick.Group = this;
        }

        public override void Retire()
        {
            Audio.PlayOneShot(GameData.UnitDeathClip, 0.7f);
            CHaptic.HapticHeavy();

            unit.gameObject.SetActive(false);
            joystick.gameObject.SetActive(false);

            if (Progress.ReviveCount != 0)
            {
                Progress.ShowReviveWindow();
            }
            else
            {
                StateMachine.Push(new GameOverState());
            }
        }

        public void Revive()
        {
            Progress.SetStarted();

            unit.gameObject.SetActive(true);
            unitCanvas.SetActive(true);
            joystick.gameObject.SetActive(true);
            animator.Play("Base Layer.idle");
            animator.SetBool("IsWalking", true);
            StartDash();
        }
    }
}
