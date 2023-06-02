using GUS.Objects.Enemies;
using UnityEngine;

public class BagController : MonoBehaviour, IClickerProgress
{
    [SerializeField] private GameObject _full;
    [SerializeField] private GameObject _first;
    [SerializeField] private GameObject _second;
    [SerializeField] private GameObject _last;
    [SerializeField] private ParticleSystem _hit;

    private GameObject _current;
    private void OnEnable()
    {
        ResetState();
        _full.SetActive(true);
        _current = _full;
    }
    public void Behaviour(EnemyStage stage)
    {
        _hit.Play();
        if (stage == EnemyStage.FULL) { }
        else if (stage == EnemyStage.FIRST) Change(_first);
        else if (stage == EnemyStage.SECOND) Change(_second);
        else if (stage == EnemyStage.LAST) Change(_last);
    }

    private void Change(GameObject next)
    {
        _current?.SetActive(false);
        _current = next;
        _current.SetActive(true);
    }

    private void ResetState()
    {
        _current?.SetActive(false);
    }
}
