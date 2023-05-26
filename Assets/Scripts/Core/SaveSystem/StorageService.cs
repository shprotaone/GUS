using GUS.Core.Locator;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace GUS.Core.SaveSystem
{
    public class StorageService
    {
        private JsonToFirebase _firebase;
        private PlayerData _playerData;
        private IStorageService _storageService;

        public PlayerData Data => _playerData;
        public StorageService(IServiceLocator serviceLocator)
        {
            _firebase = serviceLocator.Get<JsonToFirebase>();
            _storageService = new JsonToFileStorageService();

            Load();           
        }

        public void Save()
        {
            _storageService.Save("Data", _playerData);
            _firebase.Save(_playerData);
        }

        public void Load()
        {
            _storageService.Load<PlayerData>("Data", data =>
            {
                _playerData = data;
            });

            if(_playerData == null)
            {
                _playerData = new PlayerData();
                _playerData.playerName = "Player" + (int)Random.Range(0, 500000);
                _playerData.coins = 0;
                _playerData.commonDistance = 0;
                _storageService.Save("Data", _playerData);
            }
        }
    }

}

