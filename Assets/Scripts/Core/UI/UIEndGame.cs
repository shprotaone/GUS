using GUS.Core.Locator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core.UI
{
    public class UIEndGame : MonoBehaviour
    {
        [SerializeField] private GameObject _endGamePanel;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _toHubButton;

        private GameStateController _gameStateController;

        private void Start()
        {
            _restartButton.onClick.AddListener(Restart);
            _toHubButton.onClick.AddListener(ToHub);
        }

        public void Init(IServiceLocator serviceLocator)
        {
            _gameStateController = serviceLocator.Get<GameStateController>();
        }

        public void Panel(bool flag) => _endGamePanel.SetActive(flag);
        public void Restart() => _gameStateController.RestartGame();
        public void ToHub() => _gameStateController.SceneLoadToHub();
    }
}

