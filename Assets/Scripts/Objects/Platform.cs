using GUS.Core.Pool;
using GUS.Player;
using System.Collections.Generic;
using UnityEngine;

namespace GUS.Objects
{
    public class Platform : MonoBehaviour, IPoolObject
    {
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private PoolObjectType _objectPoolType;
        [SerializeField] private Transform _beginPoint;
        [SerializeField] private Transform _endPoint;

        public PoolObjectType Type => _objectPoolType;
        public List<Transform> SpawnPoints => _spawnPoints;
        public Transform EndPoint => _endPoint;
        public float PlatformLenght => Vector3.Distance(_beginPoint.position, _endPoint.position) / 2;
    }
}