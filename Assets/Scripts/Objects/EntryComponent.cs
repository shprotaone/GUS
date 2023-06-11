using GUS.Player;
using UnityEngine;
using System;
using System.Collections.Generic;

public class EntryComponent : MonoBehaviour
{
    [SerializeField]
    private List<TractorController> _tractor;
    [SerializeField] private bool _isActive;
    private void OnEnable()
    {
        _isActive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out PlayerActor actor) && _isActive)
        {
            foreach (var item in _tractor)
            {
                item.Move();
            }
            _isActive = false;
        }
    }
}
