using UnityEngine;

[CreateAssetMenu]
public class LightSettingsSO : ScriptableObject
{
    [SerializeField] private Material _skybox;
    [SerializeField] private float _lightIntencity;
    [SerializeField] private Color _lightColor;
    [SerializeField] private Color _fogColor;
    [SerializeField] private Color _ambientColor;
    
    public Material Skybox { get { return _skybox; } }
    public float LightIntencity { get { return _lightIntencity;} }
    public Color LightColor { get { return _lightColor; } }
    public Color FogColor { get { return _fogColor; } }
    public Color AmbientColor { get { return _ambientColor; } }
}
