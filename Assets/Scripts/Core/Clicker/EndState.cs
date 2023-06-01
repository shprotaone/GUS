﻿using GUS.Core.GameState;
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
        private UIClickerGame _ui;
        private ClickerGame _game;
        private WorldController _worldController;
        private GameStateController _gameStateController;

        private IServiceLocator _serviceLocator;
        public IStateMachine StateMachine { get; private set; }
        public EndState(ClickerStateMachine stateMachine, IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;       
            _worldController = serviceLocator.Get<WorldController>();
            _ui = serviceLocator.Get<UIController>().ClickerGame;
        }

        public void Enter()
        {
            Debug.Log("Выход в раннер");
            _gameStateController = _serviceLocator.Get<GameStateController>();
            if (_game == null) _game = _serviceLocator.Get<ClickerGame>();
            _ui.DisableSlider();
        }

        public IEnumerator Execute()
        {
            yield return null;
        }

        public void Exit()
        {
            Debug.Log("Выход из выхода в раннер");
            _gameStateController.StartGame();
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