using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] private AssetReference _runSeneRef;
    private SceneLoader _sceneLoader;

    private void Start()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void LoadRunScene()
    {
        _sceneLoader.ChangeAdditiveScene(_runSeneRef);
    }
}
