﻿using GUS.Core.Locator;
using GUS.Core.UI;
using UnityEngine;

namespace GUS.Core
{
    public class UiHubController : MonoBehaviour
    {
        [SerializeField] private UIMainHub _uiHub;
        [SerializeField] private UISettings _uiSettings;
        [SerializeField] private UIBuild _uiBuild;
        [SerializeField] private UIShop _uiShop;
        [SerializeField] private UIExplore _uiExplore;
        [SerializeField] private CoinView _coinView;
        [SerializeField] private HonkCoinView _honkCoinView;

        public UIMainHub UIMainHub { get { return _uiHub; } }
        public UIBuild UIBuild { get { return _uiBuild;} }
        public UIShop UIShop { get { return _uiShop; } }
        public CoinView CoinView { get { return _coinView;} }
        public HonkCoinView HonkCoinView { get { return _honkCoinView;} }

        public void Init(IServiceLocator locator)
        {
            _uiHub.Init(locator);
            _uiSettings.Init(locator);
            _uiBuild.Init(locator);
            _uiExplore.Init(locator);
            _uiShop.Init(locator);
        }

        public void BuildActivate(bool flag) => _uiBuild.Activate(flag);
        public void SettingsActivate(bool flag) => _uiSettings.Activate(flag);
        public void ShopPanel(bool flag) => _uiShop.Activate(flag);
        public void MainPanel(bool flag) => _uiHub.Activate(flag);
        public void ExplorePanelActivate(bool flag) => _uiExplore.Activate(flag);

    }
}
