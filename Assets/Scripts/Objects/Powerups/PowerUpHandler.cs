using Cysharp.Threading.Tasks;
using GUS.Core.UI;
using GUS.Player;
using System;
using System.Collections.Generic;
using System.Threading;
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
        private CancellationTokenSource _cancellationTokenSource;
        private List<IPowerUp> _powerUps= new List<IPowerUp>();
        private float _delay;

        private void Start()
        {
            _standartMaterial = _renderer.material;        
        }

        /// <summary>
        /// ����� ������� � ������
        /// </summary>
        /// <param name="powerUp"></param>
        public void Execute(IPowerUp powerUp)
        {
            if (!FindActivePowerUp(powerUp))
            {
                _powerUps.Add(powerUp);
            }

            powerUp.Execute(this);

            _delay = powerUp.Duration;
            _view.ActivateBonusView(powerUp);

            //Cancel();
            //_cancellationTokenSource = new CancellationTokenSource();
            //_particleController.BonusEffectEnable(powerUp.PowerUpEnum, _delay);

            //if (powerUp is Multiply)
            //{                
            //    MaterialChanger(_cancellationTokenSource.Token);
            //}
            //else if (powerUp is Magnet) 
            //{

            //}
        }

        private async void MaterialChanger(CancellationToken token)
        {
            _renderer.material = _goldMaterial;            

            try
            {
                await UniTask.Delay((int)_delay * 1000,false,PlayerLoopTiming.Update,token);
                _particleController.BonusEffectEnable(PowerUpEnum.Multiply, 1);
                _renderer.material = _standartMaterial;
            }
            catch (OperationCanceledException)
            {
                _renderer.material = _standartMaterial;
            }           
        }

        private bool FindActivePowerUp(IPowerUp powerUp)
        {
            foreach (var item in _powerUps)
            {
                if (item == powerUp) return true;
            }

            return false;
        }

        public void ResetPoweraUps()
        {
            foreach (var powerup in _powerUps)
            {
                powerup.Disable();
            }

            Cancel();

            _particleController.DisablePowerUpParticle(PowerUpEnum.Magnet);
            _powerUps.Clear();
            _view.DesactivateBonuses();
        }

        private void Cancel()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }
    }
}

