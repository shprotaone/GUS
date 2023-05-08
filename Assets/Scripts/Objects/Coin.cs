using GUS.Player;
using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject _model;
    [SerializeField] private ParticleSystem _getParticle;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerActor actor))
        {
            StartCoroutine(Delay(actor));
        }
    }

    private void OnEnable()
    {
        gameObject.SetActive(true);
    }

    private IEnumerator Delay(PlayerActor actor)
    {
        _model.SetActive(false);
        _getParticle.Play();
        actor.Collect();

        yield return new WaitForSeconds(0.5f);
        _model.SetActive(true);
        gameObject.SetActive(false);
    }
}
