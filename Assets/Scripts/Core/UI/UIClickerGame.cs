using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIClickerGame : MonoBehaviour
{
    [SerializeField] private GameObject _clickerPanel;
    [SerializeField] private Transform _corn;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _scaler;
    private Vector3 _scaleStep;

    private void Start()
    {
        _scaleStep = Vector3.one / _scaler;
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
        UpscaleCor();
    }

    public void PanelActivate(bool flag)
    {
        _clickerPanel.SetActive(flag);
        _slider.gameObject.SetActive(flag);
    }

    private void UpscaleCor()
    {
        _corn.DOScale(_corn.localScale += _scaleStep, 0.3f).SetEase(Ease.OutElastic);
    }
}
