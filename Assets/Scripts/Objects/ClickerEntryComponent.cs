using GUS.Player;
using GUS.Player.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerEntryComponent : MonoBehaviour
{
    [SerializeField] private bool _isActive;
    [SerializeField] private ClickerGame _clicker;

    private ClickerMovement _clickerMovement;
    public bool ActivatePit => _isActive;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerActor actor) && _isActive)
        {
            _isActive = false;                             
            StartCoroutine(Initialization(actor));
        }
    }

    private IEnumerator Initialization(PlayerActor actor)
    {
        yield return new WaitForSeconds(0.5f);
        actor.ChangeGameType(false);
        yield return StartCoroutine(_clicker.Init(actor));
    }

    private void OnDisable()
    {
        _isActive = true;
        Debug.Log("Activate Again");
    }

}
