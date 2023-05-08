using GUS.Core.GameState;
using GUS.Core.InputSys;
using GUS.Core.Locator;
using GUS.Player.State;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core
{
    public class GameStateController : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Text _deltaText;
        [SerializeField] private Text _directionText;

        [SerializeField] private bool _test;

        private GameStateMachine _gameStateMachine;
        private PlayerStateMachine _playerStateMachine;
        private IInputType _smartInput;

        public void Init(IServiceLocator serviceLocator)
        {
            _gameStateMachine = serviceLocator.Get<GameStateMachine>();
            _playerStateMachine = serviceLocator.Get<PlayerStateMachine>();

            if (_test)
            {
                _restartButton?.onClick.AddListener(RestartGame);
                _startButton.onClick.AddListener(StartGame);
                _pauseButton.onClick.AddListener(Pause);
                InitGame(); //это убрать из хаба
            }
            
            _smartInput = serviceLocator.Get<IInputType>(); //для тестов

            _playerStateMachine.stateChanged += CallCoroutine;
        }

        private void Update()
        {
            _gameStateMachine.Update();
            _playerStateMachine.Update();

            if (_smartInput is SmartphoneInput input ) 
            {
                _deltaText.text = input.Delta.ToString();
                _directionText.text = input.Direction.ToString();
            }          
        }

        private void FixedUpdate()
        {
            _gameStateMachine.FixedUpdate();
            _playerStateMachine.FixedUpdate();
        }

        private void CallCoroutine()
        {
            StartCoroutine(_playerStateMachine.CurrentState.Execute());
        }
        public void InitGame()
        {
            _gameStateMachine.InitGameLoop(_gameStateMachine.initState);
            _playerStateMachine.Initialize(_playerStateMachine.initState);
            _gameStateMachine.TransitionTo(_gameStateMachine._start);
        }
        public void Explore()
        {
            _playerStateMachine.Initialize(_playerStateMachine.initState);
            _gameStateMachine.InitGameLoop(_gameStateMachine._pitStop);
            _playerStateMachine.TransitionTo(_playerStateMachine.exploreState);
        }
        public void StartGame()
        {
            
            _gameStateMachine.TransitionTo(_gameStateMachine._session);
            _playerStateMachine.TransitionTo(_playerStateMachine.runState);
        }

        public void Pause()
        {
            _gameStateMachine.TransitionTo(_gameStateMachine._pause);
            _playerStateMachine.TransitionTo(_playerStateMachine.initState);
        }

        public void EndGame()
        {
            _gameStateMachine.TransitionTo(_gameStateMachine._endGame);
            _playerStateMachine.TransitionTo(_playerStateMachine.deathState);

            StartCoroutine(_gameStateMachine.CurrentState.Execute());
        }

        public void RestartGame()
        {           
            _gameStateMachine.TransitionTo( _gameStateMachine.initState);
            _gameStateMachine.TransitionTo(_gameStateMachine._start);
            _playerStateMachine.Initialize(_playerStateMachine.initState);          
        }
    }
}

