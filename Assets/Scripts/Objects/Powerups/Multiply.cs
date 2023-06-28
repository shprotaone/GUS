using Cysharp.Threading.Tasks;
using GUS.Core.Data;
using GUS.Core.Pool;
using GUS.Player;
using System;
using System.Collections;
using System.Threading;
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
        private CancellationTokenSource _cancellationToken;
        private bool _canTake;
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
            _cancellationToken?.Cancel();
            _cancellationToken?.Dispose();

            _cancellationToken= new CancellationTokenSource();
            Activate();
        }

        private async void Activate()
        {
            float dur = _duration;
            _model.SetActive(false);
            _wallet.SetMultiply(true);

            try
            {
                await UniTask.Delay((int)dur * 1000,false, PlayerLoopTiming.Update, _cancellationToken.Token);
                _wallet.SetMultiply(false);
            }
            catch (OperationCanceledException)
            {

            }


            Disable();            
        }

        public void SetUp(float duration)
        {
            _duration = duration;
        }

        public void Disable()
        {
            Debug.Log("MultiDisable");
            _canTake = true;

            if (_destroyable) Destroy(gameObject);
            else _objectPool.DestroyObject(this.gameObject);
        }
    }
}

