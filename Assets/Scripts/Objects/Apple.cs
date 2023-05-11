using System;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    private Vector3 _startPos;
    private bool _isActive = true;

    private void OnEnable()
    {
        _startPos = gameObject.transform.position;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if(transform.position.y < - 3 && _isActive)
        {
            _isActive = false;
            Disable();
        }
    }
    public void Fall()
    {
        _rb.useGravity = true;
        _rb.isKinematic = false;
    }

    private void Disable()
    {
        gameObject.SetActive(false);
        _rb.useGravity = false;
        _rb.isKinematic = true;
        _isActive = true;
        transform.position = _startPos;
        
    }
}