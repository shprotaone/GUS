using DG.Tweening;
using GUS.Utils;
using UnityEngine;

namespace GUS.Player.Movement
{
    public class ActorRotator
    {
        private PlayerActor _player;
        private AnimatorController _animator;
        private Tween _tween;
        public ActorRotator(PlayerActor player)
        {
            _player = player;
            _animator = player.AnimatorController;
            _tween.SetEase(Ease.Linear);
        }

        public void Rotate(Line line)
        {
            if (line == Line.Right)
            {
                //_tween = _player.transform.DORotate(new Vector3(0, 45, 0), 0.1f);
                _animator.SteeringAnimation(1);
            }

            if (line == Line.Left)
            {
                //_tween = _player.transform.DORotate(new Vector3(0, -45, 0), 0.1f);
                _animator.SteeringAnimation(-1);
            }

            _tween.OnComplete(ReturnRotate);
        }

        private void ReturnRotate()
        {
            //_player.transform.DORotate(Vector3.zero, 0.1f);
        }
    }
}