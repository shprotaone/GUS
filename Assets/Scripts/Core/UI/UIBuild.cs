using GUS.Core.GameState;
using GUS.Core.Hub;
using GUS.Core.Hub.BuildShop;
using GUS.Core.Locator;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core.UI
{
    public class UIBuild : MonoBehaviour
    {
        [SerializeField] private RectTransform _panel;
        [SerializeField] private BuildSlotView[] _views;
        [SerializeField] private Button _explore;
        [SerializeField] private Button _close;

        private HubStateController _controller;
        private CameraHubController _cameraController;
        private CoinView _coinView;
        private UiHubController _uiHubController;
        public void Init(IServiceLocator serviceLocator)
        {
            _controller = serviceLocator.Get<HubStateController>();
            _cameraController = serviceLocator.Get<ICamera>() as CameraHubController;
            _coinView = serviceLocator.Get<ICoinView>() as CoinView;
            _uiHubController = serviceLocator.Get<UiHubController>();
            _close.onClick.AddListener(Close);
            _explore.onClick.AddListener(Explore);
        }

        public void Activate(bool flag)
        {
            _panel.gameObject.SetActive(flag);
            _coinView.Activate(flag);
            _cameraController.MapCamera();
            _uiHubController.UIMainHub.UpPanelActivate(false);
        }

        public void InitSlots(BuildsSystem buildSystem, List<BuildData> data)
        {
            for (int i = 0; i < _views.Length; i++)
            {
                _views[i].Init(buildSystem, data[i]);
                Refresh(buildSystem.Builds[i], data[i], i);
            }
        }

        public void Refresh(Build builds, BuildData data, int index)
        {
            _views[index].RefreshProgress(data.state, builds.StepCount);
            _views[index].SetCost(builds.Container.costs[(int)data.state]);
        }

        private void Explore()
        {
            _controller.Explore();
            _panel.gameObject.SetActive(false);
            _coinView.Activate(false);
        }

        private async void Close()
        {
            await _cameraController.IdleCamera();
            _panel.gameObject.SetActive(false);
            _uiHubController.UIMainHub.UpPanelActivate(true);
            _coinView.Activate(false);
        }

    }
}

