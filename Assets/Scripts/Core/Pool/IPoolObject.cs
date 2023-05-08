using UnityEngine;
using UnityEngine.Pool;

namespace GUS.Core.Pool
{
    public interface IPoolObject
    {
        PoolObjectType Type { get; }
    }
}