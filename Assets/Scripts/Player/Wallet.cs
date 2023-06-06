using GUS.Core.Locator;
using GUS.Core.SaveSystem;
using GUS.Core.UI;
using System;

namespace GUS.Core.Data
{
    public class Wallet : IData
    {
        private StorageService _storageService;
        private ICoinView _coinView;

        private int _coins;
        private int _multiply = 1;

        public int Coins => _coins;
        public StorageService StorageService => _storageService;

        public void Init(IServiceLocator locator)
        {
            _coinView = locator.Get<ICoinView>();
            _storageService = locator.Get<StorageService>();
        }

        public void AddOne()
        {
            _coins += 1 * _multiply;
            _coinView.RefreshCoinsCount(_coins);
        }

        public void AddCoins(int reward)
        {
            _coins += reward;
            _coinView.RefreshCoinsCount(_coins);
        }

        public void Save()
        {
            _storageService.Data.coins += _coins;
            _storageService.Save();
        }

        public void ResetCounter()
        {
            _coins = 0;
            _coinView.RefreshCoinsCount(_coins);
        }

        public void SetMultiply(bool flag)
        {
            if (flag) _multiply = 2;
            else _multiply = 1;
        }
    }
}

