using GUS.Core.Pool;
using GUS.Utils;
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

