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
        private Dictionary<PowerUpEnum,CancellationTokenSource> _powerUps = new Dictionary<PowerUpEnum, CancellationTokenSource>();

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
            PowerUpEnum type = powerUp.PowerUpEnum;
            if (!FindActivePowerUp(powerUp))
            {
                _powerUps.Add(type, new CancellationTokenSource());
            }
            else
            {
                Cancel(_powerUps[type]);
                _powerUps.Remove(type);
                _powerUps.Add(type, new CancellationTokenSource());
            }

            powerUp.Execute(this);
            _view.ActivateBonusView(powerUp);
            _particleController.BonusEffectEnable(powerUp.PowerUpEnum, powerUp.Duration);

            CancellationToken token = _powerUps[type].Token;

            if (powerUp is Multiply)
            {               
                MaterialChanger(powerUp,token);
            }
            else if(powerUp is Magnet) 
            {
                Magnet(powerUp, token);
            }
        }

        private async void MaterialChanger(IPowerUp powerUp, CancellationToken token)
        {
            _renderer.material = _goldMaterial;

            try
            {
                await UniTask.Delay((int)powerUp.Duration * 1000,false,PlayerLoopTiming.FixedUpdate,token);
                _particleController.BonusEffectEnable(PowerUpEnum.Multiply, 1);
                _view.DisableBonusView(powerUp);
                _powerUps.Remove(powerUp.PowerUpEnum);
                _renderer.material = _standartMaterial;
            }
            catch (OperationCanceledException)
            {
                
            }           
        }

        private async void Magnet(IPowerUp powerUp, CancellationToken token)
        {
            try
            {
                await UniTask.Delay((int)powerUp.Duration * 1000, false, PlayerLoopTiming.FixedUpdate, token);
                _powerUps.Remove(powerUp.PowerUpEnum);
                _view.DisableBonusView(powerUp);
            }
            catch (OperationCanceledException)
            {

            }
        }

        private bool FindActivePowerUp(IPowerUp powerUp)
        {
            if (_powerUps.ContainsKey(powerUp.PowerUpEnum)) return true;
            else return false;
        }

        public void ResetPoweraUps()
        {
            foreach(var powerUp in _powerUps)
            {
                Cancel(powerUp.Value);
                //powerUp.Key.Disable();
            }

            _renderer.material = _standartMaterial;
            _particleController.DisablePowerUpParticle(PowerUpEnum.Magnet);
            _powerUps.Clear();
            _view.DesactivateBonuses();
        }

        private void Cancel(CancellationTokenSource source)
        {
            source?.Cancel();
            source?.Dispose();
        }
    }
}

