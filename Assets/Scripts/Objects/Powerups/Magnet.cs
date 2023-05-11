using GUS.Core.Pool;
using GUS.Objects.PowerUps;
using GUS.Player;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Magnet : MonoBehaviour,IPowerUp, IPoolObject
{
    private const float stadartColliderRadius = 0.5f;
    [SerializeField] private PoolObjectType _poolObjectType;
    [SerializeField] private SphereCollider _collider;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private float _moveTime;
    [SerializeField] private float _magnetRadius;
    [SerializeField] private float _duration;

    private PowerUpHandler _handler;
    private ObjectPool _objectPool;
    public bool IsActive { get; private set; }
    public float Duration => _duration;
    public float MoveTime => _moveTime;
    public Sprite Sprite => _sprite;
    public PoolObjectType Type => _poolObjectType;

    private void OnEnable()
    {
        if(_objectPool == null)
        {
            _objectPool = GetComponentInParent<ObjectPool>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerActor actor))
        {
            _handler = actor.PowerUpHandler;
            _handler.Execute(this);
        }
    }
    public void Execute(PowerUpHandler handler)
    {
        StartCoroutine(Activate(handler));
    }

    private IEnumerator Activate(PowerUpHandler handler)
    {
        transform.SetParent(handler.PowerUpParent);
        transform.position = handler.PowerUpParent.transform.position;

        _collider.radius = _magnetRadius;
        _collider.enabled = true;
              
        IsActive = true;

        yield return new WaitForSeconds(_duration);

        _collider.radius = stadartColliderRadius;
        _handler.Disable();
        _objectPool.DestroyObject(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, _magnetRadius);
    }
}
