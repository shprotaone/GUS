using GUS.Core.UI;
using GUS.Player;
using UnityEngine;

namespace GUS.Objects.PowerUps
{
    public class PowerUpHandler : MonoBehaviour
    {
        [SerializeField] private UIInGame _view;

        private ParticleController _particleSystem;
        private float _delay;

        private void Start()
        {
            _particleSystem = GetComponentInParent<PlayerActor>().Particles;;
        }
        /// <summary>
        /// Вызов эффекта у игрока
        /// </summary>
        /// <param name="powerUp"></param>
        public void Execute(IPowerUp powerUp)
        {
            powerUp.Execute(this);
            _delay = powerUp.Duration;

            if (powerUp is Magnet)
            {              
                StartCoroutine(_particleSystem.MagnetEffect(_delay));
            }
            else if(powerUp is Multiply)
            {
                StartCoroutine(_particleSystem.MultiplyEffect(_delay));
            }          
        }
    }
}

