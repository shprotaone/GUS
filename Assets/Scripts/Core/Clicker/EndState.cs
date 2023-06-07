using GUS.Core.GameState;
using GUS.Core.Locator;
using GUS.Core.UI;
using GUS.LevelBuild;
using System;
using System.Collections;
using UnityEngine;

namespace GUS.Core.Clicker
{
    public class EndState : IState
    {
        private UIController _uiController;
        private ClickerGame _game;
        private WorldController _worldController;
        private GameStateController _gameStateController;
        private ClickerStateMachine _clickerStateMachine;
        private IServiceLocator _serviceLocator;
        private CameraRunController _cameraController;
        public IStateMachine StateMachine { get; private set; }
        public EndState(ClickerStateMachine stateMachine, IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;       
            _clickerStateMachine = stateMachine;
            _worldController = serviceLocator.Get<WorldController>();
            _uiController = serviceLocator.Get<UIController>();
            _cameraController = serviceLocator.Get<ICamera>() as CameraRunController;
        }

        public void Enter()
        {
            Debug.Log("Выход в раннер");
            _gameStateController = _serviceLocator.Get<GameStateController>();
            if (_game == null) _game = _serviceLocator.Get<ClickerGame>();

            _uiController.UiInGame.Hide(false);
            _uiController.ClickerGame.SliderActivate(false);
            _uiController.ClickerGame.EndClicker();
            _clickerStateMachine.CallRoutine();
        }

        public IEnumerator Execute()
        {
            _cameraController.ClickerCamera();
            yield return new WaitForSeconds(2);
            _cameraController.RunCamera();
            _gameStateController.StartGame();
            _clickerStateMachine.StopRoutine();
            yield return null;
        }

        public void Exit()
        {

        }

        public void FixedUpdate()
        {

        }

        public void Update()
        {
            _worldController.Move();
        }
    }
}