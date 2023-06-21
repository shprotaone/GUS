using GUS.Core.Locator;
using GUS.Core.SaveSystem;

namespace GUS.Core.Data
{
    public class HonkCoinWallet : IData
    {
        private StorageService _storageService;
        private IHonkCoinView _view;

        private int _value;
        private int _valueInStorage;
        public int Value => _value;
        public int ValueInStorage => _valueInStorage;

        public StorageService StorageService => _storageService;

        public void Init(IServiceLocator serviceLocator)
        {
            _view = serviceLocator.Get<IHonkCoinView>();
            _storageService = serviceLocator.Get<StorageService>();

            _value = _storageService.Data.honkCoins;
            _view?.Refresh(_value);
        }

        public void AddCoin(int value)
        {
            _value += value;
            _view?.Refresh(_value);
        }

        public void RemoveCoin(int value)
        {
            _value -= value;
            _view?.Refresh(_value);
            UpdateData();
        }

        public void RemoveCoinFromData(int value)
        {
            _valueInStorage -= value;
            _storageService.Data.honkCoins = _valueInStorage;
            _storageService.Save();
        }

        public void AddCoinsToData()
        {
            _storageService.Data.honkCoins += _value;
            _storageService.Save();
        }

        public void ResetCounter()
        {
            _value = 0;
            _view?.Refresh(_value);
        }

        public void UpdateData()
        {
            _storageService.Data.honkCoins = _value;
            _storageService.Save();
        }

        public void LoadCurrentValue()
        {
            _valueInStorage = _storageService.Data.honkCoins;
        }
    }
}

