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
        public event Action<int> OnPlatformAdded;
        private const int countStartPlatform = 5;
        public const int RangeZ = -50;

        private Transform _startPosition;
        private Vector3 _offset = new Vector3(0, 0, -20);
                
        private GameObject _nextPlatform;
        private Platform _lastPlatform;
        private Vector3 _lastPos;

        private RandomLogic _randomLogic;
        private Queue<Platform> _platformsQueue;
        
        private ObjectPool _platformPool;
        private ObjectPool _collectablesPool;
        private SpecialPlatformBuilder _specialPlatformBuilder;
        private BonusSpawner _bonusSpawner;

        private int _platformCount = 0;
        private bool _isFree;

        public ClickerGame NextClickerGame{ get;private set; }
        public PlatformBuilder(Transform startPosition, IServiceLocator serviceLocator)
        {
            List<ObjectPool> pools = serviceLocator.GetAll<ObjectPool>().ToList();
            _specialPlatformBuilder = serviceLocator.Get<SpecialPlatformBuilder>();
            _platformPool = pools[0];   //небезопасно
            _collectablesPool = pools[1];

            _bonusSpawner = new BonusSpawner(_collectablesPool);
            _platformsQueue = new Queue<Platform>();
            _randomLogic = new RandomLogic(_platformPool.Storage.parts);

            _startPosition = startPosition;
        }

        public void CreateStartSection()
        {
            _isFree = true;
            for (int i = 0; i < countStartPlatform; i++)
            {
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
            
            if (_platformsQueue.Count < 7)
            {
                SetNextPlatform();

                Platform currentPlatform = _nextPlatform.GetComponent<Platform>();
                currentPlatform.SetBonus(_bonusSpawner);

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

        private void SetNextPlatform()
        {
            if (_isFree)
            {
                _nextPlatform = _platformPool.GetObject(PoolObjectType.Platform);
            }
            else if(_specialPlatformBuilder.Find(_platformCount, out PoolObjectType SpecialType))
            {
                _nextPlatform = _platformPool.GetObject(SpecialType);
                NextClickerGame = _nextPlatform.GetComponent<ClickerGame>();
                _isFree = true;
            }
            else
            {
                int index = _randomLogic.GetDigit();
                PoolObjectType type = _randomLogic.Parts[index].objectInfo.type;
                _nextPlatform = _platformPool.GetObject(type);
                _platformCount++;
                OnPlatformAdded?.Invoke(_platformCount);                           
            }
        }

        public void DeletePlatform()
        {
            if (_platformsQueue.Peek().transform.position.z < RangeZ)
            {
                _platformsQueue.Peek().DisableBonus(_collectablesPool);
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
            _platformCount = 0;
            _specialPlatformBuilder.ResetCount();
            _platformsQueue.Clear();
            _lastPlatform = null;
        }

        public void FreePlatformsMode(bool flag)
        {
            _isFree = flag;
        }
    }
}

