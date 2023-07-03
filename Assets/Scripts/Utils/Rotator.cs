using DG.Tweening;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private RotatorDirection _dir;
    [SerializeField] private float _rotateSpeedMult;
    [SerializeField] private float _duration;

    private Tween _tween;
    private void OnEnable()
    {
        switch (_dir)
        {
            case RotatorDirection.X:
                _tween = this.gameObject.transform.DOLocalRotate(Vector3.right * _rotateSpeedMult, _duration).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
                break;
            case RotatorDirection.Y:
                _tween = this.gameObject.transform.DOLocalRotate(Vector3.up * _rotateSpeedMult, _duration).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
                break;
            case RotatorDirection.Z:
                _tween = this.gameObject.transform.DOLocalRotate(Vector3.forward * _rotateSpeedMult, _duration).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
                break;
        }
    }

    private void OnDisable()
    {
        _tween.Rewind();
        _tween.Kill();
    }
}
