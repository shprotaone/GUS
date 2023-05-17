using DG.Tweening;
using GUS.Core;
using System.Collections;

namespace GUS.Player.State
{
    public class PausePlayerState : IState
    {
        private PlayerActor _player;
        private PlayerStateMachine _playerStateMachine;

        public PausePlayerState(PlayerActor player, PlayerStateMachine playerStateMachine)
        {
            _player = player;
            _playerStateMachine = playerStateMachine;
            
        }

        public void Enter()
        {
            _player.AnimatorController.Pause(true);
        }

        public IEnumerator Execute()
        {
            yield return null;
        }

        public void Exit()
        {
            _player.AnimatorController.Pause(false);
        }

        public void FixedUpdate()
        {
            
        }

        public void Update()
        {
            
        }
    }
}