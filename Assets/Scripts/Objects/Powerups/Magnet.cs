using GUS.Core.Pool;
using GUS.Objects;
using GUS.Objects.PowerUps;
using GUS.Player;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Magnet : MonoBehaviour,IPowerUp, IPoolObject
{
    private const float stadartColliderRadius = 0.5f;

    [SerializeField] private Collectable _collectable;
    [SerializeField] private SphereCollider _collider;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private float _moveTime;
    [SerializeField] private float _magnetRadius;
    [SerializeField] private float _duration;

    private GameObject _model;
    private ObjectPool _objectPool;
    private bool _canTake;
    private float _workTime;

    public bool IsActive { get; private set; }
    public float Duration => _duration;
    public float MoveTime => _moveTime;
    public Sprite Sprite => _sprite;
    public PoolObjectType Type => PoolObjectType.Magnet;

    private void OnEnable()
    {
        if(_model == null) _model = Instantiate(_collectable.model,this.transform);
        if(_objectPool == null)
        {
            _objectPool = GetComponentInParent<ObjectPool>();
            _model.SetActive(true);
            _canTake = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerActor actor) && _canTake)
        {
            _canTake = false;
            actor.CollectBonus();
            actor.PowerUpHandler.Execute(this);
            transform.SetParent(actor.transform);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out Coin coin))
        {
            if(Vector3.Distance(transform.position,coin.transform.position) <= 5)
            {
                coin.transform.position = transform.position;
            }
        }
    }

    public void Execute(PowerUpHandler handler)
    {
        StopCoroutine(Activate());
        StartCoroutine(Activate());
    }

    private IEnumerator Activate()
    {
        Debug.Log("Magnet " + _duration);
        _model.SetActive(false);

        _collider.radius = _magnetRadius;
        _collider.enabled = true;
              
        IsActive = true;

        yield return new WaitForSeconds(_duration);

        _collider.radius = stadartColliderRadius;
        _model.SetActive(true);
        _canTake = true;
        _objectPool.DestroyObject(this.gameObject);
    }

    public void SetUp(float duration)
    {
        _duration = duration;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, _magnetRadius);
    }
}
