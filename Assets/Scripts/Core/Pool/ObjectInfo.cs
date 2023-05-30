using GUS.Objects;
using System;
using UnityEngine;

namespace GUS.Core.Pool
{
    [Serializable]
    public struct ObjectInfo
    {
        private PoolObjectType _objectType;
        
        public GameObject prefab;
        public int startCount;
        public int Weight;

        public PoolObjectType ObjectType => _objectType;

        public void Init()
        {
            prefab.TryGetComponent(out IPoolObject obj);
            _objectType = obj.Type;
        }
    }
}

