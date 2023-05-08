using GUS.Core;
using System.Collections;

namespace GUS.Player.State
{
    public class AttackPlayerState : IState
    {
        private PlayerActor _actor;
        public AttackPlayerState(PlayerActor actor) 
        {
            _actor = actor;
            
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