using GUS.Core.Data;
using GUS.Core.Locator;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core.UI
{
    public class UIEndGame : MonoBehaviour
    {
        [SerializeField] private Transform _panel;
        [SerializeField] private TMP_Text _distanceText;
        [SerializeField] private TMP_Text _cornText;
        [SerializeField] private TMP_Text _honkText;

        [SerializeField] private Button _adsButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _toHubButton;

        private GameStateController _gameStateController;

        private Wallet _wallet;
        private DistanceData _distance;


        private void Start()
        {
            _restartButton.onClick.AddListener(Restart);
            _toHubButton.onClick.AddListener(ToHub);
            _adsButton.onClick.AddListener(AdsMult);
        }

        public void Init(IServiceLocator serviceLocator)
        {
            _gameStateController = serviceLocator.Get<GameStateController>();
            _wallet = serviceLocator.Get<Wallet>();
            _distance= serviceLocator.Get<DistanceData>();
        }

        public void Panel(bool flag) 
        {
            if (flag)
            {
                _distanceText.text = _distance.Value.ToString();
                _cornText.text = _wallet.Coins.ToString();   
            }

            _panel.gameObject.SetActive(flag);
        }
        public void Restart() => _gameStateController.RestartGame();
        public void ToHub() => _gameStateController.SceneLoadToHub();
        public void AdsMult()
        {
            Debug.Log("X2");
        }

        public void Save()
        {
            _wallet.AddCoinsToData();
            _distance.UpdateCoins();
            Debug.Log("SAVE");
        }
    }
}

