using DG.Tweening;
using GUS.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace GUS.Player
{
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField] private Rig _neckRig;
        [SerializeField] private Transform _neckTarget;
        [SerializeField] private float _biteAnimTime;
        [SerializeField] private Animator _animator;
        [SerializeField] private ParticleController _particleController;

        private int RunStateId = Animator.StringToHash("Run");
        private int JumpId = Animator.StringToHash("Jump");
        private int CrouchID = Animator.StringToHash("Crouch");
        private int RunSpeedMultiply = Animator.StringToHash("RunMultiplyer");
        private int DeathId = Animator.StringToHash("Death");
        private int SteeringRightID = Animator.StringToHash("Right");
        private int SteeringLeftID = Animator.StringToHash("Left");
        private int BiteID = Animator.StringToHash("Bite");

        private float _prevSpeed;
        private bool _inByte;
        private void Start()
        {
            _neckTarget.position += Vector3.forward;
            _neckTarget.rotation = Quaternion.Euler(new Vector3(-40, 0, 0));
        }
        public void RunActivate(bool flag) => _animator.SetBool(RunStateId, flag);
        public void DeathActivate() => _animator.SetTrigger(DeathId);
        public void AfterDeath() => _particleController.AfterDeath();
        public void JumpActivate() => _animator.SetTrigger(JumpId);
        public void CrouchActivate() => _animator.SetTrigger(CrouchID);
        public void BiteActivate(bool flag)
        {
            if (flag)
            {
                _neckRig.weight = 1;
                //_inByte = true;
            }
            else
            {
                _neckRig.weight = 0;
                //DOVirtual.Float(1, 0, _biteAnimTime, v => _neckRig.weight = v).OnComplete(() => _inByte = false);
            }
        }
        public void Pause(bool flag)
        {
            if (flag)
            {
                _prevSpeed = _animator.speed;
                _animator.speed = 0;
            }
            else
            {
                _animator.speed = _prevSpeed;
            }
        }
        public void ChangeAnimationSpeed(float speed)
        {
            _animator.SetFloat(RunSpeedMultiply, speed / 10);
        }

        public void SteeringAnimation(int value)
        {
            if (value == 1) _animator.SetTrigger(SteeringRightID);
            else _animator.SetTrigger(SteeringLeftID);
        }
    }
}

