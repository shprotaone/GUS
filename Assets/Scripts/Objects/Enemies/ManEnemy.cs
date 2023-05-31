using DG.Tweening;
using GUS.Core.Clicker;
using UnityEngine;

namespace GUS.Objects.Enemies
{
    public class ManEnemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private Animator _manAnimator;
        [SerializeField] private Animator _bagAnimator;
        [SerializeField] private BagController _bagController;

        private ClickerGame _game;
        private int _step;
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
            transform.DOMove(Vector3.left * 10, 2).OnComplete(() => gameObject.SetActive(false));
        }

        public void Init(ClickerGame clicker)
        {
            _isAlive = true;
            gameObject.SetActive(true);
            _animSpeed = _manAnimator.speed;
            _game = clicker;
            //transform.DOMove(clicker.RunPoint.position, 2);
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

