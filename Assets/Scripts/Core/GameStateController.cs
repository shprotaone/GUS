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
        [SerializeField] private SceneHandler _sceneHandler;
        [SerializeField] private Text _deltaText;
        [SerializeField] private Text _directionText;

        private GameStateMachine _gameStateMachine;
        private PlayerStateMachine _playerStateMachine;
        private IInputType _smartInput;

        private IState _prevPlayerState;
        private IState _prevGameState;
        public SceneHandler SceneHandler => _sceneHandler;
        public GameStateMachine GameStateMachine => _gameStateMachine;

        public void Init(IServiceLocator serviceLocator)
        {
            _gameStateMachine = serviceLocator.Get<GameStateMachine>();
            _playerStateMachine = serviceLocator.Get<PlayerStateMachine>();
            _smartInput = serviceLocator.Get<IInputType>(); //для тестов

            _playerStateMachine.stateChanged += CallPlayerRoutine;
            _gameStateMachine.stateChanged += CallGameStateRoutine;
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

        private void CallPlayerRoutine()
        {
            StartCoroutine(_playerStateMachine.CurrentState.Execute());
        }

        private void CallGameStateRoutine()
        {
            StartCoroutine(_gameStateMachine.CurrentState.Execute());
        }

        public void InitGame()
        {
            _gameStateMachine.InitGameLoop(_gameStateMachine.initState);
            _playerStateMachine.Initialize(_playerStateMachine.initState);
            _gameStateMachine.TransitionTo(_gameStateMachine.start);

            StartGame();
        }

        public void ClickerGame()
        {
            _gameStateMachine.TransitionTo(_gameStateMachine.clicker);
            _playerStateMachine.TransitionTo(_playerStateMachine.clicker);
        }
        
        public void StartGame()
        {         
            _gameStateMachine.TransitionTo(_gameStateMachine.session);
            _playerStateMachine.TransitionTo(_playerStateMachine.runState);
           // StartCoroutine(_gameStateMachine.CurrentState.Execute());
        }

        public void Resume()
        {
            _gameStateMachine.TransitionTo(_prevGameState);
            _playerStateMachine.TransitionTo(_prevPlayerState);
            //StartCoroutine(_gameStateMachine.CurrentState.Execute());
        }

        public void Pause()
        {
            _prevGameState = _gameStateMachine.CurrentState;
            _prevPlayerState = _playerStateMachine.CurrentState;

            _gameStateMachine.TransitionTo(_gameStateMachine.pause);
            _playerStateMachine.TransitionTo(_playerStateMachine.initState);
        }

        public void EndGame()
        {
            _gameStateMachine.TransitionTo(_gameStateMachine.endGame);
            _playerStateMachine.TransitionTo(_playerStateMachine.deathState);

            //StartCoroutine(_gameStateMachine.CurrentState.Execute());
        }

        public void RestartGame()
        {           
            _gameStateMachine.TransitionTo( _gameStateMachine.initState);
            _gameStateMachine.TransitionTo(_gameStateMachine.start);            
            _gameStateMachine.TransitionTo(_gameStateMachine.session);
            _playerStateMachine.Initialize(_playerStateMachine.initState);
            _playerStateMachine.TransitionTo(_playerStateMachine.runState);
        }

        public void SceneLoadHandler()
        {
            _sceneHandler.LoadRunScene();
        }

        public void InitHub()
        {
            _gameStateMachine.InitGameLoop(_gameStateMachine.initMapState);
        }

        public void Explore()
        {
            _playerStateMachine.Initialize(_playerStateMachine.initState);
            _gameStateMachine.TransitionTo(_gameStateMachine.explore);
            _playerStateMachine.TransitionTo(_playerStateMachine.exploreState);
        }
    }
}

