using GUS.Core.Pool;
using GUS.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HonkCoin : MonoBehaviour, IPoolObject
{
    [SerializeField] private Collider _collider;
    [SerializeField] private GameObject _model;
    [SerializeField] private ParticleSystem _getParticle;

    private ObjectPool _pool;
    private Vector3 _startPos;

    public PoolObjectType Type => PoolObjectType.HonkCoin;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerActor actor))
        {
            StartCoroutine(Delay(actor));
        }
    }

    private void OnEnable()
    {
        _model.SetActive(true);

        if(_pool == null)
        {
            _pool = GetComponentInParent<ObjectPool>();
        }
    }

    private IEnumerator Delay(PlayerActor actor)
    {
        _model.SetActive(false);
        _collider.enabled = false;
        //_getParticle?.Play();
        actor.CollectHonkCoin();

        yield return new WaitForSeconds(0.5f);
        _model.SetActive(true);
        _collider.enabled = true;
        _pool.DestroyObject(gameObject);
    }

    private void OnDisable()
    {
        transform.localPosition = _startPos;
    }
}
