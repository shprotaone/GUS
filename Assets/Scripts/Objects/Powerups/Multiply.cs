using GUS.Core.Pool;
using GUS.Player;
using System.Collections;
using UnityEngine;

namespace GUS.Objects.PowerUps
{
    public class Multiply : MonoBehaviour, IPowerUp,IPoolObject
    {
        [SerializeField] private GameObject _model;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private PoolObjectType _poolType;
        [SerializeField] private Sprite _bonusSprite;
        [SerializeField] private float _duration;

        private Wallet _wallet;
        private PowerUpHandler _handler;
        private ObjectPool _objectPool;

        private bool _canTake;
        public float Duration => _duration;
        public Sprite Sprite => _bonusSprite;
        public PoolObjectType Type => _poolType;

        public ParticleSystem Particle => _particleSystem;

        private void OnEnable()
        {
            if (_objectPool == null)
            {
                _objectPool = GetComponentInParent<ObjectPool>();
                _canTake = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out PlayerActor actor)&& _canTake)
            {
                _handler = actor.PowerUpHandler;
                _wallet = actor.Wallet;
                _canTake = false;
                _handler.Execute(this);
            }
        }
        public void Execute(PowerUpHandler handler)
        {
            StopCoroutine(Activate(handler));
            StartCoroutine(Activate(handler));
        }
        private IEnumerator Activate(PowerUpHandler handler)
        {
            _model.SetActive(false);
            transform.SetParent(handler.transform);
            transform.position = handler.transform.position;
            _wallet.SetMultiply(true);

            yield return new WaitForSeconds(_duration);

            _canTake = true;
            _wallet.SetMultiply(false);
            _handler.Disable();
            _objectPool.DestroyObject(this.gameObject);
        }

    }
}

