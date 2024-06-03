using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using CKB.Utilities;

namespace CKB.Core
{
    public class Pool : SmartScript
    {
        [SerializeField] private PoolType poolType;
        public PoolType Type => poolType;
        private Queue<PoolMember> _instances = new();

        public void Init(PoolType type = null)
        {
            if (type != null)
            {
                poolType = type;
            }

            FillPoolWithEmpties();
        }

        private void FillPoolWithEmpties()
        {
            for (var i = 0; i < poolType.size; i++)
            {
                SpawnPoolMember();
            }
        }

        private void SpawnPoolMember()
        {
            var newObject = Instantiate(poolType.prefab, transform.position, Quaternion.identity, transform);
            newObject.gameObject.Off();
            _instances.Enqueue(newObject);
        }

        private void Update()
        {
            var activeCount = _instances.Count(c => !c.gameObject.activeSelf);
            var allCount = poolType.size;
        }

        public GameObject Spawn(Vector3 position)
        {
            if (_instances.Count == 0)
            {
                SpawnPoolMember();
            }

            var effect = _instances.Dequeue();
            _instances.Enqueue(effect);

            effect.DOKill();
            effect.transform.position = position;
            effect.gameObject.On();

            effect.HideAfter(this, poolType.lifetime);

            return effect.gameObject;
        }

        public void HideEffect(PoolMember effectToHide)
        {
            effectToHide.gameObject.Off();
        }
    }
}
