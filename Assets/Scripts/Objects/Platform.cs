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

        private Transform _parent;
        private GameObject _currentCollectable;
        public PoolObjectType Type => _objectPoolType;
        public List<Transform> SpawnPoints => _spawnPoints;
        public Transform EndPoint => _endPoint;
        public float PlatformLenght => Vector3.Distance(_beginPoint.position, _endPoint.position) / 2;

        public void SetBonus(BonusSpawner bonusSpawner)
        {           
            if (SpawnPoints.Count > 0 && _isSpawning)
            {
                _parent = bonusSpawner.GetPos(SpawnPoints);
                ObjectInfo obj = bonusSpawner.GetTypeBonus();
                _currentCollectable = bonusSpawner.GetObject(obj);              

                if (obj.ObjectType != PoolObjectType.Empty)
                {
                    _currentCollectable.transform.SetParent(_parent);
                    _currentCollectable.transform.position = _parent.position;
                }
                else
                {
                    Debug.Log("Пустой объект");
                }
            }
        }

        public void DisableBonus(ObjectPool pool)
        {
            if (_parent == null) return;

            if(_parent.childCount > 0)
            {
                pool.DestroyObject(_currentCollectable);
                _currentCollectable = null;
            }
        }
    }
}