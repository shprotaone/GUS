using DG.Tweening;
using GUS.Core.InputSys;
using GUS.Player.State;
using System;
using UnityEngine;

namespace GUS.Player.Movement
{
    public class ClickerMovement : IMovement
    {
        public event Action OnClick;
        private AnimatorController _animatorController; //анимации и партиклы вынести отсюда? 
        private ParticleController _particleController;
        private SmartphoneInput _inputType;
        private EnumBind _action;

        private bool _canAttack;

        public void Init(PlayerActor player, PlayerStateMachine playerState, float speedMovement)
        {
            _animatorController = player.AnimatorController;
            _inputType = (SmartphoneInput)player.InputType;
            _particleController = player.Particles;
            _canAttack = false;
            
        }
        public void Fire()
        {          
            if(_canAttack )
            {
                if (_inputType.Firing() == EnumBind.Fire)
                {
                    OnClick?.Invoke();
                    _animatorController.BiteActivate(true);
                    DOVirtual.DelayedCall(0.05f, () => _animatorController.BiteActivate(false));
                    _particleController.DamageEffect(_inputType.StartPosition);
                    Debug.Log("TAP");
                }
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

        public bool CanAttack(bool flag) => _canAttack = flag;

        public void CallMove(EnumBind enumBind)
        {
            
        }
    }
}

