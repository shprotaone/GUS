using DG.Tweening;
using GUS.Core;
using System.Collections;
using UnityEngine;

namespace GUS.Player.State
{
    public class DeathPlayerState: IState
    {
        private PlayerActor _player;
        private IMovement _movement;

        public DeathPlayerState(PlayerActor player)
        {
            _player = player;
        }

        public void Enter()
        {
            _movement = _player.MovementType;
            _player.AnimatorController.DeathActivate();
            _movement.CanMove(false);
        }

        public IEnumerator Execute()
        {           
            yield return null;
        }

        public void Exit()
        {
            _movement.CanMove(true);
            _player.AnimatorController.DeathActivate();
        }

        public void FixedUpdate()
        {
            
        }

        public void Update()
        {
            
        }
    }
}