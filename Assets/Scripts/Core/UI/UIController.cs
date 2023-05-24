using GUS.Core.Locator;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private UIPause _uiPause;
        [SerializeField] private UiHubController _uiHubController;
        [SerializeField] private UIEndGame _uiEndGame;
        [SerializeField] private UIInGame _uiInGame;
        [SerializeField] private UIStartGame _uiStartGame;

        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private GameObject _clickerPanel;

        private GameStateController _controller;

        public UIStartGame UIStartGame => _uiStartGame;
        public UIInGame UiInGame => _uiInGame;
        public UIEndGame UIEndGame => _uiEndGame;

        public void Init(IServiceLocator serviceLocator)
        {
            _controller = serviceLocator.Get<GameStateController>();
            _uiInGame.Init(_controller);
            _uiEndGame.Init(serviceLocator);
            _uiPause.Init(_controller);
        }

        public void InitHub(IServiceLocator serviceLocator)
        {
            _controller = serviceLocator.Get<GameStateController>();
            _uiHubController.Init(serviceLocator);
        }


        public void PausePanel(bool flag)
        {
            _pausePanel.SetActive(flag);
        }

        public Slider GetClickerSlider()
        {
            return _clickerPanel.GetComponentInChildren<Slider>();
        }

        public void HPSliderActivate(bool flag)
        {
            _clickerPanel.SetActive(flag);
        }
    }
}

