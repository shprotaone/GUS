using GUS.Core.Pool;
using GUS.Player;
using System.Collections;
using UnityEngine;


public class CornMulti : MonoBehaviour,IPoolObject
{
    [SerializeField] private GameObject _model;
    [SerializeField] private Collider _collider;
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private int _value;
    public float Duration => 0;

    public PoolObjectType Type => PoolObjectType.CoinCob;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerActor actor))
        {
            StartCoroutine(Delay(actor));
        }
    }

    private void OnEnable()
    {
        gameObject.SetActive(true);
    }

    private IEnumerator Delay(PlayerActor actor)
    {
        _model.SetActive(false);
        _collider.enabled = false;
        actor.CollectBonus();
        _particles.Play();
        actor.Wallet.AddCoins(_value);

        yield return new WaitForSeconds(0.5f);
        _model.SetActive(true);
        _collider.enabled=true;
        gameObject.SetActive(false);
    }
}
