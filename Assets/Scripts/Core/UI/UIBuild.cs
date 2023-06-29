using GUS.Core.Data;
using GUS.Core.GameState;
using GUS.Core.Hub;
using GUS.Core.Hub.BuildShop;
using GUS.Core.Locator;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.ObjectChangeEventStream;

namespace GUS.Core.UI
{
    public class UIBuild : MonoBehaviour
    {
        [SerializeField] private RectTransform _panel;
        [SerializeField] private BuildSlotView[] _views;
        [SerializeField] private Button _explore;
        [SerializeField] private Button _close;
        [SerializeField] private MultiplicatorViewer _multiplicatorViewer;

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
            _multiplicatorViewer.Init(serviceLocator);
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
            }

            Refresh(buildSystem, data);
        }

        public void Refresh(BuildsSystem buildSystem, List<BuildData> data)
        {
            for (int i = 0; i < _views.Length; i++)
            {
                _views[i].RefreshProgress(data[i].state, buildSystem.Builds[i].StepCount);

                if (data[i].state < buildSystem.Builds[i].Container.costs.Length)
                {
                    _views[i].SetCost(buildSystem.Builds[i].Container.costs[(int)data[i].state]);
                }
                else
                {
                    _views[i].Disable();
                }
            }
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

