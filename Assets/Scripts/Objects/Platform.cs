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
        [SerializeField] private bool _isSpawning = true;

        private GameObject _currentCollectable;
        public PoolObjectType Type => _objectPoolType;
        public List<Transform> SpawnPoints => _spawnPoints;
        public Transform EndPoint => _endPoint;
        public float PlatformLenght => Vector3.Distance(_beginPoint.position, _endPoint.position) / 2;

        public void SetBonus(BonusSpawner bonusSpawner)
        {           
            if (SpawnPoints.Count > 0 && _isSpawning)
            {
                Vector3 pos = bonusSpawner.GetPos(SpawnPoints);
                ObjectInfo obj = bonusSpawner.GetTypeBonus();
                _currentCollectable = bonusSpawner.GetObject(obj);

                if (obj.ObjectType != PoolObjectType.Empty)
                {
                    _currentCollectable.transform.SetParent(transform);
                    _currentCollectable.transform.position = pos;
                }
                else
                {
                    Debug.Log("Пустой объект");
                }
            }
        }

        public void DisableBonus(ObjectPool pool)
        {
            if(_currentCollectable != null)
            {
                Debug.Log("Удален " + _currentCollectable.GetType().Name);
                pool.DestroyObject(_currentCollectable);
                _currentCollectable = null;
            }
        }
    }
}