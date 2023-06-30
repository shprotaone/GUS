using Cysharp.Threading.Tasks;
using GUS.SceneManagment;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;


/// <summary>
/// Загрузчик сцен с затемнением
/// </summary>
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
    /// <summary>
    /// Загрузка со всех сцен
    /// </summary>
    public void LoadOtherScene()
    {
        _sceneLoader.ChangeAdditiveScene(_runSeneRef);
    }

    /// <summary>
    /// Появление на всех сценах
    /// </summary>
    public async UniTask FadeInHandle()
    {
        if (_fader != null)
        {
            await _fader.FadeIn();
        }
    }

    /// <summary>
    /// Затемнение на всех сценах 
    /// </summary>
    public async UniTask FadeOutHandle()
    {
        if (_fader != null) await _fader.FadeOut();
    }
}
