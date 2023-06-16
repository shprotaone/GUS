using GUS.Core;
using GUS.Core.GameState;
using GUS.Core.Locator;
using System.Collections;

namespace GUS.Player.State
{
    public class IdlePlayerState : IState
    {
        private PlayerActor _player;
        public IStateMachine StateMachine {get; private set;}

        public IdlePlayerState(IServiceLocator serviceLocator)
        {
            _player = serviceLocator.Get<PlayerActor>();
        }

        public void Enter()
        {
            _player.SetMovementType(null);
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
            
        }
    }
}