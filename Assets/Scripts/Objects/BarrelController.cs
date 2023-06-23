using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour,IDynamicObstacle
{
    [SerializeField] private float _speedMultilpy;
    [SerializeField] private float _speed;
    [SerializeField] private float _time;
    private Vector3 _startPos;

    private void OnEnable()
    {
        _startPos = transform.localPosition;
    }

    public void Init(float speed, float mult)
    {
        _speedMultilpy = speed * mult;
        Debug.Log("BarrelSpeed " + _speed * _speedMultilpy);
    }

    public void Move()
    {
        transform.DOMoveZ(_speed * _speedMultilpy, _time).SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        transform.localPosition = _startPos;
    }
}
