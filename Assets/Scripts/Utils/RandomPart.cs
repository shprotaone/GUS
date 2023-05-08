using GUS.Core.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUS.Utils
{
    [Serializable]
    public class RandomPart 
    {
        public ObjectInfo objectInfo;
        public int _weight;

        public int Weight => _weight;

    }
}

