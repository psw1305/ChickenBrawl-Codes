using System.Collections.Generic;
using UnityEngine;
using CKB.Core;
using CKB.Utilities;
using static CKB.Utilities.CommonGameplayFacade;

public class PoolManager : SmartScript, ILazy
{
    [SerializeField] private List<Pool> _manualSetupPools;

    private List<PoolType> poolTypes = new();

    private Dictionary<PoolType, Pool> pools = new();

    private void Awake()
    {
        ObjectPool = this;

        foreach (PoolType poolType in poolTypes)
        {
            CreatePool(poolType);
        }

        foreach (Pool pool in _manualSetupPools)
        {
            pool.Init();
            pools.Add(pool.Type, pool);
        }
    }

    private void CreatePool(PoolType poolType)
    {
        if (pools.ContainsKey(poolType)) return;

        var newPoolGameObject = new GameObject
        {
            transform =
                {
                    localPosition = Vector3.zero,
                    parent = transform
                },

            name = poolType.poolName
        };

        var newPool = newPoolGameObject.AddComponent<Pool>();
        newPool.Init(poolType);

        pools.Add(poolType, newPool);
    }

    public GameObject Spawn(PoolType type, Vector3 position)
    {
        if (!pools.ContainsKey(type))
        {
            poolTypes.Add(type);
            CreatePool(type);
        }

        return pools[type].Spawn(position);
    }
}
