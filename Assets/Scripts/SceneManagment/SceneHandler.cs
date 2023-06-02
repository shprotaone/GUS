using GUS.SceneManagment;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] private AssetReference _runSeneRef;
    [SerializeField] private Fader _fader;
    private SceneLoader _sceneLoader;

    private void Awake()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _fader= FindObjectOfType<Fader>();
    }

    public void LoadOtherScene()
    {
        _sceneLoader.ChangeAdditiveScene(_runSeneRef);
    }

    public void FadeInHandle()
    {
        if(_fader != null)
        {
            _fader?.FadeIn();
        }
    }
    public void FadeOutHandle()
    {
        if(_fader != null) StartCoroutine(_fader.FadeOut());
    }
    

}
