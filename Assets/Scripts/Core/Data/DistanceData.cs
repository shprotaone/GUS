using GUS.Core.Locator;
using GUS.Core.SaveSystem;
using GUS.Core.UI;

namespace GUS.Core.Data
{
    public class DistanceData :IData
    {
        private StorageService _storageService;
        private IDistanceView _distanceView;
        private float _value;

        public StorageService StorageService => _storageService;
        public float Value => _value;
        public void Init(IServiceLocator serviceLocator)
        {
            _storageService = serviceLocator.Get<StorageService>();
            _distanceView = serviceLocator.Get<IDistanceView>();
        }

        public void Reset()
        {
            _value = 0;
            _distanceView.RefreshDistancePointCount(_value);
        }

        public void Set(int distance)
        {
            _value = (distance);
            _distanceView.RefreshDistancePointCount(_value);
        }

        public void UpdateCoins()
        {
            _storageService.Data.commonDistance += _value;
            _storageService.Save();
        }
    }
}
