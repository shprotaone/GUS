using UnityEngine;
using UnityEngine.UI;

public class UIClickerGame : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public void InitSlider(float health)
    {
        _slider.gameObject.SetActive(true);
        _slider.maxValue = health;
        _slider.value = health;
    }

    public void UpdateSlider(float value)
    {
        _slider.value -= value;
    }

    public void DisableSlider()
    {
        _slider.gameObject.SetActive(false);
    }
}
