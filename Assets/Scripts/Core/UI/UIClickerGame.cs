using Cysharp.Threading.Tasks;
using DG.Tweening;
using GUS.Core.Clicker;
using GUS.Core.Locator;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIClickerGame : MonoBehaviour
{
    [SerializeField] private GameObject _tutorialPanel;
    [SerializeField] private GameObject _clickerPanel;
    [SerializeField] private RectTransform _corn;
    [SerializeField] private Image _focus;
    [SerializeField] private CoinAnimationHandler _coinAnimationHandler;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _scaler;

    private Vector3 _startPosCorn;
    private Vector3 _scaleStep;
    private ClickerGame _clickerGame;

    private Color _alphaColor;
    private float _aplhaFocus = 100;

    public void Init(IServiceLocator serviceLocator)
    {
        _coinAnimationHandler.Init(serviceLocator);
        _clickerGame = serviceLocator.Get<ClickerGame>();
    }

    public void InitSlider(float health)
    {
        _slider.gameObject.SetActive(true);
        _slider.maxValue = health;
        _slider.value = 0;
    }

    public void UpdateSlider(float value)
    {
        _slider.value += value;
    }

    public void PanelActivate(bool flag) => _clickerPanel.SetActive(flag);
    public void SliderActivate(bool flag)
    {
        _slider.gameObject.SetActive(flag);
    }

    public void FocusActivate()
    {
        _focus.gameObject.SetActive(true);
    }
    public void FocusDeactivate()
    {
        _focus.gameObject.SetActive(false);
        _focus.DOFade(0, 1);
    }

    public async UniTask EndClicker()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(DOVirtual.DelayedCall(0, () => _coinAnimationHandler.Animate()));
        sequence.Append(DOVirtual.DelayedCall(0, () => PanelActivate(false)));
        sequence.Play();
        await UniTask.Delay(2);
        await UniTask.Yield();
    }

    public void TutorialPanel(bool flag)
    {
        Sequence sequence = DOTween.Sequence();

        if (flag)
        {
            sequence.Append(_tutorialPanel.transform.DOLocalMoveX(0, 1));
            sequence.AppendInterval(3);
            sequence.Append(_tutorialPanel.transform.DOLocalMoveX(-1200, 1));
            sequence.Append(DOVirtual.DelayedCall(0,() => _tutorialPanel.SetActive(false)));
        }

        sequence.Play();
    }

    public void ResetClickerUI()
    {
        //_corn.position = _startPosCorn;
        //_corn.localScale= Vector3.one;
        //_corn.gameObject.SetActive(true);
    }
}
