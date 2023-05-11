using DG.Tweening;
using GUS.Player;
using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    [SerializeField] private GameObject _model;
    [SerializeField] private ParticleSystem _getParticle;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerActor actor))
        {
            StartCoroutine(Delay(actor));
        }
        else if(other.TryGetComponent(out Magnet magnet))
        {
            Debug.Log(other.gameObject.name);
            //Magnet magnet = other.GetComponentInChildren<Magnet>();
            MoveToMagnet(magnet);
        }       
    }

    private void OnEnable()
    {
        gameObject.SetActive(true);
    }

    private IEnumerator Delay(PlayerActor actor)
    {
        _model.SetActive(false);
        _collider.enabled = false;
        _getParticle.Play();
        actor.Collect();

        yield return new WaitForSeconds(0.5f);
        _model.SetActive(true);
        _collider.enabled = true;
        gameObject.SetActive(false);
    }

    private void MoveToMagnet(Magnet magnet)
    {
        transform.DOMove(magnet.transform.position, magnet.MoveTime);
    }
}
