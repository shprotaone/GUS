using GUS.Core;
using GUS.Core.GameState;
using GUS.Player.Movement;
using System.Collections;

namespace GUS.Player.State
{
    public class ClickerPlayerState : IState
    {
        private PlayerActor _actor;
        private ClickerMovement _clickerMovement;
        private PlayerStateMachine _playerStateMachine;
        public IStateMachine StateMachine => _playerStateMachine;
        public ClickerPlayerState(PlayerActor actor, PlayerStateMachine playerState)
        {
            _playerStateMachine = playerState;
            _actor = actor;                               
        }
        
        public void Enter()
        {            
            _actor.AnimatorController.RunActivate(true);
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
            if(_clickerMovement!= null)
            {
                _clickerMovement.Update();
            }        
        }
    }
}