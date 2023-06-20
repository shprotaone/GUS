using Cysharp.Threading.Tasks;
using DG.Tweening;
using GUS.Objects.Enemies;
using Unity.VisualScripting;
using UnityEngine;

public class BagController : MonoBehaviour, IClickerProgress
{
    [SerializeField] private GameObject _full;
    [SerializeField] private GameObject _first;
    [SerializeField] private GameObject _second;
    [SerializeField] private GameObject _last;
    [SerializeField] private ParticleSystem _hit;
    [SerializeField] private ParticleSystem _sticker;
    [SerializeField] private string _emissionName;

    private Material _currentMaterial;
    private GameObject _current;
    private void OnEnable()
    {
        ResetState();
        _full.SetActive(true);
        _current = _full;
        _currentMaterial = _current.GetComponentInChildren<MeshRenderer>().material;
    }
    public void Behaviour(EnemyStage stage)
    {
        _hit.Play();
        _sticker.Play();
        Flashing();
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
        _currentMaterial = _current.GetComponentInChildren<MeshRenderer>().material;
    }

    private void ResetState()
    {
        _current?.SetActive(false);
    }

    private async void Flashing()
    {
        _currentMaterial.SetColor(_emissionName, Color.white);
        await UniTask.Delay(100);
        _currentMaterial.SetColor(_emissionName, Color.black);
    }
}
