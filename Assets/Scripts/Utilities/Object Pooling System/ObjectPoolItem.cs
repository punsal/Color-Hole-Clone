using UnityEngine;

namespace Utilities.Object_Pooler_System
{
    [System.Serializable]
    public struct ObjectPoolItem {
        public GameObject objectToPool;
        public int amountToPool;
        public PoolItemType poolItemType;
    }
}