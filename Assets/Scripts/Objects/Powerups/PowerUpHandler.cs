using GUS.Core.UI;
using GUS.Player;
using System.Collections.Generic;
using UnityEngine;

namespace GUS.Objects.PowerUps
{
    public class PowerUpHandler : MonoBehaviour
    {

        [SerializeField] private UIInGame _view;
        [SerializeField] private Transform _powerUpParent;
        [SerializeField] private PlayerActor _playerActor;

        private ParticleSystem _particle;
        public Transform PowerUpParent => _powerUpParent;
        private void Start()
        {

        }
        public void Execute(IPowerUp powerUp)
        {
            //index = _view.SetBonusImage(powerUp.Sprite);
            _particle = powerUp.Particle;
            _particle.transform.position = this.transform.position;
            _particle.Play();
            powerUp.Execute(this);            
        }

        public void Disable()
        {
            _particle.Stop();
            //_view.DisableBonusImage();   
        }
    }
}

