using GUS.Core;
using GUS.Core.GameState;
using GUS.Player.Movement;
using System;
using System.Collections;

namespace GUS.Player.State
{
    public class RunPlayerState : IState
    {
        private PlayerStateMachine _playerState;
        private PlayerActor _player;
        private AnimatorController _animatorController;
        private RunMovement _movement;
        private float _steerSpeed;

        public RunPlayerState(LevelSettings settings, PlayerActor player, PlayerStateMachine state)
        {
            _movement = new RunMovement();
            
            _player = player;
            _steerSpeed = settings.steerSpeed;
            _playerState = state;
            _animatorController = player.AnimatorController;
            _movement.Init(_player, _playerState, _steerSpeed);
            SetMoveSettings(settings);
        }

        public void SetMoveSettings(LevelSettings settings)
        {
            _movement.SetDistance(settings.distanceToMovement);          
            _movement.SetGravity(settings.gravity, settings.gravityScale);
            _animatorController.ChangeAnimationSpeed(settings.maxWorldSpeed);
        }

        public void Enter()
        {
            _player.SetMovementType(_movement);           
            _animatorController.RunActivate(true);
        }

        public IEnumerator Execute()
        {
            yield return null;
        }

        public void Exit()
        {
            _animatorController.RunActivate(false);
        }

        public void Update()
        {
            _movement.Update();
        }

        public void FixedUpdate()
        {
            _movement.FixedUpdate();
        }
    }
}