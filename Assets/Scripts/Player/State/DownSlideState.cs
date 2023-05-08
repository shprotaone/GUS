using DG.Tweening;
using GUS.Core;
using System.Collections;
using UnityEngine;

namespace GUS.Player.State
{
    public class DownSlideState :IState
    {
        private float _downSlideTime;
        private PlayerActor _player;
        private PlayerStateMachine _playerStateMachine;
        private IMovement _movement;

        public DownSlideState(float downSlideTime,  PlayerActor player, PlayerStateMachine playerStateMachine)
        {
            this._downSlideTime = downSlideTime;
            this._player = player;
            this._playerStateMachine = playerStateMachine;
        }

        public void Enter()
        {
            _movement = _player.MovementType;
        }

        public IEnumerator Execute()
        {
            _player.transform.localScale = Vector3.one/2;

            yield return new WaitForSeconds(_downSlideTime);
            _player.transform.localScale = Vector3.one;

            _playerStateMachine.TransitionTo(_playerStateMachine.runState);
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
            _movement.Update();
        }
    }
}