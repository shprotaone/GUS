using GUS.Core.Clicker;
using GUS.Core.Data;
using GUS.Core.GameState;
using GUS.Core.InputSys;
using GUS.Core.Locator;
using GUS.Core.Pool;
using GUS.Core.SaveSystem;
using GUS.Core.Tutorial;
using GUS.Core.UI;
using GUS.LevelBuild;
using GUS.Player;
using GUS.Player.Movement;
using GUS.Player.State;
using System.Linq;
using UnityEngine;
using SystemInfo = UnityEngine.Device.SystemInfo;

namespace GUS.Core
{
    public class RunLocator : MonoBehaviour
    {
        [Header("Игрок")]
        [SerializeField] private PlayerActor _player;

        [Header("Пулы")]    
        [SerializeField] private ObjectPool _platformPool;
        [SerializeField] private ObjectPool _collectablesPool;

        [Header("Настройки уровня")]
        [SerializeField] private PoolObjectStorage _platformStorage;
        [SerializeField] private PoolObjectStorage _collectablesStorage;
        [SerializeField] private LevelSettings _levelSettings;
        [SerializeField] private SpecialPlatformBuilder _specialPlatformBuilder; 
        [SerializeField] private Progressive _progressiveSystem;

        [Header("Вспомогательные")]
        [SerializeField] private UIController _uiController;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private GameStateController _stateController;        
        [SerializeField] private BossPositions _bossPositions;
        
        [Header("Сервисы")]
        [SerializeField] private AudioService _audioService;
        [SerializeField] private JsonToFirebase _jsonToFirebase;
        [SerializeField] private RoutineExecuter _routineExecuter;
        [SerializeField] private CollectableFactory _collectableFactory;

        private PlayerStateMachine _playerState;
        private GameStateMachine _gameStateMachine;

        private DeleteService _deleteService;
        private Wallet _wallet;
        private HonkCoinWallet _honkWallet;
        private DistanceData _distance;
        private StorageService _storageService;
        private WorldController _worldController;
        private ClickerGame _clicker;
        private ClickerMovement _clickerMovement;
        private RunMovement _runMovement;
        private DistanceMutiplier _distanceMutiplier;
        private PauseHandle _pauseHandle;
        private TutorialSystemRun _tutorial;
        private BonusSpawnCatcher _bonusSpawnCatcher;

        private IStateChanger _stateChanger;
        private ICoinView _coinView;
        private IHonkCoinView _honkCoinView;
        private IDistanceView _distanceView;
        private ICamera _cameraController;
        private IServiceLocator _serviceLocator;
        private IInputType _inputType;        

        private void Awake()
        {
            Application.targetFrameRate = 75;

            var cam = FindObjectsOfType<MonoBehaviour>().OfType<ICamera>();
            _cameraController = cam.First();

            _serviceLocator = new ServiceLocator();
        }

        private void Start()
        {
            Create();
            Registration();
            Initialization();

            _stateController.Init(_serviceLocator);
            _stateController.InitGame();           
        }

        private void Create()
        {
            _wallet = new Wallet();
            _distance = new DistanceData();
            _storageService = new StorageService();
            _deleteService = new DeleteService();
            _worldController = new WorldController();
            _clicker = new ClickerGame();
            _playerState = new PlayerStateMachine();
            _gameStateMachine = new GameStateMachine();
            _clickerMovement = new ClickerMovement();
            _runMovement= new RunMovement();
            _distanceMutiplier = new DistanceMutiplier();
            _honkWallet = new HonkCoinWallet();
            _pauseHandle = new PauseHandle();
            _bonusSpawnCatcher = new BonusSpawnCatcher();

            _coinView = _uiController.UiInGame;
            _distanceView = _uiController.UiInGame;
            _stateChanger = _stateController;
        }

        private void Registration()
        {              
            _serviceLocator.Register(this);
            _serviceLocator.Register(_storageService);
            _serviceLocator.Register(_wallet);
            _serviceLocator.Register(_honkWallet);
            _serviceLocator.Register(_distance);
            _serviceLocator.Register(_deleteService);
            _serviceLocator.Register(_jsonToFirebase);
            _serviceLocator.Register(_routineExecuter);
            _serviceLocator.Register(_worldController);
            _serviceLocator.Register(_cameraController);
            _serviceLocator.Register(_audioService);
            _serviceLocator.Register(_specialPlatformBuilder);
            _serviceLocator.Register(_uiController);
            _serviceLocator.Register(_levelSettings);
            _serviceLocator.Register(_stateController);
            _serviceLocator.Register(_progressiveSystem);
            _serviceLocator.Register(_player);
            _serviceLocator.Register(_clicker);
            _serviceLocator.Register(_runMovement);
            _serviceLocator.Register(_coinView);
            _serviceLocator.Register(_honkCoinView);
            _serviceLocator.Register(_distanceView);
            _serviceLocator.Register(_stateChanger);
            _serviceLocator.Register(_bossPositions);
            _serviceLocator.Register(_playerState);
            _serviceLocator.Register(_gameStateMachine);
            _serviceLocator.Register(_distanceMutiplier);
            _serviceLocator.Register(_pauseHandle);
            _serviceLocator.Register(_bonusSpawnCatcher);
            
           
            RegisterPools();
            _serviceLocator.Register(_collectableFactory);
            RegisterInput();
        }

        private void Initialization()
        {
            _storageService.Init(_serviceLocator);
            _collectableFactory.Init(_collectablesStorage, _serviceLocator);
            
            _gameStateMachine.Init(_serviceLocator);       
            _wallet.Init(_serviceLocator);
            _honkWallet.Init(_serviceLocator);
            _distance.Init(_serviceLocator);       
            _deleteService.Init(_serviceLocator);

            _collectablesPool.InitPool(_collectablesStorage);           
            _platformPool.InitPool(_platformStorage);           
          
            _worldController.Init(_startPoint, _serviceLocator);
            _player.Init(_serviceLocator,false);
            _progressiveSystem.Init(_serviceLocator);
            _uiController.Init(_serviceLocator);
            _clicker.Init(_serviceLocator);
            _playerState.Init(_serviceLocator);
            _distanceMutiplier.Init(_serviceLocator);

            CheckTutorial();
        }

        private void RegisterPools()
        {
            _serviceLocator.Register(_platformStorage);
            _serviceLocator.Register(_collectablesStorage);
            _serviceLocator.Register(_platformPool);
            _serviceLocator.Register(_collectablesPool);
        }

        private void RegisterInput()
        {
            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                _inputType = new SmartphoneInput();
            }
            else if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                _inputType = new Keyboard();
            }
            else
            {
                Debug.LogWarning("Устройство не опознано" + SystemInfo.deviceType);
            }

            _serviceLocator.Register(_clickerMovement);
            _serviceLocator.Register(_inputType);
        }

        public ObjectPool GetPool(PoolTypeEnum type)
        {
            if(type == PoolTypeEnum.Platform)
            {
                return _platformPool;
            }
            else
            {
                return _collectablesPool;
            }
        }

        private void CheckTutorial()
        {
            if (!_storageService.Data._tutorialSteps[1])
            {
                _tutorial = new TutorialSystemRun();
                _uiController.TuttorialCanvas(true);
                _serviceLocator.Register(_tutorial);
                _tutorial.Init(_serviceLocator,_startPoint);
            }
                                              
        }
    }
}

