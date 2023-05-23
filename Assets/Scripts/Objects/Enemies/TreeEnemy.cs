using UnityEngine;

namespace GUS.Objects.Enemies
{
    public class TreeEnemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private ClickerGame _clicker;
        [SerializeField] private Apple[] _apples;

        private float _max;
        private float _step;
        private int _appleIndex = 0;

        public bool IsAlive { get; }

        public void Init(ClickerGame clicker)
        {
            _clicker = clicker;
            _max = _clicker.HP;
            _step = _clicker.HP / _apples.Length;
            _max -= _step;
            gameObject.SetActive(true);
        }

        public void Behaviour(EnemyStage stage)
        {
            //if (_max > value)
            //{
            //    _apples[_appleIndex].Fall();
            //    _appleIndex++;
            //    _max -= _step;
            //}
        }

        public void Death()
        {
            _appleIndex = 0;
            gameObject.SetActive(false);
        }

        public void Paused(bool flag)
        {

        }
    }
}

