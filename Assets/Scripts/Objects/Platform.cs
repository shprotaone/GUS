﻿using Cysharp.Threading.Tasks;
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
        [SerializeField] private int _countBonuses;

        private Transform _parent;
        private GameObject _currentCollectable;
        public PoolObjectType Type => _objectPoolType;
        public List<Transform> SpawnPoints => _spawnPoints;
        public Transform EndPoint => _endPoint;
        public float PlatformLenght => Vector3.Distance(_beginPoint.position, _endPoint.position) / 2;

        public async void SetBonus(BonusSpawner bonusSpawner)
        {
            for (int i = 0; i < _countBonuses; i++)
            {
                if (SpawnPoints.Count > 0 && _isSpawning)
                {
                    await FindPosition(bonusSpawner);
                    if(_parent!= null) SpawnBonus(bonusSpawner);
                }
            }            
        }

        private void SpawnBonus(BonusSpawner bonusSpawner)
        {
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
        private async UniTask FindPosition(BonusSpawner bonusSpawner)
        {
            //int counter = 0;


            //while (_parent == null)
            //{
            //    _parent = bonusSpawner.GetPos(SpawnPoints);      
            //    counter++;
            //    if (counter > 10) break;
            //}
            //Debug.Log("Сделано попыток" + counter);
            _parent = bonusSpawner.GetPos(SpawnPoints);
            await UniTask.Yield();
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

        private void OnDestroy()
        {
            Debug.Log("Destroy? " + gameObject.name);
        }
    }
}