using GUS.Core.Locator;
using GUS.Core.Pool;
using GUS.Objects;
using GUS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GUS.LevelBuild
{
    public class PlatformBuilder
    {
        private const int countStartPlatform = 2;
        public const int RangeZ = -50;

        private Transform _startPosition;
        private Vector3 _offset = new Vector3(0, 0, -20);
                
        private GameObject _nextPlatform;
        private GameObject _currentCollectable;
        private Platform _lastPlatform;
        private Vector3 _lastPos;

        private RandomLogic _randomLogic;
        private Queue<Platform> _platformsQueue;
        
        private ObjectPool _platformPool;
        private ObjectPool _collectablesPool;
        private BonusSpawner _bonusSpawner;
        private bool _isFree;
        public PlatformBuilder(Transform startPosition, IServiceLocator serviceLocator)
        {
            List<ObjectPool> pools = serviceLocator.GetAll<ObjectPool>().ToList();
            _platformPool = pools[0];   //небезопасно
            _collectablesPool = pools[1];

            _bonusSpawner = new BonusSpawner();
            _platformsQueue = new Queue<Platform>();
            _randomLogic = new RandomLogic(_platformPool.Storage.parts);

            _startPosition = startPosition;
        }

        public void CreateStartSection()
        {
            _isFree = true;
            for (int i = 0; i < countStartPlatform; i++)
            {
                SetNextFreePlatform();
                CreateNextPlatform();
            }

            _isFree = false;
            for (int i = 0; i < countStartPlatform + 5; i++)
            {
                CreateNextPlatform();
            }
        }

        public void CreateNextPlatform()
        {
            float nextPlatformOffset;

            if (_platformsQueue.Count < 15)
            {
                if(!_isFree) SetNextPlatform();

                Platform currentPlatform = _nextPlatform.GetComponent<Platform>();

                if (_lastPlatform == null)
                {
                    _lastPos = _startPosition.position + _offset;
                    _lastPlatform = currentPlatform;
                }
                else
                {
                    _lastPos = _lastPlatform.EndPoint.position;
                }
                
                Vector3 instantiatePos = _lastPos;
                _nextPlatform.transform.SetParent(_startPosition);
                nextPlatformOffset = currentPlatform.PlatformLenght;
                _nextPlatform.transform.position = instantiatePos + new Vector3(0,0,nextPlatformOffset);

                _platformsQueue.Enqueue(currentPlatform);
                _lastPlatform = currentPlatform;              
            }
        }

        private void SetNextFreePlatform()
        {
            _nextPlatform = _platformPool.GetObject(0); //небезопасно, переделать на поиск по типу
        }
        private void SetNextPlatform()
        {
            int index = _randomLogic.GetDigit();
            PoolObjectType type = _randomLogic.Parts[index].objectInfo.type;
            _nextPlatform = _platformPool.GetObject(type);
            SetBonus();
        }

        private void SetBonus()
        {
            Platform platform = _nextPlatform.GetComponent<Platform>();
           
            if (platform.SpawnPoints.Count > 0)
            {
                Vector3 pos = _bonusSpawner.GetPos(platform.SpawnPoints);
                ObjectInfo objInfo = _bonusSpawner.GetTypeBonus(_collectablesPool.Storage);

                if (objInfo.type != PoolObjectType.Empty)
                {
                    _currentCollectable = _collectablesPool.GetObject(objInfo.type);
                    _currentCollectable.transform.SetParent(platform.transform);
                    _currentCollectable.transform.position = pos;
                }
                else
                {
                    //Debug.Log("Пустой объект");
                }
            }
        }

        public void DeletePlatform()
        {
            if (_platformsQueue.Peek().transform.position.z < RangeZ)
            {
                _platformPool.DestroyObject(_platformsQueue.Peek().gameObject);
                _platformsQueue.Dequeue();
            }
        }

        public void ClearBuilder()
        {
            if(_platformsQueue.Count > 0)
            {
                foreach (var platform in _platformsQueue)
                {
                    _platformPool.DestroyObject(platform.gameObject);                   
                }
            }
            else
            {
                Debug.Log("Билдер пуст");
            }
            _platformsQueue.Clear();
            _lastPlatform = null;
        }
    }
}

