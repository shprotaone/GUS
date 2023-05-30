using GUS.Core.Pool;
using GUS.LevelBuild;
using GUS.Objects;
using GUS.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.TestSystems
{
    public class PlatformGeneratorTest : MonoBehaviour
    {
        [SerializeField] private Button _generateButton;
        [SerializeField] private PoolObjectStorage _platformStorage;
        [SerializeField] private PoolObjectStorage _collectableStorage;
        [SerializeField] private ObjectPool _platformPool;
        [SerializeField] private ObjectPool _collectablesPool;

        private List<ObjectInfo> _platformObjects;
        private List<ObjectInfo> _collectablesObject;

        private GameObject _currentPlatform;
        private GameObject _currentCollectable;
        private RandomLogic _platformRandomLogic;

        private BonusSpawner _bonusSpawner;

        void Start()
        {
            InitObjects();
            
            _generateButton.onClick.AddListener(Generate);
            _platformRandomLogic = new RandomLogic(_platformPool);
            
            _bonusSpawner = new BonusSpawner(_collectablesPool);
        }

        private void InitObjects()
        {
            _platformPool.InitPool(_platformStorage);
            _collectablesPool.InitPool(_collectableStorage);
        }

        private void Generate()
        {
            if(_currentPlatform != null)
            {
                _platformPool.DestroyObject(_currentPlatform);              
            }

            if(_currentCollectable != null)
            {
                _collectablesPool.DestroyObject(_currentCollectable);
            }
            SetNextPlatform();
        }

        private void SetNextPlatform()
        {
            int index = _platformRandomLogic.GetDigit();
            _currentPlatform = _platformPool.GetObject(_platformStorage.parts[index].ObjectType);
            SetBonus();
        }

        private void SetBonus()
        {
            Platform platform = _currentPlatform.GetComponent<Platform>();
            if(platform.SpawnPoints.Count > 0)
            {
                Vector3 pos = _bonusSpawner.GetPos(platform.SpawnPoints);
                ObjectInfo objInfo = _bonusSpawner.GetTypeBonus();

                if (objInfo.ObjectType != PoolObjectType.Empty)
                {
                    _currentCollectable = _collectablesPool.GetObject(objInfo.ObjectType);
                    _currentCollectable.transform.position = pos;
                }
                else
                {
                    Debug.Log("Пустой объект");
                }
            }              
        }
    }
}

