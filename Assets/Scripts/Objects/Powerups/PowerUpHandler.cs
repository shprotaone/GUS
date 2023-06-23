using Cysharp.Threading.Tasks;
using GUS.Core.UI;
using GUS.Player;
using UnityEngine;

namespace GUS.Objects.PowerUps
{
    public class PowerUpHandler : MonoBehaviour
    {
        [SerializeField] private UIInGame _view;
        [SerializeField] private ParticleController _particleController;
        [SerializeField] private SkinnedMeshRenderer _renderer;
        [SerializeField] private Material _goldMaterial;

        private Material _standartMaterial;
        private float _delay;

        private void Start()
        {
            _standartMaterial = _renderer.material;
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
                StartCoroutine(_particleController.MagnetEffect(_delay));
            }
            else if(powerUp is Multiply)
            {
                StartCoroutine(_particleController.MultiplyEffect(1));
                MaterialChanger();
            }          
        }

        private async void MaterialChanger()
        {
            _renderer.material = _goldMaterial;
            await UniTask.Delay((int)_delay * 1000);
            StartCoroutine(_particleController.MultiplyEffect(1));
            _renderer.material = _standartMaterial;
        }
    }
}

