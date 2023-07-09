using GUS.Core.Data;
using GUS.Core.GameState;
using GUS.Core.Locator;
using GUS.Core.UI;
using GUS.LevelBuild;
using System;
using System.Collections;
using System.Threading;
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
        private CameraRunController _cameraController;
        private Wallet _wallet;
        private BossSettings _bossSettings;
        private AudioService _audioService;

        private IServiceLocator _serviceLocator;
        public IStateMachine StateMachine { get; private set; }
        public EndState(ClickerStateMachine stateMachine, IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;       
            _clickerStateMachine = stateMachine;
            _worldController = serviceLocator.Get<WorldController>();
            _wallet= serviceLocator.Get<Wallet>();           
            _uiController = serviceLocator.Get<UIController>();
            _audioService= serviceLocator.Get<AudioService>();
            _cameraController = serviceLocator.Get<ICamera>() as CameraRunController;
        }

        public void Enter()
        {
            Debug.Log("Выход в раннер");
            _gameStateController = _serviceLocator.Get<GameStateController>();
            if (_game == null) _game = _serviceLocator.Get<ClickerGame>();

            _uiController.UiInGame.Hide(false);
            _uiController.ClickerGame.SliderActivate(false);
            
            _clickerStateMachine.CallRoutine();
            _bossSettings = _game.Settings;
        }

        public IEnumerator Execute()
        {
            _cameraController.RunCamera();
            _game.Complete();
            _audioService.PlaySFX(_audioService.Data.afterRunner);
            yield return _uiController.ClickerGame.EndClicker();

            yield return new WaitForSeconds(3);            
            _wallet.AddCoins(_bossSettings.reward);

            _gameStateController.StartGame();
            _clickerStateMachine.StopRoutine();
            yield return null;
        }

        public void Exit()
        {
            //сюда не заходит в end? 
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