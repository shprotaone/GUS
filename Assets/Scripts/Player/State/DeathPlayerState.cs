
using GUS.Core;
using GUS.Core.GameState;
using GUS.Player.Movement;
using System.Collections;

namespace GUS.Player.State
{
    public class DeathPlayerState: IState
    {
        private PlayerActor _player;
        private IMovement _movement;
        public IStateMachine StateMachine { get; private set; }
        public DeathPlayerState(IStateMachine stateMachine, PlayerActor player)
        {
            _player = player;
            StateMachine = stateMachine;    
        }
        
        public void Enter()
        {
            _movement = _player.MovementType;
            _player.AnimatorController.DeathActivate();
            _movement.CanMove(false);
        }

        public IEnumerator Execute()
        {           
            yield return null;
        }

        public void Exit()
        {
            if(_movement is RunMovement move)
                move.ReturnPosition();

            _player.Particles.DeathEffect(false);
        }

        public void FixedUpdate()
        {
            
        }

        public void Update()
        {
            
        }
    }
}