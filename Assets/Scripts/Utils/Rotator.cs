using DG.Tweening;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private RotatorDirection _dir;
    [SerializeField] private float _rotateSpeedMult;
    [SerializeField] private float _duration;


    private void OnEnable()
    {
        switch (_dir)
        {
            case RotatorDirection.X:
                this.gameObject.transform.DOLocalRotate(Vector3.right * _rotateSpeedMult, _duration).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
                break;
            case RotatorDirection.Y:
                this.gameObject.transform.DOLocalRotate(Vector3.up * _rotateSpeedMult, _duration).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
                break;
            case RotatorDirection.Z:
                this.gameObject.transform.DOLocalRotate(Vector3.forward * _rotateSpeedMult, _duration).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
                break;
        }
    }

    private void OnDisable()
    {
        DOTween.Rewind(this.gameObject);
        DOTween.Kill(this.gameObject);
    }
}
