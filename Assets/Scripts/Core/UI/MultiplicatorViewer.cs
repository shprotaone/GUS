using DG.Tweening;
using GUS.Core.Locator;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class MultiplicatorViewer : MonoBehaviour
{
    [SerializeField] private Image _multiplyImage;
    [SerializeField] private bool _withAnimation = false;
    [SerializeField] private float _animTime;

    [Title("Ресурсы для мультипликатора")]
    [SerializeField] private Sprite[] _sprites;

    private DistanceMutiplier _distanceMutiplier;
    private Sequence _sequence;
    public void Init(IServiceLocator serviceLocator)
    {
        _distanceMutiplier = serviceLocator.Get<DistanceMutiplier>();
        _distanceMutiplier.OnMultiplyChanged += SetMultiplyImage;
    }

    public void SetMultiplyImage(int val)
    {
        if(_withAnimation)
        {
            _multiplyImage.transform.localScale = Vector3.zero;
            _multiplyImage.sprite = _sprites[val];
            _multiplyImage.transform.DOScale(1, _animTime).SetEase(Ease.OutBack);
                                          
        }
        else
        {
            _multiplyImage.sprite = _sprites[val];
        }        
    }

    public void Display(bool flag) => _multiplyImage.enabled = flag;

    private void OnDestroy()
    {
        _distanceMutiplier.OnMultiplyChanged -= SetMultiplyImage;
    }
}
