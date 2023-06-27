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
        [SerializeField] private bool _destroyable;
        [SerializeField] private float _duration;

        private GameObject _model;
        private Wallet _wallet;
        private ObjectPool _objectPool;

        private bool _canTake;
        private float _workTime;
        public float Duration => _duration;
        public Sprite Sprite => _collectable.icon;
        public PoolObjectType Type => PoolObjectType.Multiply;
        public PowerUpEnum PowerUpEnum => PowerUpEnum.Multiply;

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
                actor.CollectBonus();
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

            Disable();            
        }

        public void SetUp(float duration)
        {
            _duration = duration;
        }

        public void Disable()
        {
            StopAllCoroutines();
            _canTake = true;
            _wallet.SetMultiply(false);

            if (_destroyable) Destroy(gameObject);
            else _objectPool.DestroyObject(this.gameObject);


        }
    }
}

