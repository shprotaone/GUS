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
        _full.SetActive(true);
        _current = _full;
    }
    public void Behaviour(EnemyStage stage)
    {
        _hit.Play();
        if (stage == EnemyStage.FULL) Change(_first);
        else if (stage == EnemyStage.FIRST) Change(_second);
        else if (stage == EnemyStage.SECOND) Change(_last);
        else if (stage == EnemyStage.LAST) _last.SetActive(false);
    }

    private void Change(GameObject next)
    {
        _current?.SetActive(false);
        _current = next;
        _current.SetActive(true);
    }
}
