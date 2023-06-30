using GUS.Core.Data;
using GUS.Core.Locator;
using System.Collections.Generic;
using UnityEngine;

namespace GUS.Core.SaveSystem
{
    public class StorageService
    {
        private const string fileName = "Data";

        private JsonToFirebase _firebase;
        private PlayerData _playerData;
        private IStorageService _storageService;

        public PlayerData Data => _playerData;

        public void Init(IServiceLocator serviceLocator)
        {
            _firebase = serviceLocator.Get<JsonToFirebase>();
            _storageService = new JsonToFileStorageService();

            Load();
        }

        public void Save()
        {
            _storageService.Save(fileName, _playerData);
            _firebase.Save(_playerData);
        }

        public void Load()
        {
            _storageService.Load<PlayerData>(fileName, data =>
            {
                _playerData = data;
            });

            if(_playerData == null || !CheckSave())
            {
                _playerData = new PlayerData();
                _playerData.playerName = "Player" + (int)Random.Range(0, 500000);
                _playerData.coins = 0;
                _playerData.commonDistance = 0;
                _playerData.buildDatas = new List<BuildData>();
                _playerData.bonusDatas = new List<BonusData>();

                _playerData._tutorialSteps = new List<bool>();
                for(int i = 0; i< 3; i++)
                {
                    _playerData._tutorialSteps.Add(false);
                }

                _storageService.Save(fileName, _playerData);
            }
        }

        public void Delete()
        {
            _storageService.Delete(fileName);
            Load();
        }

        private bool CheckSave()
        {
            bool check = _playerData.buildDatas != null && 
                         _playerData.bonusDatas != null && 
                         _playerData._tutorialSteps != null;

            return check;
        }
    }

}

