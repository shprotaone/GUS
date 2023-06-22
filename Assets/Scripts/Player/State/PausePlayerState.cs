using DG.Tweening;
using GUS.Core;
using GUS.Core.GameState;
using System.Collections;

namespace GUS.Player.State
{
    public class PausePlayerState : IState
    {
        private PlayerActor _player;

        public IStateMachine StateMachine {get;private set;}
        public PausePlayerState(IStateMachine stateMachine, PlayerActor player)
        {
            _player = player;
            
            StateMachine = stateMachine;
        }

        public void Enter()
        {
            _player.AnimatorController.Pause(true);
            _player.MovementType.CanMove(false);
        }

        public IEnumerator Execute()
        {
            yield return null;
        }

        public void Exit()
        {
            _player.AnimatorController.Pause(false);
            _player.MovementType.CanMove(true);
        }

        public void FixedUpdate()
        {
            
        }

        public void Update()
        {
            
        }
    }
}