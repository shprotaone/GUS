using GUS.Core.Locator;
using GUS.Core.SaveSystem;
using GUS.Core.UI;

namespace GUS.Core.Data
{
    public class DistanceData :IData
    {
        private StorageService _storageService;
        private IDistanceView _distanceView;
        private float _distance;

        public StorageService StorageService => _storageService;

        public void Init(IServiceLocator serviceLocator)
        {
            _storageService = serviceLocator.Get<StorageService>();
            _distanceView = serviceLocator.Get<IDistanceView>();
        }

        public void Reset()
        {
            _distance = 0;
            _distanceView.RefreshDistancePointCount(_distance);
        }

        public void Set(int distance)
        {
            _distance = (distance);
            _distanceView.RefreshDistancePointCount(_distance);
        }

        public void Save()
        {
            _storageService.Data.commonDistance += _distance;
            _storageService.Save();
        }
    }
}
