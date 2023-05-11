using GUS.Core.UI;
using GUS.Player;

using UnityEngine;

namespace GUS.Objects.PowerUps
{
    public class PowerUpHandler : MonoBehaviour
    {
        [SerializeField] private UIInGame _view;
        [SerializeField] private Transform _powerUpParent;
        [SerializeField] private PlayerActor _playerActor;

        private IPowerUp _currentPowerUp;
        public Transform PowerUpParent => _powerUpParent;
        public void Execute(IPowerUp powerUp)
        {
            //TODO:
            //укзание UI и обноление слайдера или еще чего, указывающего на прогресс
            if(_currentPowerUp == null)
            {
                _currentPowerUp = powerUp;
                _currentPowerUp.Execute(this);
                _view.SetBonusImage(powerUp.Sprite);
            }
        }

        public void Disable()
        {
            _view.DisableBonusImage();
            _currentPowerUp = null;           
        }
    }
}

