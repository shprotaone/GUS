using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    [SerializeField] private Light _mainLight;
    [SerializeField] private Light _secondaryLight;
    [SerializeField] private SceneHandler _handler;
    [SerializeField] private LightSettingsSO[] _settings;

    private LightSettingsSO _currentSettings;
    private void Start()
    {
        if(_handler.SceneLoader != null)
        {
            _handler.SceneLoader.OnLoaded += ChangeSkybox;
        }
        else ChangeSkybox();
        
    }

    public void ChangeSkybox()
    {
        ChooseSettings();

        _mainLight.intensity = _currentSettings.LightIntencity;
        _mainLight.color= _currentSettings.LightColor;

        RenderSettings.skybox= _currentSettings.Skybox;
        RenderSettings.fogColor= _currentSettings.FogColor;
        RenderSettings.ambientSkyColor = _currentSettings.AmbientColor;
        
    }

    private void ChooseSettings()
    {
        int index = Random.Range(0, _settings.Length);
        _currentSettings = _settings[index];

        if(_currentSettings.WithFlash) _secondaryLight.gameObject.SetActive(true);
        else _mainLight.gameObject.SetActive(true);
    }

    private void ResetSettings()
    {
        RenderSettings.skybox = _settings[0].Skybox;
        RenderSettings.fogColor = _settings[0].FogColor;
        RenderSettings.ambientSkyColor = _settings[0].AmbientColor;
    }

    private void OnDisable()
    {
        //ResetSettings();
        if (_handler.SceneLoader != null)
        {
            _handler.SceneLoader.OnLoaded -= ChangeSkybox;
        }
    }
}
