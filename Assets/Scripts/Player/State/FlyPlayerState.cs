using GUS.Core;
using GUS.Player.Movement;
using System.Collections;

namespace GUS.Player.State
{
    public class FlyPlayerState : IState
    {
        private FlyMovement _movement;
        public FlyPlayerState(float flySpeed,IMovement movement,PlayerActor playerActor,PlayerStateMachine playerState) {

            _movement = new FlyMovement();  //образование движения полетом тут вредно
            _movement.Init(playerActor, playerState, flySpeed);
        }
        public void Enter()
        {
            _movement.HoldTypeHandler(true);
        }

        public IEnumerator Execute()
        {
            yield return null;
        }

        public void Exit()
        {
            _movement.HoldTypeHandler(false);
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