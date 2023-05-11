using GUS.Core.InputSys;
using GUS.Player.State;
using System;

namespace GUS.Player.Movement
{
    public class ClickerMovement : IMovement
    {
        public event Action OnClick;
        private PlayerStateMachine _playerStateMachine;
        private IInputType _inputType;
        private EnumBind _action;

        public void Init(PlayerActor player, PlayerStateMachine playerState, float speedMovement)
        {
            _playerStateMachine = playerState;
            _inputType = player.InputType;

        }
        public void Fire()
        {
           
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
            _action = _inputType.Movement();

            if (_action == EnumBind.Fire)
            {
                OnClick?.Invoke();
            }
        }
    }
}

