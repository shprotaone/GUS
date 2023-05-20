using DG.Tweening;
using System;
using UnityEngine;

namespace GUS.Player
{
    public class ParticleController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _afterDeath;
        [SerializeField] private ParticleSystem _deathParticle;
        [SerializeField] private ParticleSystem _damageParticle;

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }
        public void DeathEffect()
        {
            _deathParticle.gameObject.SetActive(true);
            _deathParticle.Play();
        }

        public void DamageEffect(Vector3 position)
        {
            Ray ray = _camera.ScreenPointToRay(position);
            RaycastHit hitInfo;

            if(Physics.Raycast(ray,out hitInfo))
            {
                _damageParticle.gameObject.transform.position = hitInfo.point;
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1000);
            }
            _damageParticle.Play();
        }

        public void AfterDeath()
        {
            _afterDeath.Play();           
        }
    }
}