using DG.Tweening;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private bool _isVertical;
    [SerializeField] private float _rotateSpeed;


    private void OnEnable()
    {
        if (!_isVertical) this.gameObject.transform.DOLocalRotate(Vector3.up * _rotateSpeed, 1).SetLoops(-1,LoopType.Incremental).SetEase(Ease.Linear);
        else this.gameObject.transform.DOLocalRotate(Vector3.forward * _rotateSpeed, 1).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        DOTween.Rewind(transform);
        DOTween.Kill(transform);
    }
}
