using UnityEngine;

namespace CKB.Core
{
    [CreateAssetMenu(fileName = "PoolType", menuName = "SO/PoolType")]
    public class PoolType : ScriptableObject
    {
        public string poolName;
        public PoolMember prefab;
        public int size;
        public int lifetime;
    }
}
