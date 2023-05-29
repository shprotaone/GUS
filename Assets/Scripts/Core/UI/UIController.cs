using GUS.Core.Locator;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core.UI
{
    public class UIController : MonoBehaviour
    {
        [Header("In Game")]
        [SerializeField] private UIStartGame _uiStartGame;
        [SerializeField] private UIInGame _uiInGame;
        [SerializeField] private UIPause _uiPause;
        [SerializeField] private UIEndGame _uiEndGame;
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private GameObject _clickerPanel;

        [Header("In HUB")]
        [SerializeField] private UISettings _uiSettings;
        [SerializeField] private UiHubController _uiHubController;


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
            _uiSettings.Init(serviceLocator);
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

