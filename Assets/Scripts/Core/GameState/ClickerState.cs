﻿using GUS.Core.Clicker;
using GUS.Core.Locator;
using GUS.LevelBuild;
using GUS.Player;
using System.Collections;

namespace GUS.Core.GameState
{
    public class ClickerState : IState
    {
        private WorldController _worldController;
        private ClickerGame _clicker;
        private ClickerStateMachine _clickerStateMachine;
        private PlayerActor _playerActor;
        private IServiceLocator _serviceLocator;
        public IStateMachine StateMachine { get; private set; }

        public ClickerState(IStateMachine stateMachine, IServiceLocator serviceLocator) 
        {                       
            _serviceLocator = serviceLocator;
            _worldController = serviceLocator.Get<WorldController>();
            _playerActor= _serviceLocator.Get<PlayerActor>();
            StateMachine = stateMachine;
        }        

        public void Enter()
        {
            _clicker = _serviceLocator.Get<ClickerGame>();
            _clickerStateMachine = _clicker.StateMachine;
            _playerActor.PowerUpHandler.ResetPoweraUps();
            _clicker.Paused(false);
            _worldController.CreateOnlyFreePlatforms(true);            
        }

        public IEnumerator Execute()
        {
            yield return null;
        }

        public void Exit()
        {
            //_clicker.Paused(true);
            _worldController.CreateOnlyFreePlatforms(false);
        }

        public void FixedUpdate()
        {
           _clickerStateMachine.FixedUpdate();
        }

        public void Update()
        {         
            _clickerStateMachine.Update();
        }

        public void ResetMan()
        {
            _clicker?.Restart();
        }
    }
}