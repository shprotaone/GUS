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
        private AnimatorController _animatorController;
        private ParticleController _particleController;
        private SmartphoneInput _inputType;
        private EnumBind _action;

        public void Init(PlayerActor player, PlayerStateMachine playerState, float speedMovement)
        {
            _playerStateMachine = playerState;
            _animatorController = player.AnimatorController;
            _inputType = (SmartphoneInput)player.InputType;
            _particleController = player.Particles;
            
        }
        public void Fire()
        {           
            if (_inputType.Firing() == EnumBind.Fire)
            {
                OnClick?.Invoke();
                
                _particleController.DamageEffect(_inputType.StartPosition);
            }
            else if(_inputType.Firing() == EnumBind.FireHold)
            {
                _animatorController.BiteActivate(true);
            }
            else
            {
                _animatorController.BiteActivate(false);
            }
        }

        public void FixedUpdate()
        {
            
        }       
        public void Move()
        {
            
        }

        public void CanMove(bool flag)
        {
            
        }

        public void Update()
        {
            Fire();
        }
    }
}

