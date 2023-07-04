using DG.Tweening;
using GUS.Core.Clicker;
using GUS.Player;
using UnityEngine;

namespace GUS.Objects.Enemies
{
    public class ManEnemy : MonoBehaviour, IEnemy
    {
        private const float attackOffsetZ = 1f;

        [SerializeField] private Animator _manAnimator;
        [SerializeField] private Animator _bagAnimator;
        [SerializeField] private BagController _bagController;
        [SerializeField] private float _speedMovement;

        private Vector3 _standartPos;
        private float _animSpeed;
        private bool _isAlive;

        public bool IsAlive => _isAlive;

        public void Behaviour(EnemyStage stage)
        {
            _bagController.Behaviour(stage);
        }

        public void Death()
        {
            _isAlive = false;
            _manAnimator.speed = _animSpeed;
            _bagAnimator.speed = _animSpeed;
            transform.DOMove(Vector3.forward * 45, 5).SetEase(Ease.InCirc).OnComplete(() => gameObject.SetActive(false));
        }

        public void Init(ClickerGame clicker)
        {
            _isAlive = true;
            gameObject.SetActive(true);
            _animSpeed = _manAnimator.speed;                     
        }

        public void SlowMo(bool flag)
        {
            if(flag)
            {
                _manAnimator.speed = _animSpeed / _speedMovement;
                _bagAnimator.speed = _animSpeed / _speedMovement;
            }
            else
            {
                _manAnimator.speed = _animSpeed;
                _bagAnimator.speed = _animSpeed;
            }
            
        }

        public void MoveToDamage(bool flag,float time)
        {
            if (flag)
            {
                transform.DOMoveZ(_standartPos.z + attackOffsetZ, time);
            }
            else
            {
                transform.DOMoveZ(_standartPos.z - attackOffsetZ, time);
            }
        }

        public void Move(bool flag,Vector3 pos)
        {
            _standartPos = pos;
            transform.DOMove(pos, 2);
        }

        public void Paused(bool flag)
        {
            if (flag)
            {
                _manAnimator.speed = 0;
                _bagAnimator.speed = 0;
            }
            else
            {
                _manAnimator.speed = _animSpeed;
                _bagAnimator.speed = _animSpeed;
            }
        }

        private void OnDisable()
        {
            gameObject.SetActive(false);
        }
    }
}

