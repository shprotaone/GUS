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
        [SerializeField] private UIClickerGame _uiClickerGame;
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private SaveMe _saveMePanel;

        private GameStateController _controller;

        public UIStartGame UIStartGame => _uiStartGame;
        public UIInGame UiInGame => _uiInGame;
        public UIEndGame UIEndGame => _uiEndGame;
        public UIClickerGame ClickerGame => _uiClickerGame;

        public void Init(IServiceLocator serviceLocator)
        {
            _controller = serviceLocator.Get<GameStateController>();
            _uiInGame.Init(_controller);
            _uiEndGame.Init(serviceLocator);
            _uiPause.Init(_controller,serviceLocator);
            _uiClickerGame.Init(serviceLocator);
            _saveMePanel.Init(serviceLocator);
        }

        public void PausePanel(bool flag)
        {
            _pausePanel.SetActive(flag);
            _uiPause.Show();
        }

        public void SaveMe(bool flag)
        {
            if(flag) 
            { 
                _saveMePanel.gameObject.SetActive(true);
                _saveMePanel.Execute();
            }
        }
    }
}

