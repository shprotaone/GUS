using GUS.Objects;
using System;
using UnityEngine;

namespace GUS.Core.Pool
{
    [Serializable]
    public struct ObjectInfo
    {        
        public GameObject prefab;  
        public PoolObjectType type;
        public int startCount;
    }
}

