using GUS.Core;
using GUS.Core.GameState;
using GUS.Player.Movement;
using System.Collections;
using UnityEngine;

namespace GUS.Player.State
{
    public class InitPlayerState : IState
    {  
        private PlayerActor _player;
        public IStateMachine StateMachine { get;private set; }

        public InitPlayerState(IStateMachine stateMachine, PlayerActor player)
        {            
            this._player = player;
            StateMachine = stateMachine;
        }

        public void Enter()
        {
            _player.AnimatorController.RunActivate(true);
            _player.Particles.AfterDeathDisabler();
            _player.ResetDeath();
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