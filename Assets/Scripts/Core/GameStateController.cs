using GUS.Core.GameState;
using GUS.Core.InputSys;
using GUS.Core.Locator;
using GUS.Player.State;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core
{
    public class GameStateController : MonoBehaviour, IStateChanger
    {
        [SerializeField] private SceneHandler _sceneHandler;
        [SerializeField] private Text _deltaText;
        [SerializeField] private Text _directionText;

        private GameStateMachine _gameStateMachine;
        private PlayerStateMachine _playerStateMachine;
        private RoutineExecuter _routineExecuter;

        private IState _prevPlayerState;
        private IState _prevGameState;

        public void Init(IServiceLocator serviceLocator)
        {
            _gameStateMachine = serviceLocator.Get<GameStateMachine>();
            _playerStateMachine = serviceLocator.Get<PlayerStateMachine>();
            _routineExecuter = serviceLocator.Get<RoutineExecuter>();

            if(_gameStateMachine.start!= null) // временное решение
            {
                _gameStateMachine.start.Init(this);
            }
            
            _playerStateMachine.stateChanged += CallPlayerRoutine;
            _gameStateMachine.stateChanged += CallGameStateRoutine;
        }

        private void Update()
        {
            _gameStateMachine.Update();        
        }

        private void FixedUpdate()
        {
            _gameStateMachine.FixedUpdate();
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
            _playerStateMachine.InitGameLoop(_playerStateMachine.initState);
            _gameStateMachine.start.WithStartCut(true);
            _gameStateMachine.TransitionTo(_gameStateMachine.start);
        }

        public void ClickerGame()
        {
            _gameStateMachine.TransitionTo(_gameStateMachine.clicker);
            _playerStateMachine.TransitionTo(_playerStateMachine.clicker);
        }
        
        public void StartGame()
        {
            _playerStateMachine.TransitionTo(_playerStateMachine.runState);
            _gameStateMachine.TransitionTo(_gameStateMachine.session);          
        }

        public void Resume()
        {
            _gameStateMachine.TransitionTo(_prevGameState);
            _playerStateMachine.TransitionTo(_prevPlayerState);
        }

        public void Pause()
        {
            _prevGameState = _gameStateMachine.CurrentState;
            _prevPlayerState = _playerStateMachine.CurrentState;

            _gameStateMachine.TransitionTo(_gameStateMachine.pause);
           _playerStateMachine.TransitionTo(_playerStateMachine.pauseState);
        }

        public void EndGame()
        {
            _gameStateMachine.TransitionTo(_gameStateMachine.endGame);
            _playerStateMachine.TransitionTo(_playerStateMachine.deathState);
        }

        public void RestartGame()
        {
            _routineExecuter.AllStop();
            _gameStateMachine.TransitionTo( _gameStateMachine.initState);
            _gameStateMachine.start.WithStartCut(false);
            _gameStateMachine.clicker.ResetMan();
            _gameStateMachine.TransitionTo(_gameStateMachine.start);
            
            _playerStateMachine.TransitionTo(_playerStateMachine.initState);
            _playerStateMachine.TransitionTo(_playerStateMachine.runState);
        }

        public void SceneLoadToHub()
        {
            EndGame();
            _sceneHandler.LoadOtherScene();
        }
    }
}

