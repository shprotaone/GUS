using GUS.Core.InputSys;
using GUS.Player.State;
using UnityEngine;

namespace GUS.Player.Movement
{
    public class FlyMovement : IMovement
    {
        private PlayerStateMachine _playerState;
        private PlayerActor _playActor;
        private IInputType _inputType;

        private EnumBind _movementAction;
        private EnumBind _action;
        private Vector3 _target = new Vector3(0,10,0);
        private float _speedMovement;
        private bool _canMove;

        public void Init(PlayerActor player, PlayerStateMachine playerState, float speedMovement)
        {
            _playActor = player;
            _inputType = player.InputType;
            _playerState = playerState;           
            _speedMovement = speedMovement;
            _canMove = true;
        }

        public void Update()
        {
            if (_canMove)
            {
                Move();
                Fire();
            }          
        }

        public void Fire()
        {
            _action = _inputType.Movement();

            if(_action == EnumBind.Fire)
            {
                _playerState.ToActionState(_playerState.attackState);
            }
            else if(_action == EnumBind.None && _playerState.ActionState!= null)
            {
                _playerState.ActionState.Exit();
            }
            
        }      

        public void Move()  //спуск работает не корректно
        {
            //float tmpDist = Time.deltaTime * _speedMovement;
            //Vector3 movement = Vector3.MoveTowards(_playActor.transform.position, _target, tmpDist) - _playActor.transform.position;

            //_movementAction = _inputType.Movement();
            //if (_movementAction == EnumBind.Up && _target.y < flyRange + 10)
            //{
            //    _target += Vector3.up;
            //}

            //if (_movementAction == EnumBind.Down && _target.y > -flyRange - 10)
            //{
            //    _target += Vector3.down;
            //}

            //if (_movementAction == EnumBind.Left && _target.x > -flyRange)
            //{
            //    _target += Vector3.left;
            //}

            //if (_movementAction == EnumBind.Right && _target.x < flyRange)
            //{
            //    _target += Vector3.right;
            //}

            //_playActor.CharController.Move(movement);
        }               

        public void HoldTypeHandler(bool flag)
        {
            if (_inputType is Keyboard keyboard) keyboard.SetHold(flag);
        }

        public void CanMove(bool flag)
        {
            _canMove = flag;
        }

        public void FixedUpdate()
        {
            
        }

        public void CallMove(EnumBind enumBind)
        {
            
        }
    }
}