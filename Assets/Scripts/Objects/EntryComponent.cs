using GUS.Player;
using UnityEngine;

public class EntryComponent : MonoBehaviour
{
    [SerializeField] private TractorController _tractor;
    [SerializeField] private bool _isActive;
    private void OnEnable()
    {
        _isActive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out PlayerActor actor) && _isActive)
        {
            _tractor.Move();
            _isActive = false;
        }
    }
}
