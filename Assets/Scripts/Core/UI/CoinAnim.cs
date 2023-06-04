using DG.Tweening;
using GUS.Core.Pool;
using UnityEngine;

public class CoinAnim : MonoBehaviour, IPoolObject
{
    [SerializeField] private RectTransform _rect;
    public PoolObjectType Type => PoolObjectType.CoinUI;

    public void Movement(Vector2 jarPosition)
    {
        _rect.DOMove(jarPosition, 1).SetEase(Ease.InOutBack).OnComplete(DestroyCoin);
    }

    private void DestroyCoin()
    {
        gameObject.SetActive(false);
    }
}
