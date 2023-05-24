using GUS.Core;
using GUS.Core.GameState;
using GUS.Player.Movement;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace GUS.Player.State
{
    public class JumpPlayerState : IState
    {       
        private PlayerStateMachine _playerState;
        private RunMovement _movement;
        private PlayerActor _player;
        private AnimatorController _animatorController;
        private float _playerVelocity;

        private float _jumpHeight;      
        private bool _inAir;

        public IStateMachine StateMachine => _playerState;

        public JumpPlayerState(PlayerStateMachine playerState, LevelSettings settings, PlayerActor player)
        {
            this._player = player;
            _playerState = playerState;
            _jumpHeight = settings.jumpHeight;
            _animatorController = player.AnimatorController;
        }

        public void Enter()
        {
            _movement = (RunMovement)_player.MovementType;
            JumpMeth();
            _animatorController.JumpActivate();
        }

        public IEnumerator Execute()
        {
            yield return null;
        }

        public void Exit()
        {
            _inAir = false;
        }

        public void FixedUpdate()
        {
            
        }

        public void Update()
        {
            
        }

        public void SetJump(float jumpHeight)
        {
            _jumpHeight = jumpHeight;
        }

        private void JumpMeth()
        {
            _playerVelocity = _jumpHeight;
            _movement.ChangeVerticalVelocity(_playerVelocity);
            _playerState.TransitionTo(_playerState.runState);
        }               
    }
}