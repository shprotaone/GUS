using GUS.Core.Pool;
using System;

namespace GUS.LevelBuild
{
    [Serializable]
    public class SpecialPlatformKey
    {
        public int platformIndex;
        public PoolObjectType type;
    }
}