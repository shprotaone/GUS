using GUS.Core;
using GUS.Core.Clicker;
using GUS.Core.Locator;
using GUS.Core.Pool;
using GUS.Core.SaveSystem;
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
        private const int countStartPlatform = 2;
        public const int RangeZ = -50;

        private Transform _beginWorldTransform;
        private Vector3 _offset = new Vector3(0, 0, -20);
                
        private GameObject _nextPlatform;
        private Platform _lastPlatform;
        private Vector3 _lastPos;

        private RandomLogic _randomLogic;
        private Queue<Platform> _platformsQueue;
        
        private ObjectPool _platformPool;
        private ObjectPool _collectablesPool;
        private SpecialPlatformBuilder _specialPlatformBuilder;
        private RunLocator _runLocator;
        private BonusSpawner _bonusSpawner;

        private int _currentPlatformNumber = 0;
        private int _platformsCount = 3;
       
        private bool _isTutorial;
        private bool _isFree;
        private bool _rewardPlatform;

        private IServiceLocator _serviceLocator;
        public ClickerGame NextClickerGame{ get;private set; }
        public PlatformBuilder(Transform startPosition, IServiceLocator serviceLocator)
        {            
            _specialPlatformBuilder = serviceLocator.Get<SpecialPlatformBuilder>();
            _serviceLocator = serviceLocator;

            _runLocator = _serviceLocator.Get<RunLocator>();
            _platformPool = _runLocator.GetPool(PoolTypeEnum.Platform);
            _collectablesPool = _runLocator.GetPool(PoolTypeEnum.Collectable);
            _isTutorial = _serviceLocator.Get<StorageService>().Data._tutorialSteps[1];

            _bonusSpawner = new BonusSpawner(_collectablesPool,
                                             serviceLocator.Get<BonusSpawnCatcher>());

            _platformsQueue = new Queue<Platform>();
            _randomLogic = new RandomLogic(_platformPool);

            _beginWorldTransform = startPosition;
        }

        public void CreateStartSection()
        {

            if (!_isTutorial)
            {               
                _isFree = true;
                CreateNextPlatform();
                CreateNextPlatform();
                _isFree = false;
                CreateNextPlatform();
            }

            _isFree = true;
            CreateNextPlatform();

            _isFree = false;
            for (int i = 0; i < countStartPlatform; i++)
            {
                CreateNextPlatform();
            }
        }

        public void CreateNextPlatform()
        {
            float nextPlatformOffset;
            
            if (_platformsQueue.Count < _platformsCount)
            {
                SetNextPlatform();

                Platform currentPlatform = _nextPlatform.GetComponent<Platform>();
                currentPlatform.SetBonus(_bonusSpawner);

                if (_lastPlatform == null)
                {
                    _lastPos = _beginWorldTransform.position + _offset;
                    _lastPlatform = currentPlatform;
                }
                else
                {
                    _lastPos = _lastPlatform.EndPoint.position;
                }
                
                Vector3 instantiatePos = _lastPos;
                _nextPlatform.transform.SetParent(_beginWorldTransform);
                nextPlatformOffset = currentPlatform.PlatformLenght;
                _nextPlatform.transform.position = instantiatePos + new Vector3(0, 0, nextPlatformOffset);

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
            else if (_rewardPlatform)
            {
                _nextPlatform = _platformPool.GetObject(PoolObjectType.AfterClicker);
                _rewardPlatform = false;
            }
            else if(_specialPlatformBuilder.Find(_currentPlatformNumber, out PoolObjectType specialType))
            {
                CallSpecialPlatform(_currentPlatformNumber, specialType, true, true);
            }
            else
            {
                NextObstaclePlatform();        
            }
        }

        private void CallSpecialPlatform(int platformCount, PoolObjectType specialType, bool withReward, bool freeZone)
        {
            _nextPlatform = _platformPool.GetObject(specialType);
            _rewardPlatform = withReward;
            _isFree = freeZone;
        }

        private void NextObstaclePlatform()
        {
            int index = _randomLogic.GetDigit();
            PoolObjectType type = _randomLogic.Parts[index].ObjectType;

            if (type == _lastPlatform.Type)
            {
                NextObstaclePlatform();
            }
            else
            {
                _nextPlatform = _platformPool.GetObject(type);
                _currentPlatformNumber++;
                OnPlatformAdded?.Invoke(_currentPlatformNumber);
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
                    platform.DisableBonus(_collectablesPool);
                    _platformPool.DestroyObject(platform.gameObject);                   
                }
            }

            _currentPlatformNumber = 0;
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

