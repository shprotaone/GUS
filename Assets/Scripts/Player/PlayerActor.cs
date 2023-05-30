using DG.Tweening;
using GUS.Core;
using GUS.Core.GameState;
using GUS.Core.InputSys;
using GUS.Core.Locator;
using GUS.Core.Weapon;
using GUS.Objects.PowerUps;
using GUS.Player.Movement;
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
        [SerializeField] private Transform _bossPosition;

        private Vector3 _startPosition;
        private GameStateController _stateController;
        private Wallet _wallet;
        private CameraRunController _cameraController;
        private IWeapon _weapon;
        private IInputType _inputType;
        private IMovement _movement;
        private AudioService _audioService;

        #region Properties
        public CameraRunController CameraController => _cameraController;
        public ParticleController Particles => _particleController;
        public IMovement MovementType => _movement;
        public IInputType InputType => _inputType;
        public IWeapon Weapon => _weapon;
        public CharacterController CharController => _controller;
        public GameStateController GameStateController => _stateController;
        public Wallet Wallet => _wallet;
        public PowerUpHandler PowerUpHandler => _powerUpHandler;
        public AnimatorController AnimatorController => _animator;
        public CapsuleCollider Collider => _capsuleCollider;
        public Transform BossPosition => _bossPosition;
        public AudioService AudioService => _audioService;
        #endregion
        private void Start()
        {
            _weapon = GetComponentInChildren<IWeapon>();
            _startPosition= transform.position;
        }

        public void Init(IInputType inputType,IServiceLocator serviceLocator)
        {
            _inputType = inputType;
            if(serviceLocator.Get<ICamera>() is CameraRunController cam)
            {
                _cameraController = cam;
            }
            
            _stateController = serviceLocator.Get<GameStateController>();
            _audioService= serviceLocator.Get<AudioService>();
            _wallet = serviceLocator.Get<Wallet>();
        }

        public void SetMovementType(IMovement movement)
        {
            _movement = movement;
        }

        public void Death()
        {
            _audioService.PlaySFX(_audioService.Data.death);
            _particleController.DeathEffect(true);
            StartCoroutine(_cameraController.ShakeCamera(5, 0.2f));
            _stateController.EndGame();
        }  

        public void RestartPosition()
        {
            transform.DOMove(_startPosition, 0.2f);
        }

        public void PlayBackSound()
        {
            _audioService.PlaySFX(_audioService.Data.quack);
        }
        //public void SmoothSecondLevel(bool isOn)
        //{
        //    if (isOn) _rigidbody.interpolation = RigidbodyInterpolation.Extrapolate;
        //    else _rigidbody.interpolation = RigidbodyInterpolation.None;
        //}

        public void ChangeGameType(bool isRunner)
        {
            if (isRunner) _stateController.StartGame();
            else _stateController.ClickerGame();
        }

        public void Collect()
        {
            _wallet.AddCoin();
            _audioService.PlaySFX(_audioService.Data.cornPickUp);
        }

        public void CameraHandler(RunMovement movement)
        {
            _cameraController.CameraCalculate(movement);
        }
    }
}