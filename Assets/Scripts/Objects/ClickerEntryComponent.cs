using Cysharp.Threading.Tasks;
using GUS.Player;
using System.Collections;
using UnityEngine;

namespace GUS.Core.Clicker
{
    public class ClickerEntryComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyObj;
        [SerializeField] private BossSettings _settings;
        [SerializeField] private Transform _bossTransform;
        [SerializeField] private float _prepareTime;
        [SerializeField] private bool _isActive;

        private ClickerGame _clicker;

        private void OnEnable()
        {                
            _enemyObj.transform.SetParent(_bossTransform);           
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerActor actor) && _isActive)
            {
                _isActive = false;
                PrepareEnemy();
                actor.MovementType.CanMove(false);
                actor.RestartPosition();
                Initialization(actor);
                Debug.Log("Начинается кликер");
            }
        }

        private void Initialization(PlayerActor actor)
        {
            Debug.Log("Инициализатор Entry");
            _clicker = actor.ServiceLocator.Get<ClickerGame>();
            _enemyObj.SetActive(true);
            _clicker.OnRestart += PrepareEnemy;
            StartCoroutine(_clicker.Init(_settings,_enemyObj));          
        }

        public void PrepareEnemy()
        {
            _enemyObj.SetActive(false);
        }

        private void OnDisable()
        {
            _isActive = true;
        }

    }
}

