using GUS.Core.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace GUS.LevelBuild
{
    public class SpecialPlatformBuilder : MonoBehaviour
    {
        [SerializeField] private List<SpecialPlatformKey> _specials;

        private int count = 0;

        public void SetTutorial()
        {
            _specials.Insert(0, new SpecialPlatformKey
            {
                platformIndex = 0,
                type = PoolObjectType.Tutorial
            });
        }

        public bool Find(int index, out PoolObjectType type)
        {
            if (_specials.Count > count && _specials[count].platformIndex == index)
            {
                type = _specials[count].type;
                count++;              
                return true;
            }

            type = PoolObjectType.Empty;
            return false;
        }

        public void ResetCount() => count = 0;
    }
}

