using GUS.Core.GameState;
using GUS.Core.InputSys.Joiystick;
using GUS.Core.Locator;
using GUS.Core.InputSys;
using GUS.Player.State;
using GUS.Player;
using GUS.Core.Data;
using GUS.Core.UI;
using GUS.Core.SaveSystem;
using GUS.Core.Hub.BonusShop;
using GUS.Core.Hub.BuildShop;
using System.Linq;
using TMPro;
using UnityEngine;
using Sirenix.OdinInspector;
using GUS.Player.Movement;
using GUS.Core.Tutorial;

namespace GUS.Core.Hub
{
    public class HubLocator : MonoBehaviour
    {
        [Title("Стартовые настройки игрока")]
        [SerializeField] private Transform _startPos;
        [SerializeField] private PlayerActor _player;
        [SerializeField] private LevelSettings _levelSettings;

        [Title("Сервисы")]
        [SerializeField] private AudioService _audioService;
        [SerializeField] private SceneHandler _sceneHandler;
        [SerializeField] private FloatingJoystick _joystick;
        [SerializeField] private UiHubController _uiHubController;       
        [SerializeField] private JsonToFirebase _jsonToFirebase;
        [SerializeField] private TutorialSystemHUB _tutorialSystem;

        [Title("Магазины")]
        [SerializeField] private BuildsSystem _buildSystem;
        [SerializeField] private ShopSystem _shopSystem;

        private Wallet _wallet;
        private HonkCoinWallet _honkWallet;
        private HubStateController _hubController;
        private StorageService _storageService;
        private DeleteService _deleteService;
        private PlayerStateMachine _playerState;
        private GameStateMachine _gameStateMachine;
        private RunMovement _runMovement;

        private IStateChanger _stateChanger;
        private ICamera _cameraController;
        private IServiceLocator _serviceLocator;
        private IInputType _inputType;
        private ICoinView _coinView;
        private IHonkCoinView _honkCoinView;

        private TMP_Text _testText;

        public IServiceLocator ServiceLocator => _serviceLocator;
        private void Awake()
        {
            var cam = FindObjectsOfType<MonoBehaviour>().OfType<ICamera>();
            _cameraController = cam.First();
            _serviceLocator = new ServiceLocator();
        }

        private void Start()
        {
            Create();
            Registartion();
            Initialization();

            _audioService.PlayMusic(_audioService.Data.mainMenu);
        }

        private void Create()
        {
            _wallet = new Wallet();
            _storageService = new StorageService();
            _hubController = new HubStateController();
            _honkWallet = new HonkCoinWallet();
            _runMovement= new RunMovement();
            _stateChanger = _hubController;
            _inputType = _joystick;
            _coinView = _uiHubController.CoinView;
            _honkCoinView= _uiHubController.HonkCoinView;
            _deleteService = new DeleteService();
            _playerState = new PlayerStateMachine();
            _gameStateMachine = new GameStateMachine();
            
        }

        private void Registartion()
        {
            _serviceLocator.Register(_tutorialSystem);
            _serviceLocator.Register(_cameraController);
            _serviceLocator.Register(_audioService);
            _serviceLocator.Register(_sceneHandler);
            _serviceLocator.Register(_joystick);
            _serviceLocator.Register(_inputType);
            _serviceLocator.Register(_uiHubController);
            _serviceLocator.Register(_wallet);
            _serviceLocator.Register(_honkWallet);
            _serviceLocator.Register(_levelSettings);
            _serviceLocator.Register(_hubController);
            _serviceLocator.Register(_player);
            _serviceLocator.Register(_runMovement);
            _serviceLocator.Register(_coinView);
            _serviceLocator.Register(_honkCoinView);
            _serviceLocator.Register(_storageService);
            _serviceLocator.Register(_jsonToFirebase);
            _serviceLocator.Register(_deleteService);
            _serviceLocator.Register(_stateChanger);
            _serviceLocator.Register(_playerState);
            _serviceLocator.Register(_gameStateMachine);
            _serviceLocator.Register(_shopSystem);
            _serviceLocator.Register(_buildSystem);
            
        }

        private void Initialization()
        {           
            _gameStateMachine.InitHub(ServiceLocator);
            _storageService.Init(ServiceLocator);                      
            _uiHubController.Init(ServiceLocator);            
            _hubController.SetStartPosition(_startPos.position);
            _hubController.Init(ServiceLocator);
            _tutorialSystem.Init(ServiceLocator);

            _wallet.Init(ServiceLocator);
            _honkWallet.Init(ServiceLocator);
            _player.Init(_serviceLocator,true);
            _deleteService.Init(ServiceLocator);
            _playerState.Init(ServiceLocator);
            _hubController.Idle();
            _buildSystem.Init(ServiceLocator);
            _shopSystem.Init(ServiceLocator);
        }
    }
}
