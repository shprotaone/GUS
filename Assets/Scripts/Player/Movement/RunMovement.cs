using GUS.Core.InputSys;
using GUS.Core.Locator;
using GUS.Player.State;
using UnityEngine;

namespace GUS.Player.Movement
{
    public class RunMovement : IMovement
    {
        private Vector3 _startPosition;
        private PlayerStateMachine _playerState;
        private PlayerActor _player;
        private IInputType _inputType;
        private Vector3 _targetPosition;
        //private Vector3 _direction;
        private EnumBind _movementAction;
        private EnumBind _action;

        private float _distance;
        private float _speedMovement;
        private float _gravity;
        private float _gravityScale;
        private float _verticalVelocity;

        private bool isLeft = false;
        private bool isRight = false;
        private bool _canMoved;

        public Vector3 TargetPos => _targetPosition;
        public bool IsGrounded { get;private set; }
        public void Init(PlayerActor player, PlayerStateMachine playerState, float speedMovement)
        {
            _player = player;
            _inputType = player.InputType;
            _playerState = playerState;
            _speedMovement = speedMovement;            
            _canMoved = true;
            _targetPosition = _player.transform.position;
        }

        public void SetDistance(float distance) => _distance = distance;
        public void SetGravity(float gravity, float gravityScale)
        {
            _gravityScale = gravityScale;
            _gravity = gravity;
        }

        public void Update()
        {
            if(_inputType != null && _canMoved)
            {
                Move();
                CheckMove();
                Jump();
                Crunch();

                Fire();
            }
        }

        public void FixedUpdate()
        {
            if(_inputType != null && _canMoved)
            {
                
                CheckGravity();                               
            }          
        }

        public void Fire()
        {
            //_action = _inputType.Movement();
            //if (_action == EnumBind.Fire)
            //{
            //    _playerState.ToActionState(_playerState.attackState);
            //}
            //else
            //{
            //    _playerState.ActionState.Exit();
            //}
        }

        public void Move()
        {
            _movementAction = _inputType.Movement();
            float tmpDist = Time.deltaTime * _speedMovement;

            //_direction = Vector3.zero;
            Vector3 direction = new Vector3(0, 0, 0);

            direction.x = Mathf.Lerp(_player.transform.position.x, _targetPosition.x, tmpDist) - _player.transform.position.x;
            direction.y = _verticalVelocity * Time.deltaTime;

            _player.CharController.Move(direction);

            if (_movementAction == EnumBind.Left && !isLeft)
            {
                _targetPosition.x -= _distance;
            }
            else if (_movementAction == EnumBind.Right && !isRight)
            {
                _targetPosition.x += _distance;
            }

            ResetPosition();
        }

        private void Jump()
        {
            if (_movementAction == EnumBind.Up && _player.CharController.isGrounded)
            {
                _playerState.TransitionTo(_playerState.jumpState);
            }
        }

        private void Crunch()
        {
            if (_movementAction == EnumBind.Down)
            {
                _playerState.TransitionTo(_playerState.downslide);
            }
        }

        private void ResetPosition()
        {
            isLeft = false;
            isRight = false;
        }

        private void CheckMove()
        {
            if (_targetPosition.x == _distance)
            {
                isLeft = false;
                isRight = true;
            }
            else if (_targetPosition.x == -_distance)
            {
                isLeft = true;
                isRight = false;
            }
            else
            {
                isLeft = false;
                isRight = false;
            }
        }

        private void CheckGravity()
        {
            IsGrounded = _player.CharController.isGrounded;

            if (IsGrounded && _verticalVelocity < 0.1f)
            {
                _verticalVelocity = -1;
            }
            else
            {
                _verticalVelocity += _gravity * _gravityScale * Time.deltaTime;
            }
        }

        private bool OnSLope()
        {
            RaycastHit hit;

            if (Physics.Raycast(_player.transform.position, Vector3.down, out hit, _player.CharController.height / 2))
            {
                if(hit.normal != Vector3.up)
                {
                    return true;
                }
            }
            return false;
        }

        public void ChangeVerticalVelocity(float velocity)
        {
            _verticalVelocity = velocity;
        }

        public void StopMovement(bool flag)
        {
            _canMoved = flag;
            _targetPosition = _startPosition;
        }

        public void ReturnPosition()
        {
            _targetPosition = _startPosition;
            ResetPosition();
            Debug.Log("Back");
        }
    }
}

