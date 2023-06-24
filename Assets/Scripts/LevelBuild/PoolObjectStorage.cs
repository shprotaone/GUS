using GUS.Core.Pool;
using System.Collections.Generic;
using UnityEngine;


namespace GUS.LevelBuild
{
    [CreateAssetMenu]
    public class PoolObjectStorage : ScriptableObject
    {
        public List<ObjectInfo> parts;
    }
}

