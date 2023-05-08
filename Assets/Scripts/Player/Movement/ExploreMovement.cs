using GUS.Core.InputSys.Joiystick;
using GUS.Player.State;
using UnityEngine;

namespace GUS.Player.Movement 
{
    public class ExploreMovement : IMovement
    {
        private PlayerStateMachine _playerState;
        private PlayerActor _playActor;
        private FloatingJoystick _inputType;

        private Camera _camera;
        private Vector3 _direction;
        private EnumBind _movementAction;
        private EnumBind _action;
        private float _speedMovement;
        private float turnSmoothTime = 0.1f;
        private float turnSmoothVelocity;

        private bool _canMove;

        public void Init(PlayerActor player, PlayerStateMachine playerState, float speedMovement)
        {
            _playActor = player;
            if (player.InputType is FloatingJoystick)
            {
                _inputType = (FloatingJoystick)player.InputType;
            }
            _playerState = playerState;
            _speedMovement = speedMovement;
            _camera = Camera.main;
            _canMove = true;
        }

        public void Update()
        {
            if(_canMove)
            {
                Move();
                Fire();
            }           
        }

        public void Fire()
        {
            _action = _inputType.Movement();
            if (_action == EnumBind.Fire)
            {
                Debug.Log("Action");
            }
        }

        public void Move()
        {
            float horizontal = _inputType.Horizontal;
            float vertical = _inputType.Vertical;

            Vector3 direction = new Vector3(horizontal, 0f, vertical);

            if (direction.magnitude >= 0.1)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.transform.rotation.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(_playActor.transform.rotation.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                _playActor.transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                _playActor.CharController.Move(moveDir * _speedMovement * Time.deltaTime);
            }
        }

        public void StopMovement(bool flag)
        {
            _canMove = flag;
        }

        public void FixedUpdate()
        {
            
        }
    }
}


