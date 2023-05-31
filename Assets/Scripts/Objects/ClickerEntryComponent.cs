using GUS.Player;
using System.Collections;
using UnityEngine;

namespace GUS.Core.Clicker
{
    public class ClickerEntryComponent : MonoBehaviour
    {
        [SerializeField] private BossSettings _settings;
        [SerializeField] private Transform _bossTransform;
        [SerializeField] private float _prepareTime;
        [SerializeField] private bool _isActive;

        private GameObject _enemyObj;
        private ClickerGame _clicker;

        public bool ActivatePit => _isActive;


        private void OnEnable()
        {
            if (_enemyObj == null)
            {
                _enemyObj = Instantiate(_settings.BossPrefab, _bossTransform);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerActor actor) && _isActive)
            {
                _isActive = false;
                PrepareEnemy();
                StartCoroutine(Initialization(actor));
            }
        }

        private IEnumerator Initialization(PlayerActor actor)
        {
            _clicker = actor.ServiceLocator.Get<ClickerGame>();
            _clicker.OnRestart += PrepareEnemy;
            yield return StartCoroutine(_clicker.Init(_settings,_enemyObj));
        }

        public void PrepareEnemy()
        {
            _enemyObj.SetActive(true);
            _enemyObj.transform.SetParent(_bossTransform);
            _enemyObj.transform.position = _bossTransform.position;
        }

        private void OnDisable()
        {
            _isActive = true;
        }

    }
}

