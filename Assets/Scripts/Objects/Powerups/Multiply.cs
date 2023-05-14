using GUS.Core.Pool;
using GUS.Objects.PowerUps;
using GUS.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUS.Objects.PowerUps
{
    public class Multiply : MonoBehaviour, IPowerUp,IPoolObject
    {
        [SerializeField] private PoolObjectType _poolType;
        [SerializeField] private Sprite _bonusSprite;
        [SerializeField] private float _duration;

        private Wallet _wallet;
        private PowerUpHandler _handler;
        private ObjectPool _objectPool;

        public float Duration => _duration;
        public Sprite Sprite => _bonusSprite;
        public PoolObjectType Type => _poolType;

        private void OnEnable()
        {
            if (_objectPool == null)
            {
                _objectPool = GetComponentInParent<ObjectPool>();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out PlayerActor actor))
            {
                _handler = actor.PowerUpHandler;
                _wallet = actor.Wallet;
                _handler.Execute(this);
            }
        }
        public void Execute(PowerUpHandler handler)
        {
            StartCoroutine(Activate(handler));
        }
        private IEnumerator Activate(PowerUpHandler handler)
        {
            transform.SetParent(handler.transform);
            transform.position = handler.transform.position;
            _wallet.SetMultiply(true);

            yield return new WaitForSeconds(_duration);

            _wallet.SetMultiply(false);
            _handler.Disable();
            _objectPool.DestroyObject(this.gameObject);
        }

    }
}

