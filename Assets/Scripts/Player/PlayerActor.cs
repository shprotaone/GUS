using GUS.Core;
using GUS.Core.InputSys;
using GUS.Core.Locator;
using GUS.Core.Weapon;
using GUS.Player.Movement;
using System;
using TMPro;
using UnityEngine;

namespace GUS.Player
{
    public class PlayerActor : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private Rigidbody _rigidbody;
              
        private GameStateController _stateController;
        private IWeapon _weapon;
        private IInputType  _inputType;
        private IMovement _movement;
        private Wallet _wallet;
        private Vector3 _dir;

        public Rigidbody Rigidbody => _rigidbody;
        public IMovement MovementType => _movement;
        public CharacterController CharController => _controller;
        public IInputType InputType => _inputType;
        public IWeapon Weapon => _weapon;

        private void Start()
        {
            _weapon = GetComponentInChildren<IWeapon>();
        }

        public void Init(IInputType inputType,IServiceLocator serviceLocator)
        {
            _inputType = inputType;
            _stateController = serviceLocator.Get<GameStateController>();   
            _wallet = serviceLocator.Get<Wallet>();
        }

        public void SetMovementType(IMovement movement)
        {
            _movement = movement;
        }

        public void Death()
        {
            _stateController.EndGame();
        }  

        public void RestartPosition()
        {
            
        }

        public void SmoothSecondLevel(bool isOn)
        {
            if (isOn) _rigidbody.interpolation = RigidbodyInterpolation.Extrapolate;
            else _rigidbody.interpolation = RigidbodyInterpolation.None;
        }

        public void Collect()
        {
            _wallet.AddCoin();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            if(_movement is RunMovement movement)
            {
                Vector3 point = movement.TargetPos;
                Gizmos.DrawSphere(point, 1);
            }                     
        }
    }
}