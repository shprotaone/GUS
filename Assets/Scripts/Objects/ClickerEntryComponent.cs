using GUS.Player;
using GUS.Player.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerEntryComponent : MonoBehaviour
{
    [SerializeField] private bool _isActive;
    [SerializeField] private UIClicker _clicker;

    private ClickerMovement _clickerMovement;
    public bool ActivatePit => _isActive;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerActor actor) && _isActive)
        {
            _isActive = false;
            actor.ChangeGameType(false);
            StartCoroutine(Initialization(actor));            
        }
    }

    private IEnumerator Initialization(PlayerActor actor)
    {
        yield return StartCoroutine(_clicker.Init(actor));
    }

}
