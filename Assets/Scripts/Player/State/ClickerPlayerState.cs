using GUS.Core;
using GUS.Player.Movement;
using System.Collections;
using UnityEngine.Playables;

namespace GUS.Player.State
{
    public class ClickerPlayerState : IState
    {
        private PlayerActor _actor;
        private ClickerMovement _clickerMovement;
        private PlayerStateMachine _playerStateMachine;

        public ClickerPlayerState(PlayerActor actor, PlayerStateMachine playerState)
        {
            _playerStateMachine = playerState;
            _actor = actor;
            _clickerMovement = new ClickerMovement();                     
        }
        public void Enter()
        {
            _actor.RestartPosition();
            _actor.SetMovementType(_clickerMovement);
            _clickerMovement.Init(_actor, _playerStateMachine, 0);
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
            _clickerMovement.Update();
        }
    }
}