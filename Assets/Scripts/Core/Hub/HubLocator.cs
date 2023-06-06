using GUS.Core.GameState;
using GUS.Core.InputSys.Joiystick;
using GUS.Core.Locator;
using System.Linq;
using TMPro;
using UnityEngine;
using GUS.Core.InputSys;
using GUS.Player.State;
using GUS.Player;
using GUS.Core.Data;
using GUS.Core.UI;
using GUS.Core.SaveSystem;

namespace GUS.Core.Hub
{
    public class HubLocator : MonoBehaviour
    {
        [SerializeField] private PlayerActor _player;
        [SerializeField] private AudioService _audioService;
        [SerializeField] private SceneHandler _sceneHandler;
        [SerializeField] private FloatingJoystick _joystick;
        [SerializeField] private UiHubController _uiHubController;
        [SerializeField] private LevelSettings _levelSettings;
        [SerializeField] private JsonToFirebase _jsonToFirebase;

        private Wallet _wallet;
        private HubStateController _hubController;
        private StorageService _storageService;
        private DeleteService _deleteService;
        private PlayerStateMachine _playerState;
        private GameStateMachine _gameStateMachine;


        private IStateChanger _stateChanger;
        private ICamera _cameraController;
        private IServiceLocator _serviceLocator;
        private IInputType _inputType;
        private ICoinView _coinView;

        private TMP_Text _testText;

        public IServiceLocator ServiceLocator => _serviceLocator;
        private void Awake()
        {
            var cam = FindObjectsOfType<MonoBehaviour>().OfType<ICamera>();
            _cameraController = cam.First();
            _coinView = _uiHubController;
            _serviceLocator = new ServiceLocator();
        }

        private void Start()
        {
            Create();
            Registartion();
            Initialization();
        }

        private void Create()
        {
            _wallet = new Wallet();
            _storageService = new StorageService();
            _hubController = new HubStateController();
            _stateChanger = _hubController;
            _inputType = _joystick;
            _deleteService = new DeleteService();
            _playerState = new PlayerStateMachine();
            _gameStateMachine = new GameStateMachine();
            
        }

        private void Registartion()
        {
            _serviceLocator.Register(_cameraController);
            _serviceLocator.Register(_audioService);
            _serviceLocator.Register(_sceneHandler);
            _serviceLocator.Register(_joystick);
            _serviceLocator.Register(_inputType);
            _serviceLocator.Register(_uiHubController);
            _serviceLocator.Register(_wallet);
            _serviceLocator.Register(_levelSettings);
            _serviceLocator.Register(_hubController);
            _serviceLocator.Register(_player);
            _serviceLocator.Register(_coinView);
            _serviceLocator.Register(_storageService);
            _serviceLocator.Register(_jsonToFirebase);
            _serviceLocator.Register(_deleteService);
            _serviceLocator.Register(_stateChanger);
            _serviceLocator.Register(_playerState);
            _serviceLocator.Register(_gameStateMachine);
        }

        private void Initialization()
        {
            _gameStateMachine.InitHub(ServiceLocator);
            _storageService.Init(ServiceLocator);
            _wallet.Init(ServiceLocator);
            _uiHubController.Init(ServiceLocator);          
            _hubController.Init(ServiceLocator);
            _player.Init(_serviceLocator,true);
            _deleteService.Init(ServiceLocator);
            _playerState.Init(ServiceLocator);
            _hubController.Idle();
        }
    }
}
