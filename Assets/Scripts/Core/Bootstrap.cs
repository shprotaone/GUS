using GUS.Core.GameState;
using GUS.Core.InputSys;
using GUS.Core.InputSys.Joiystick;
using GUS.Core.Locator;
using GUS.Core.Pool;
using GUS.LevelBuild;
using GUS.Player;
using GUS.Player.State;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using SystemInfo = UnityEngine.Device.SystemInfo;

namespace GUS.Core
{
    public class Bootstrap : MonoBehaviour
    {      
        [SerializeField] private TMP_Text _stateText;
        [SerializeField] private GameStateController _stateController;
        [SerializeField] private UIController _uiController;
        [SerializeField] private PlayerActor _player;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private PoolObjectStorage _platformStorage;
        [SerializeField] private PoolObjectStorage _collectablesStorage;
        [SerializeField] private LevelSettings _levelSettings;
        [SerializeField] private ObjectPool _platformPool;
        [SerializeField] private ObjectPool _collectablesPool;
        [SerializeField] private FloatingJoystick _joystick;    //кандидат на отделение
        [SerializeField] private bool _joystickMove;

        private IServiceLocator _serviceLocator;
        private IInputType _inputType;

        private void Awake()
        {
           if (!_joystickMove) RunInit();
           else HubInit();          
        }

        private void Start()
        {           
            _stateController.Init(_serviceLocator);
            _player.Init(_inputType, _serviceLocator);
        }


        private void RunInit()
        {
            _serviceLocator = new ServiceLocator();
            _serviceLocator.Register<UIController>(_uiController);
            PoolInitialization();
            _serviceLocator.Register<LevelSettings>(_levelSettings);
            _serviceLocator.Register<WorldController>(new WorldController(_startPoint, _serviceLocator));
            _serviceLocator.Register<GameStateMachine>(new GameStateMachine(_stateText, _serviceLocator));
            _serviceLocator.Register<GameStateController>(_stateController);
            _serviceLocator.Register<TMP_Text>(_stateText);           

            SetInput();
            PlayerInit();
        }
          
        private void HubInit()
        {
            _serviceLocator= new ServiceLocator();
            _serviceLocator.Register<UIController>(_uiController);
            _serviceLocator.Register<LevelSettings>(_levelSettings);
            //_serviceLocator.Register<TMP_Text>(_stateText);
            _serviceLocator.Register<GameStateController>(_stateController);
            _serviceLocator.Register<GameStateMachine>(new GameStateMachine(_serviceLocator,true));
            SetInput();
            PlayerInit();
        }

        private void FlyInit()
        {

        }
        private void PoolInitialization()
        {
            _serviceLocator.Register<PoolObjectStorage>(_platformStorage);
            _serviceLocator.Register<PoolObjectStorage>(_collectablesStorage);
            _serviceLocator.Register<ObjectPool>(_platformPool);
            _serviceLocator.Register<ObjectPool>(_collectablesPool);

            _platformPool.InitPool(_platformStorage);
            _collectablesPool.InitPool(_collectablesStorage);
        }

        private void SetInput()
        {
            if (_joystickMove)
            {
                _inputType = _joystick;
            }
            else if (SystemInfo.deviceType == DeviceType.Handheld)
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
          
            _serviceLocator.Register<IInputType>(_inputType);
        }

        private void PlayerInit()
        {
            _serviceLocator.Register<Wallet>(new Wallet(_serviceLocator));
            _player.Init(_inputType, _serviceLocator);
            _serviceLocator.Register<PlayerActor>(_player);                
            _serviceLocator.Register<PlayerStateMachine>(new PlayerStateMachine(_serviceLocator));        
        }
    }
}

