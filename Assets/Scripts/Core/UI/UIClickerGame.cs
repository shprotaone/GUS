using DG.Tweening;
using GUS.Core.Clicker;
using GUS.Core.Locator;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIClickerGame : MonoBehaviour
{
    [SerializeField] private GameObject _clickerPanel;
    [SerializeField] private RectTransform _corn;
    [SerializeField] private CoinAnimationHandler _coinAnimationHandler;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _scaler;

    private Vector3 _scaleStep;
    private ClickerGame _clickerGame;

    public void Init(IServiceLocator serviceLocator)
    {
        _scaleStep = Vector3.one / _scaler;
        _clickerGame = serviceLocator.Get<ClickerGame>();
    }

    public void InitSlider(float health)
    {
        _slider.gameObject.SetActive(true);
        _slider.maxValue = health;
        _slider.value = health;
    }

    public void UpdateSlider(float value)
    {
        _slider.value -= value;
        UpscaleCorn();
    }

    public void PanelActivate(bool flag) => _clickerPanel.SetActive(flag);
    public void SliderActivate(bool flag) => _slider.gameObject.SetActive(flag);

    private void UpscaleCorn()
    {
        _corn.DOPunchScale(_scaleStep, 0.3f).SetEase(Ease.OutElastic);

        if (_corn.localScale.x < 2.5f)
        {
            _corn.localScale += _scaleStep;
        }        
    }

    public void EndClicker()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_corn.DOAnchorPos(new Vector3(0, -500, 0), 1));
        sequence.Append(_corn.DOScale(Vector3.one * 4,1)).SetEase(Ease.OutSine);
        sequence.Append(DOVirtual.DelayedCall(0, () =>_corn.gameObject.SetActive(false)));
        sequence.Append(DOVirtual.DelayedCall(0, () => _coinAnimationHandler.Animate()));
        sequence.Append(DOVirtual.DelayedCall(0, () => PanelActivate(false)));
        sequence.Append(DOVirtual.DelayedCall(0,() => _clickerGame.Complete()));
        sequence.Play();      
    }

    
}
