using GUS.Core.Data;
using GUS.Core.Locator;
using GUS.Core.SaveSystem;
using GUS.Core.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GUS.Core.Hub.BuildShop
{
    public class BuildsSystem : MonoBehaviour
    {
        public event Action OnBuyed;

        [SerializeField] private Build[] _builds;

        private Wallet _wallet;
        private StorageService _storageService;
        private DistanceMutiplier _distanceMutiplier;
        private AudioService _audioService;
        private UIBuild _uiBuild;

        private List<BuildData> _buildsData;

        public Build[] Builds => _builds;
        public Wallet Wallet => _wallet;
        public void Init(IServiceLocator serviceLocator)
        {
            _wallet = serviceLocator.Get<Wallet>();
            _storageService = serviceLocator.Get<StorageService>();
            _audioService = serviceLocator.Get<AudioService>();
            _uiBuild = serviceLocator.Get<UiHubController>().UIBuild;
            _distanceMutiplier= serviceLocator.Get<DistanceMutiplier>();
            _buildsData = _storageService.Data.buildDatas;

            LoadListBuild();
            UpdateBuilds();
            _uiBuild.InitSlots(this, _buildsData);
        }

        private void LoadListBuild()
        {
            if (_buildsData.Count == 0)
            {
                foreach (var build in _builds)
                {
                    _buildsData.Add(new BuildData(build.Container.buildName, 0));
                }
            }

        }
        private void UpdateBuilds()
        {
            for (int i = 0; i < _builds.Length; i++)
            {
                _builds[i].Init(this, _buildsData[i]);
            }
        }

        public void Buy(BuildNameEnum buildName, int cost)
        {
            int index = 0;
            if (_wallet.Coins < cost) return;

            _wallet.DecreaseCoins(cost);

            for (int i = 0; i < _buildsData.Count; i++)
            {
                if (_buildsData[i].nameEnum == buildName)
                {
                    index = i;
                    _buildsData[i].state++;
                    _builds[i].RefreshData();
                }
            }
            _uiBuild.Refresh(_builds[index], _buildsData[index], index);
            _distanceMutiplier.ChangeBonusAnimation();
            OnBuyed?.Invoke();

            _audioService.PlaySFX(_audioService.Data.buySound);
            _storageService.Save();
        }
    }
}

