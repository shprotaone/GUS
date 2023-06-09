using DG.Tweening;
using GUS.Core.Pool;
using UnityEngine;

public class CoinAnim : MonoBehaviour, IPoolObject
{
    [SerializeField] private RectTransform _rect;
    private Vector3 _startPos;
    public PoolObjectType Type => PoolObjectType.CoinUI;

    private void Start()
    {
        _startPos = _rect.position;
    }
    public void Movement(Vector2 jarPosition)
    {
        gameObject.SetActive(true);
        _rect.DOMove(jarPosition, 1).SetEase(Ease.InOutBack).OnComplete(DisableCoin);
    }

    private void DisableCoin()
    {
        gameObject.SetActive(false);
        _rect.position= _startPos;
    }
}
