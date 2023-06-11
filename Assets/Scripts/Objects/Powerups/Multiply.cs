using GUS.Core.Data;
using GUS.Core.Pool;
using GUS.Player;
using System.Collections;
using UnityEngine;

namespace GUS.Objects.PowerUps
{
    public class Multiply : MonoBehaviour, IPowerUp,IPoolObject
    {
        [SerializeField] private Collectable _collectable;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Sprite _bonusSprite;
        [SerializeField] private float _duration;

        private GameObject _model;
        private Wallet _wallet;
        private ObjectPool _objectPool;

        private bool _canTake;
        private float _workTime;
        public float Duration => _duration;
        public Sprite Sprite => _bonusSprite;
        public PoolObjectType Type => PoolObjectType.Multiply;

        private void OnEnable()
        {
            if (_model == null) _model = Instantiate(_collectable.model,this.transform);

            _model.SetActive(true);
            _canTake = true;

            if (_objectPool == null)
            {
                _objectPool = GetComponentInParent<ObjectPool>();                            
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out PlayerActor actor)&& _canTake)
            {
                _wallet = actor.Wallet;
                transform.SetParent(actor.transform);
                actor.PowerUpHandler.Execute(this);
                _canTake = false;
            }
        }

        public void Execute(PowerUpHandler handler)
        {
            StopCoroutine(Activate());
            StartCoroutine(Activate());
        }

        private IEnumerator Activate()
        {
            Debug.Log("Multi " + _duration);
            _model.SetActive(false);
            _wallet.SetMultiply(true);

            yield return new WaitForSeconds(_duration);

            _canTake = true;
            _wallet.SetMultiply(false);
            _objectPool.DestroyObject(this.gameObject);
            
        }

        public void SetUp(float duration)
        {
            _duration = duration;
        }
    }
}

