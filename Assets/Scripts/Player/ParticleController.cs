using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

namespace GUS.Player
{
    public class ParticleController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _afterDeath;
        [SerializeField] private ParticleSystem _deathParticle;
        [SerializeField] private ParticleSystem _slideEffect;
        [SerializeField] private ParticleSystem _magnet;
        [SerializeField] private ParticleSystem _multiply;

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }
        public void DeathEffect(bool flag)
        {
            if(flag)
            {
                _deathParticle.gameObject.SetActive(true);
                _deathParticle.Play();
                _magnet.Stop();
            }           
        }

        public void SlideEffect()
        {
            _slideEffect.Play();
        }

        public void BonusEffectEnable(PowerUpEnum powerUp,float delay)
        {
            if(powerUp == PowerUpEnum.Magnet)
            {
                ParticleRoutine(delay,_magnet);
            }
            else if(powerUp == PowerUpEnum.Multiply)
            {
                ParticleRoutine(delay, _deathParticle);
            }         
        }

        private async void ParticleRoutine(float delay,ParticleSystem particles)
        {
            particles.gameObject.SetActive(true);
            particles.Play();
            await UniTask.Delay((int)delay * 1000,false,PlayerLoopTiming.Update);

            particles.Stop();
            particles.gameObject.SetActive(false);
        }
        public void DamageEffect(Vector3 position)
        {
            //Ray ray = _camera.ScreenPointToRay(position);
            //RaycastHit hitInfo;

            //if(Physics.Raycast(ray,out hitInfo))
            //{
            //    _damageParticle.gameObject.transform.position = hitInfo.point;
            //    Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1000);
            //}
            //_biteParticle.Play();
        }

        public void AfterDeath()
        {
            _afterDeath.gameObject.SetActive(true);
            _afterDeath.Play();           
        }

        public void AfterDeathDisabler()
        {
            _afterDeath.gameObject.SetActive(false);
        }

        public void DisablePowerUpParticle(PowerUpEnum powerUp)
        {           
            if (powerUp == PowerUpEnum.Magnet) _magnet.Stop();
            _magnet.gameObject.SetActive(false);
        }
    }
}