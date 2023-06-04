using GUS.Core;
using GUS.Core.GameState;
using GUS.Player.Movement;
using System.Collections;
using UnityEngine;

namespace GUS.Player.State
{
    public class DownSlideState : IState
    {
        private LevelSettings _levelSettings;
        private PlayerStateMachine _playerStateMachine;
        private RunMovement _movement;
        private PlayerActor _player;
        private AnimatorController _animatorController;
            
        private float _downSlideTime;
        private float _standartHeight;

        public IStateMachine StateMachine => _playerStateMachine;

        public DownSlideState(float downSlideTime,  PlayerActor player, PlayerStateMachine playerStateMachine)
        {
            _downSlideTime = downSlideTime;
            _player = player;
            _playerStateMachine = playerStateMachine;
            _standartHeight = _player.Collider.height;
            _levelSettings = playerStateMachine.LevelSettings;
            _animatorController = player.AnimatorController;
        }

        public void Enter()
        {           
            _movement = _player.MovementType as RunMovement;
            _animatorController.RunActivate(true);
            if (_movement.IsGrounded) _animatorController.CrouchActivate();
        }

        public IEnumerator Execute()
        {
            if(!_movement.IsGrounded) _movement.SetGravityScale(_levelSettings.forceLandingPower);
            else
            {
                _player.Collider.height = _levelSettings.downSlideHeight;
                _player.Collider.center -= new Vector3(0,0.5f,0);
                yield return new WaitForSeconds(_downSlideTime);
                _player.Collider.height = _standartHeight;
                _player.Collider.center += new Vector3(0, 0.5f, 0);
            }
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