using DG.Tweening;
using System;
using UnityEngine;

public class TractorController : MonoBehaviour, IDynamicObstacle
{
    [SerializeField] private float _speedMultilpy;
    [SerializeField] private float _speed;
    [SerializeField] private float _time;
    private Vector3 _startPos;

    private void OnEnable()
    {
        _startPos = transform.localPosition;
    }
    public void Move()
    {
        transform.DOMoveZ(_speed * _speedMultilpy, _time).SetEase(Ease.Linear);
    }

    public void Init(float speed,float mult)
    {
        _speedMultilpy = speed * mult;
    }

    private void OnDisable()
    {
        transform.localPosition = _startPos;
    }
}
