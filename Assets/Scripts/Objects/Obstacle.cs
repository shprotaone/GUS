using GUS.Player;
using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameObject _model;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerActor player))
        {
            player.Death();
            StartCoroutine(Delay());
        }
    }

    private void OnEnable()
    {
        gameObject.SetActive(true);
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        _model.SetActive(false);
    }
}

