using GUS.Core;
using GUS.Player.Movement;
using System;
using System.Collections;

namespace GUS.Player.State
{
    public class RunPlayerState : IState
    {
        private PlayerStateMachine _playerState;
        private PlayerActor _player;
        private RunMovement _movement;
        private float _steerSpeed;

        public RunPlayerState(LevelSettings settings, PlayerActor player,PlayerStateMachine state)
        {
            _movement = new RunMovement();
            SetMoveSettings(settings.distanceToMovement,settings.gravity,settings.gravityScale);
            _player = player;
            _steerSpeed = settings.steerSpeed;
            _playerState = state;

            _movement.Init(_player, _playerState, _steerSpeed);
        }

        public void SetMoveSettings(float distance,float gravity, float gravityScale)
        {
            _movement.SetDistance(distance);
            _movement.SetGravity(gravity, gravityScale);
        }

        public void Enter()
        {
            _player.SetMovementType(_movement);       
        }

        public IEnumerator Execute()
        {
            yield return null;
        }

        public void Exit()
        {
            
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