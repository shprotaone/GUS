using GUS.Core;
using GUS.Player.Movement;
using System.Collections;

namespace GUS.Player.State
{
    public class ExplorePlayerState : IState
    {
        private PlayerActor _player;
        private PlayerStateMachine _playerState;
        private ExploreMovement _movement;
        public ExplorePlayerState(float steerSpeed, PlayerActor playerActor,PlayerStateMachine playerStateMachine)
        {
            _movement = new ExploreMovement();
            _movement.Init(playerActor, playerStateMachine, steerSpeed);
        
        }
        public void Enter()
        {
            _player?.SetMovementType(_movement);
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
            _movement.Update();
        }
    }
}