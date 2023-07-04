using DG.Tweening;
using GUS.Core.InputSys;
using GUS.Core.Locator;
using GUS.Player.State;
using GUS.Utils;
using System;
using UnityEngine;

namespace GUS.Player.Movement
{
    public class RunMovement : IMovement
    {
        public Action OnChangePosition;
        
        private PlayerStateMachine _playerState;
        private PlayerActor _player;
        private IInputType _inputType;
        private Vector3 _startPosition;
        private Vector3 _targetPosition;
        private Vector3 _direction;
        private EnumBind _movementAction;
        private ActorRotator _rotator;
        private AudioService _audioService;

        private float _distance;
        private float _speedMovement;
        private float _gravity;
        private float _gravityScale;
        private float _verticalVelocity;

        private Line _currentLine;
        private bool _canMoved;

        public Line Line => _currentLine;

        public bool IsGrounded { get;private set; }
        public void Init(PlayerActor player, PlayerStateMachine playerState, float speedMovement)
        {
            _player = player;
            _inputType = player.InputType;
            _playerState = playerState;
            _speedMovement = speedMovement;            
            _canMoved = true;
            _startPosition = _player.transform.position;
            _currentLine = Line.Center;
            _rotator = new ActorRotator(player);
            _audioService = player.ServiceLocator.Get<AudioService>();
            OnChangePosition += _player.Particles.SlideEffect;
            OnChangePosition += CheckLinePosition;
            OnChangePosition += () => _player.CameraHandler(this);
        }

        public void SetDistance(float distance) => _distance = distance;

        public void SetGravity(float gravity) => _gravity = gravity;

        public void SetGravityScale(float gravityScale) => _gravityScale = gravityScale;

        public void Update()
        {
            _movementAction = _inputType.Movement();

            if (_inputType != null && _canMoved)
            {
                Move();
                Jump();
                DownSlide();
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
            float tmpDist = Time.deltaTime * _speedMovement;

            _direction.x = Mathf.Lerp(_player.transform.position.x, _targetPosition.x, tmpDist) - _player.transform.position.x;
            _direction.y = _verticalVelocity * Time.deltaTime;

            if (_movementAction == EnumBind.Left && _currentLine != Line.Left)
            {
                _targetPosition.x -= _distance;
                _audioService.PlaySFX(_audioService.Data.swipeSound);
                OnChangePosition?.Invoke();
                _rotator.Rotate(Line.Left);
            }
            else if (_movementAction == EnumBind.Right && _currentLine != Line.Right)
            {
                _targetPosition.x += _distance;
                _audioService.PlaySFX(_audioService.Data.swipeSound);
                OnChangePosition?.Invoke();
                _rotator.Rotate(Line.Right);
            }

            _player.CharController.Move(_direction);
        }

        private void Jump()
        {
            if (_movementAction == EnumBind.Up && _player.CharController.isGrounded)
            {
                _playerState.TransitionTo(_playerState.jumpState);
                _audioService.PlaySFX(_audioService.Data.swipeSound);
            }            
        }

        private void DownSlide()
        {
            if (_movementAction == EnumBind.Down/* && _player.CharController.isGrounded*/)
            {
                _playerState.TransitionTo(_playerState.downslide);
                _audioService.PlaySFX(_audioService.Data.swipeSound);
            }
        }

        public void ResetPosition()
        {
            _currentLine = Line.Center;
        }

        private void CheckLinePosition()
        {
            if (_targetPosition.x == _distance)
            {             
                _currentLine = Line.Right;
            }
            else if (_targetPosition.x == -_distance)
            {              
                _currentLine = Line.Left;
            }
            else
            {
                _currentLine= Line.Center;
            }
        }

        private void CheckGravity()
        {
            IsGrounded = _player.CharController.isGrounded;

            if (IsGrounded && _verticalVelocity < 0.1f)
            {
                _verticalVelocity = -1;                
                _gravityScale = _playerState.LevelSettings.gravityScale;
            }
            else
            {
                _verticalVelocity += _gravity * _gravityScale * Time.deltaTime;
            }
        }

        public void ChangeVerticalVelocity(float velocity)
        {
            _verticalVelocity = velocity;
        }

        public void CanMove(bool flag)
        {
            _canMoved = flag;
        }

        public void ReturnObstaclePosition()
        {
            _targetPosition.x = _startPosition.x;
            _player.BackObstacle();
            ResetPosition();
            CheckLinePosition();
            OnChangePosition?.Invoke();
        }

        public void ReturnPosition()
        {
            _targetPosition = _startPosition;
            ResetPosition();
            CheckLinePosition();
            //OnChangePosition?.Invoke();
        }

        public void CallMove(EnumBind enumBind)
        {
            _movementAction = enumBind;

            switch (enumBind)
            {
                case EnumBind.Up:               
                    Jump();
                    break;
                case EnumBind.Down: 
                    DownSlide(); 
                    break;                        
            }            
        }
    }

}

