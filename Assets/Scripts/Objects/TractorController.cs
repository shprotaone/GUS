using DG.Tweening;
using UnityEngine;

public class TractorController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _time;
    private Vector3 _startPos;
    private void Start()
    {
        _startPos = transform.localPosition;
    }
    public void Move()
    {
        transform.DOMoveZ(_speed, _time).SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        transform.localPosition= _startPos;
    }
}
