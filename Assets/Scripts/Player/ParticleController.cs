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

        public IEnumerator MagnetEffect(float delay)
        {
            float timer = delay;
            _magnet.gameObject.SetActive(true);
            _magnet.Play();
            while(timer > 0)
            {
                yield return new WaitForSeconds(1);
                timer--;
            }

            _magnet.Stop();
            _magnet.gameObject.SetActive(false);
            yield return null;
        }

        public IEnumerator MultiplyEffect(float delay)
        {
            float timer = delay;
            _deathParticle.gameObject.SetActive(true);
            _deathParticle.Play();
            while (timer > 0)
            {
                yield return new WaitForSeconds(1);
                timer--;
            }

            _deathParticle.Stop();
            _deathParticle.gameObject.SetActive(false);
            yield return null;
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
    }
}