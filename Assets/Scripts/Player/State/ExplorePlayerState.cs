using GUS.Core;
using GUS.Core.GameState;
using GUS.Player.Movement;
using System.Collections;

namespace GUS.Player.State
{
    public class ExplorePlayerState : IState
    {
        private PlayerActor _playerActor;
        private PlayerStateMachine _playerState;
        private float _steerSpeed;
        private ExploreMovement _movement;
        public IStateMachine StateMachine => _playerState;
        public ExplorePlayerState(float steerSpeed, PlayerActor playerActor,PlayerStateMachine playerStateMachine)
        {          
            _playerActor = playerActor;
            _playerState = playerStateMachine;
            _steerSpeed = steerSpeed;        
        }

        public void Enter()
        {
            _movement = new ExploreMovement();
            _movement.Init(_playerActor, _playerState, _steerSpeed);
            _playerActor.SetMovementType(_movement);
        }

        public IEnumerator Execute()
        {
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
            if(_movement != null)
            {
                _movement.Update();
                _playerActor.AnimatorController.RunActivate(_movement.IsMove);
            }            
        }
    }
}