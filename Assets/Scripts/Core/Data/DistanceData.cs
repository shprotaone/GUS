using GUS.Core.Locator;
using GUS.Core.SaveSystem;
using GUS.Core.UI;
using UnityEngine;

namespace GUS.Core.Data
{
    public class DistanceData : IData
    {
        private StorageService _storageService;
        private DistanceMutiplier _distanceMutiplier;
        private IDistanceView _distanceView;
        private float _value;

        public StorageService StorageService => _storageService;
        public float Value => _value;
        public void Init(IServiceLocator serviceLocator)
        {
            _storageService = serviceLocator.Get<StorageService>();
            _distanceView = serviceLocator.Get<IDistanceView>();
            _distanceMutiplier = serviceLocator.Get<DistanceMutiplier>();
        }

        public void Reset()
        {
            _value = 0;
            _distanceView.RefreshDistancePointCount(_value);
        }

        public void Set(int distance)
        {
            _value = distance * _distanceMutiplier.ResultMulty;
            _distanceView.RefreshDistancePointCount(_value);
        }

        public void UpdateData()
        {
            _storageService.Data.commonDistance += _value;
            _storageService.Save();
        }
    }
}
