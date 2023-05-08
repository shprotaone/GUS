using GUS.Core;
using GUS.Player.Movement;
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
        private float _playerVelocity;

        private float _jumpHeight;      
        private bool _inAir;

        public JumpPlayerState(LevelSettings settings, PlayerActor player, PlayerStateMachine playerState)
        {
            this._player = player;
            _playerState = playerState;
            _jumpHeight = settings.jumpHeight;
        }

        public void Enter()
        {
            _movement = (RunMovement)_player.MovementType;
            JumpMeth();
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
        //public void Update()
        //{
        //    if (!_inAir)
        //    {
        //        JumpMeth();
        //        _inAir = true;
        //    }

        //    if (_movement is RunMovement mov)
        //    {
        //        mov.ChangeVerticalVelocity(_playerVelocity);
        //    }

        //    _movement.Update();

        //    _playerState.TransitionTo(_playerState.runState);
        //}

        private void JumpMeth()
        {
            //_playerVelocity = Mathf.Sqrt(_jumpHeight * 2f);
            _playerVelocity = _jumpHeight;
            _movement.ChangeVerticalVelocity(_playerVelocity);
            _playerState.TransitionTo(_playerState.runState);

        }               
    }
}