using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core.UI
{
    public class UIPause :MonoBehaviour
    {
        [SerializeField] private Button _toHubButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _settings;

        [SerializeField] private GameObject _settingsPanel;

        private GameStateController _controller;

        private void Start ()
        {
            _restartButton.onClick.AddListener(_controller.RestartGame);
            _toHubButton.onClick.AddListener(_controller.SceneLoadToHub);
            _resumeButton.onClick.AddListener(_controller.Resume);
            _settings.onClick.AddListener(PanelActivate);
        }

        private void PanelActivate() => _settingsPanel.SetActive(true);

        public void Init(GameStateController gameState)
        {
            _controller = gameState;
        }
    }
}