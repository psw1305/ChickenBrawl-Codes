using UnityEngine;
using CKB.Utilities;
using static CKB.Utilities.CommonGameplayFacade;

namespace CKB
{
    public class OpponentGroup : Group
    {
        [Header("Opponent Group")]
        [SerializeField] private float rotateSpeed = 5f;

        private GameObject closest;
        private float searchInterval = 1f;
        private float lastSearchTime;

        protected override void Start()
        {
            Level = Progress.StepEnemyLevel();
            Nickname = CName.GetRandomName();

            base.Start();
        }

        public override void SetMoveSpeed(float moveSpeed)
        {
            this.moveSpeed = moveSpeed;
        }

        public void SetPosition(Vector3 startPosition)
        {
            unit.transform.position = startPosition;
        }

        public void Init()
        {
            animator.SetBool("IsWalking", true);

            if (Level >= 200)
            {
                SetMoveSpeed(4.0f);
            }
            else if (Level >= 50)
            {
                SetMoveSpeed(3.0f);
            }
        }

        #region State

        protected override void OnGamePlay()
        {
            Init();
        }

        #endregion

        protected override void MoveChicken()
        {
            base.MoveChicken();

            if (Time.time - lastSearchTime >= searchInterval)
            {
                FindClosestObject();
                lastSearchTime = Time.time;
                searchInterval = CMath.RandomFloat(new Vector2(1.0f, 3.0f));
            }

            if (closest != null)
            {
                var targetRotation = Quaternion.LookRotation(closest.transform.position - unit.transform.position);
                unit.transform.rotation = Quaternion.Slerp(unit.transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            }
        }

        private void FindClosestObject()
        {
            var findObjects = GameObject.FindGameObjectsWithTag("XP");
            var closestDistance = Mathf.Infinity;

            foreach (var findObject in findObjects)
            {
                float distance = Vector3.Distance(unit.transform.position, findObject.transform.position);
                if (distance < closestDistance)
                {
                    closest = findObject;
                    closestDistance = distance;
                }
            }
        }

        public override void AddChick()
        {
            if (chickCount >= GameData.ChickMax) return;
            chickCount++;

            var chick = Instantiate(GameData.OpponentChick, chickSpawn.position, Quaternion.identity, flockBox).GetComponent<Chick>();
            chick.Group = this;
        }

        public override void Retire()
        {
            var childCount = flockBox.childCount;

            for (int i = 0; i < childCount; i++)
            {
                var chick = flockBox.GetChild(0).GetComponent<Chick>();
                chick.ChickIdle(Progress.ChickRest);
            }

            ObjectPool.Spawn(GameData.UnitDeathVFX, unit.transform.position);

            Rank.RemoveRankingGroup(this);
            OpponentSpawn.Spawn();
            Destroy(gameObject);
        }
    }
}
