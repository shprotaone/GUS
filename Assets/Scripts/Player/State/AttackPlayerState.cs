using GUS.Core;
using GUS.Core.GameState;
using System.Collections;

namespace GUS.Player.State
{
    public class AttackPlayerState : IState
    {
        private PlayerActor _actor;
        public IStateMachine StateMachine { get; private set; }
        public AttackPlayerState(PlayerStateMachine stateMachine,PlayerActor actor) 
        {
            _actor = actor;
            StateMachine = stateMachine;
        }

        
        public void Enter()
        {
            _actor.Weapon.Fire();
        }

        public IEnumerator Execute()
        {
            yield return null;
        }

        public void Exit()
        {
            _actor.Weapon.UnFire();
        }

        public void FixedUpdate()
        {
            
        }

        public void Update()
        {
            
        }
    }
}