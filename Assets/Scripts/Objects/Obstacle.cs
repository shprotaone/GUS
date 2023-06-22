using GUS.Player;
using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameObject _model;
    [SerializeField] private bool _isActive = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerActor player) && _isActive)
        {
            player.Death();
            _isActive = false;
            StartCoroutine(Delay());
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        _model.SetActive(false);
    }
    private void OnEnable()
    {
        _isActive = true;
    }
}

