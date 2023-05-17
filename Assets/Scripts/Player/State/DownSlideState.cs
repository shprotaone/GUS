using DG.Tweening;
using GUS.Core;
using System;
using System.Collections;
using UnityEngine;

namespace GUS.Player.State
{
    public class DownSlideState :IState
    {
        private const float downHeight = 0.5f;
        private PlayerStateMachine _playerStateMachine;
        private IMovement _movement;
        private PlayerActor _player;
        private AnimatorController _animatorController;
            
        private float _downSlideTime;
        private float _standartHeight;

        public DownSlideState(float downSlideTime,  PlayerActor player, PlayerStateMachine playerStateMachine)
        {
            this._downSlideTime = downSlideTime;
            this._player = player;
            this._playerStateMachine = playerStateMachine;
            _standartHeight = _player.Collider.height;
            _animatorController = player.AnimatorController;
        }

        public void Enter()
        {
            _movement = _player.MovementType;
            _animatorController.CrouchActivate();
        }

        public IEnumerator Execute()
        {
            _player.Collider.height = downHeight;
            yield return new WaitForSeconds(_downSlideTime);
            _player.Collider.height = _standartHeight;
            _playerStateMachine.TransitionTo(_playerStateMachine.runState);
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
            _movement.Update();
        }

        public void SetCrouch(float downSlideTime)
        {
            _downSlideTime = downSlideTime;
        }
    }
}