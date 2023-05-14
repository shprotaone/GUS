using GUS.Core.InputSys;
using GUS.Player.State;
using System;
using UnityEngine;

namespace GUS.Player.Movement
{
    public class ClickerMovement : IMovement
    {
        public event Action OnClick;
        private PlayerStateMachine _playerStateMachine;
        private SmartphoneInput _inputType;
        private EnumBind _action;

        public void Init(PlayerActor player, PlayerStateMachine playerState, float speedMovement)
        {
            _playerStateMachine = playerState;
            _inputType = (SmartphoneInput)player.InputType;
            
        }
        public void Fire()
        {
            _inputType.Firing();

            if (_action == EnumBind.Fire)
            {
                Debug.Log("Click");
                OnClick?.Invoke();
            }
        }

        public void FixedUpdate()
        {
            
        }       
        public void Move()
        {
            
        }

        public void StopMovement(bool flag)
        {
            
        }

        public void Update()
        {
            Fire();
        }
    }
}

