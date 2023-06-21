using DG.Tweening;
using GUS.Core.Data;
using GUS.Core.Locator;
using GUS.Core.SaveSystem;
using GUS.Objects.PowerUps;
using System.Collections.Generic;
using UnityEngine;

namespace GUS.Core.Hub.BonusShop
{
    public class ShopSystem :MonoBehaviour
    {       
        [SerializeField] private List<Collectable> _collectables;

        private Wallet _wallet;
        private HonkCoinWallet _honkWallet;
        private UIShop _shopView;
        private StorageService _storageService;
        private List<BonusData> _bonusData;
        private AudioService _audioService;
        public Wallet Wallet => _wallet;

        public void Init(IServiceLocator serviceLocator)
        {
            _wallet = serviceLocator.Get<Wallet>();
            _honkWallet= serviceLocator.Get<HonkCoinWallet>();
            _storageService = serviceLocator.Get<StorageService>();         
            _audioService= serviceLocator.Get<AudioService>();
            _shopView = serviceLocator.Get<UiHubController>().UIShop;

            LoadList();
            _shopView.InitSlots(this,_collectables, _bonusData);
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

                DOVirtual.DelayedCall(1,() => _shopView.UpdateSlots(_collectables, _bonusData));
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

            if (powerUpEnum == PowerUpEnum.HonkCoin)
            {
                _honkWallet.AddCoin(1);
                _honkWallet.UpdateData();
            }
            else
            {
                _shopView.UpdateSlots(_collectables, _bonusData);
            }

            _audioService.PlaySFX(_audioService.Data.buySound);
            _storageService.Save();
            
        }
    }
}