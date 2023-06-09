﻿using GUS.Core.GameState;
using GUS.Core.Locator;
using GUS.LevelBuild;
using GUS.Player.Movement;
using GUS.Player;
using System.Collections;
using UnityEngine;
using GUS.Core.UI;

namespace GUS.Core.Clicker
{
    public class BiteState : IState
    {
        private PlayerActor _actor;
        private CameraRunController _cameraController;
        private WorldController _worldController;
        private AudioService _audioService;
               
        private ClickerGame _game;
        private ClickerMovement _movement;
        private ClickerStateMachine _clickerStateMachine;
        private UIClickerGame _uiClicker;
        private IServiceLocator _serviceLocator;
        public IStateMachine StateMachine { get; private set; }
        public BiteState(ClickerStateMachine stateMachine, IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
            _clickerStateMachine = stateMachine;
            _worldController = serviceLocator.Get<WorldController>();
            _cameraController = serviceLocator.Get<ICamera>() as CameraRunController;
            _audioService = serviceLocator.Get<AudioService>();
            _uiClicker = serviceLocator.Get<UIController>().ClickerGame;
            _actor= serviceLocator.Get<PlayerActor>();
            
        }

        public void Enter()
        {
            Debug.Log("Кусательный");           
            _movement = _serviceLocator.Get<ClickerMovement>();
            _movement.Init(_actor, null, 0);
            _actor.SetMovementType(_movement);

            if (_game == null) _game = _serviceLocator.Get<ClickerGame>();
            _clickerStateMachine.CallRoutine();

            _game.Enemy.MoveToDamage(false,_game.Settings.prepareTime);
        }

        public IEnumerator Execute()
        {
            if (!_game.IsActive) yield break;

            _cameraController.BiteCamera();
            yield return new WaitForSeconds(_game.Settings.prepareTime);

            _audioService.PlaySFX(_audioService.Data.slowMo);
            _uiClicker.TutorialPanel(true);
            _movement.CanAttack(true);
            _movement.OnClick += BiteSound;
            _movement.OnClick += _game.GetDamage;
            _movement.OnClick += () => _cameraController.ShackeCameraHandle(5, 0.1f);
            _uiClicker.FocusActivate();
             _actor.ChangeModelPos(0.6f, 0.2f);
            _game.Enemy.SlowMo(true);

            //_actor.AnimatorController.Pause(true);
            yield return null;
        }

        public void Exit()
        {
            Debug.Log("Выход из Кусательного");
            //_actor.AnimatorController.Pause(false);
            _movement.OnClick -= BiteSound;
            _movement.OnClick -= _game.GetDamage;
            _movement.OnClick -= () => _cameraController.ShackeCameraHandle(5, 0.1f);
            _movement.CanAttack(false);
            _actor.ChangeModelPos(-0.3f, 0.2f);
            _game.Enemy.SlowMo(false);
            _uiClicker.FocusDeactivate();
            _clickerStateMachine.StopRoutine();
        }

        public void FixedUpdate()
        {

        }

        public void Update()
        {
            _worldController.Move();
            _movement.Update();
        }

        private void BiteSound()
        {
            _audioService.PlaySFX(_audioService.Data.bite);
        }
    }
}