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
        [SerializeField] private Button _toHubButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private GameObject _clickerPanel;

        private GameStateController _controller;

        public UIInGame UiInGame => _uiInGame;
        public UIEndGame UIEndGame => _uiEndGame;

        public void Init(IServiceLocator serviceLocator)
        {
            _controller = serviceLocator.Get<GameStateController>();
            _uiInGame.Init(_controller);
            _restartButton.onClick.AddListener(_controller.RestartGame);
            _toHubButton.onClick.AddListener(_controller.SceneLoadHandler);
            _resumeButton.onClick.AddListener(_controller.Resume);
            _uiEndGame.Init(serviceLocator);
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

