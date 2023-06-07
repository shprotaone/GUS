using GUS.Core.Data;
using GUS.Core.Locator;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core.UI
{
    public class UIPause :MonoBehaviour
    {
        [SerializeField] private TMP_Text _distanceText;
        [SerializeField] private Button _yesButton;
        [SerializeField] private Button _noButton;
        [SerializeField] private Button _settings;

        [SerializeField] private GameObject _settingsPanel;

        private GameStateController _controller;
        private DistanceData _distance;

        public void Show()
        {
            _distanceText.text = _distance.Value.ToString();
        }

        private void SettingsActivate()
        {
            _settingsPanel.SetActive(true);            
        }

        public void Init(GameStateController gameState,IServiceLocator serviceLocator)
        {
            _controller = gameState;
            _yesButton.onClick.AddListener(_controller.Result);
            _noButton.onClick.AddListener(_controller.Resume);
            _settings.onClick.AddListener(SettingsActivate);
            _distance = serviceLocator.Get<DistanceData>();
        }
    }
}