using Cysharp.Threading.Tasks;
using GUS.SceneManagment;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public event Action OnLoaded;
    [SerializeField] private Fader _fader;
    [SerializeField] private AssetReference _hubSceneRef;

    private AsyncOperationHandle<SceneInstance> _handlePrevScene;
    private AsyncOperationHandle<SceneInstance> _handle;
    private void Start()
    {
        _handle = Addressables.LoadSceneAsync(_hubSceneRef,LoadSceneMode.Additive,true);
        Application.targetFrameRate = 75;
    }

    public async void ChangeAdditiveScene(AssetReference reference)
    {
        await DelayRoutine(reference);       
    }

    private async UniTask DelayRoutine(AssetReference reference)
    {      
        await PrevSceneUnload();
        
        _handle = Addressables.LoadSceneAsync(reference, LoadSceneMode.Additive);
        if (!_handle.IsDone)
            await _handle;

        await ChangeScene(_handle);

    }
    private async UniTask ChangeScene(AsyncOperationHandle<SceneInstance> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log(obj.Result.Scene.name);

        }
        else
        {
            Debug.LogError("����� �� ������");
        }

        if (obj.IsDone)
        {
            SceneManager.SetActiveScene(obj.Result.Scene);
            OnLoaded?.Invoke();
        }
        await UniTask.Yield();
    }
    private async UniTask PrevSceneUnload()
    {
        Addressables.UnloadSceneAsync(_handle).Completed += op =>
        {
            if(op.Status == AsyncOperationStatus.Succeeded)
            {               
                Debug.Log("UnloadComplete");
            }
        };

        await UniTask.Yield();
    }
}
