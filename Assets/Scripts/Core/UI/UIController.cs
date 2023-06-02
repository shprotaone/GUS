using GUS.Core.Locator;
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
        [SerializeField] private UIClickerGame _clickerGame;
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private GameObject _clickerPanel;      

        private GameStateController _controller;

        public UIStartGame UIStartGame => _uiStartGame;
        public UIInGame UiInGame => _uiInGame;
        public UIEndGame UIEndGame => _uiEndGame;
        public UIClickerGame ClickerGame => _clickerGame;

        public void Init(IServiceLocator serviceLocator)
        {
            _controller = serviceLocator.Get<GameStateController>();
            _uiInGame.Init(_controller);
            _uiEndGame.Init(serviceLocator);
            _uiPause.Init(_controller);
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

