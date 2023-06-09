using DG.Tweening;
using UnityEngine;

public class TractorController : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _startPos;
    private void Start()
    {
        _startPos = transform.localPosition;
    }
    public void Move()
    {
        transform.DOMoveZ(_speed, 1).SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        transform.localPosition= _startPos;
    }
}
