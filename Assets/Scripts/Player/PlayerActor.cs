using DG.Tweening;
using GUS.Core;
using GUS.Core.Data;
using GUS.Core.GameState;
using GUS.Core.Hub;
using GUS.Core.InputSys;
using GUS.Core.Locator;
using GUS.Core.Weapon;
using GUS.LevelBuild;
using GUS.Objects.PowerUps;
using GUS.Player.Movement;
using GUS.Player.State;
using System;
using UnityEngine;

namespace GUS.Player
{
    public class PlayerActor : MonoBehaviour
    {        
        [SerializeField] private CharacterController _controller;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private CapsuleCollider _capsuleCollider;
        [SerializeField] private AnimatorController _animator;
        [SerializeField] private PowerUpHandler _powerUpHandler;      
        [SerializeField] private ParticleController _particleController;
        [SerializeField] private Transform _model;

        private PlayerStateMachine _playerStateMachine;
        private Vector3 _startPosition;
        private GameStateController _stateController;
        private Wallet _wallet;
        private CameraRunController _cameraController;
        private AudioService _audioService;
        private WorldController _worldController;
        private IWeapon _weapon;
        private IInputType _inputType;
        private IMovement _movement;


        private bool _isDamage;

        #region Properties
        public CameraRunController CameraController => _cameraController;
        public ParticleController Particles => _particleController;
        public IMovement MovementType => _movement;
        public IInputType InputType => _inputType;
        public IWeapon Weapon => _weapon;
        public CharacterController CharController => _controller;
        public Wallet Wallet => _wallet;
        public PowerUpHandler PowerUpHandler => _powerUpHandler;
        public AnimatorController AnimatorController => _animator;
        public CapsuleCollider Collider => _capsuleCollider;
        public IServiceLocator ServiceLocator { get; private set; }
        #endregion
        private void Start()
        {
            _weapon = GetComponentInChildren<IWeapon>();
            _startPosition= transform.position;
        }

        public void Init(IServiceLocator serviceLocator)
        {
            
            _inputType = serviceLocator.Get<IInputType>();
            _cameraController = serviceLocator.Get<ICamera>() as CameraRunController;
            _stateController = serviceLocator.Get<IStateChanger>() as GameStateController;            
            _audioService= serviceLocator.Get<AudioService>();
            _wallet = serviceLocator.Get<Wallet>();
            _worldController = serviceLocator.Get<WorldController>();
            _playerStateMachine = serviceLocator.Get<PlayerStateMachine>();

            ServiceLocator = serviceLocator;
        }

        public void SetMovementType(IMovement movement)
        {
            _movement = movement;
        }

        public void Death()
        {
            _audioService.PlaySFX(_audioService.Data.death);
            _particleController.DeathEffect(true);
            _cameraController.ShackeCameraHandle(5, 0.2f);
            _stateController.EndGame();
        }  

        public void RestartPosition()
        {
            transform.DOMove(_startPosition, 0.2f);
            _isDamage = false;
        }

        public void BackObstacle()
        {
            if (!_isDamage)
            {
                _isDamage = true;
                _worldController.ChangeAcceleration(1);
            }
            else
            {
                Death();
            }

            _audioService.PlaySFX(_audioService.Data.quack);
        }

        public void Collect()
        {
            _wallet.AddOne();
            _audioService.PlaySFX(_audioService.Data.cornPickUp);
        }

        public void CameraHandler(RunMovement movement)
        {
            _cameraController.CameraCalculate(movement);
        }

        public void ChangeModelPos(float offset, float time)
        {
            _model.transform.DOLocalMoveY(offset, time);
        }

        private void Update()
        {
            if(_playerStateMachine!= null)
            {
                _playerStateMachine.Update();
            }
        }

        private void FixedUpdate()
        {
            if(_playerStateMachine != null)
            {
                _playerStateMachine.FixedUpdate();
            }
        }
    }
}