using GUS.Core;
using System.Collections;
using UnityEngine;

namespace GUS.Player.State
{
    public class InitPlayerState : IState
    {
        private PlayerStateMachine _playerState;       
        private PlayerActor _player;
        
        public InitPlayerState(PlayerActor player, PlayerStateMachine playerState)
        {            
            this._player = player;
            _playerState = playerState;
        }

        public void Enter()
        {
            _player.RestartPosition();
            _player.AnimatorController.RunActivate(true);
            _player.Particles.AfterDeathDisabler();
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