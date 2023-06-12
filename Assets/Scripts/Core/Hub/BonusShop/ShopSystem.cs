using GUS.Core.Data;
using GUS.Core.Locator;
using GUS.Core.SaveSystem;
using GUS.Objects.PowerUps;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GUS.Core.Hub.BonusShop
{
    public class ShopSystem :MonoBehaviour
    {
        [SerializeField] private TMP_Text _coins;
        [SerializeField] private List<ShopSlotView> _slotView;
        [SerializeField] private List<Collectable> _collectables;

        private Wallet _wallet;
        private StorageService _storageService;
        private List<BonusData> _bonusData;
        private AudioService _audioService;
        public Wallet Wallet => _wallet;

        public void Init(IServiceLocator serviceLocator)
        {
            _wallet = serviceLocator.Get<Wallet>();
            _storageService = serviceLocator.Get<StorageService>();
            _coins.text = _wallet.Coins.ToString();
            _audioService= serviceLocator.Get<AudioService>();

            LoadList();
            InitSlots();         
        }

        private void InitSlots()
        {
            for (int i = 0; i < _collectables.Count; i++)
            {
                 _slotView[i].Init(this, _collectables[i], _bonusData[i]);
            }
        }

        private void UpdateSlots() 
        {
            for (int i = 0; i < _collectables.Count; i++)
            {
                _slotView[i].UpdateData(_bonusData[i]);
            }
        }

        private void LoadList()
        {
            _bonusData = _storageService.Data.bonusDatas;

            if (_bonusData.Count == 0)
            {
                _bonusData = new List<BonusData>();
                for (int i = 0; i < _collectables.Count; i++)
                {
                    _bonusData.Add(new BonusData(_collectables[i].powerUpEnum,
                                    _collectables[i].value[0], 0));
                }
                _storageService.Data.bonusDatas = _bonusData;
                _storageService.Save();
            }
        }

        public void Buy(PowerUpEnum powerUpEnum,int cost, int powerTime)
        {
            if (_wallet.Coins < cost) return;

            _wallet.DecreaseCoins(cost);
           
            foreach (var bonus in _bonusData)
            {
                if(powerUpEnum == bonus.powerUp)
                {
                    bonus.Increase(1,powerTime);   
                    
                }
            }

            _audioService.PlaySFX(_audioService.Data.buySound);
            _coins.text = _wallet.Coins.ToString();
            _storageService.Save();
            UpdateSlots();
        }
    }
}