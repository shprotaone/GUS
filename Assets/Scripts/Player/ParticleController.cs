using DG.Tweening;
using System;
using UnityEngine;

namespace GUS.Player
{
    public class ParticleController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _afterDeath;
        [SerializeField] private ParticleSystem _deathParticle;

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
            }
            else
            {
                _afterDeath.Stop();
                _afterDeath.gameObject.SetActive(false);
            }
            
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
    }
}