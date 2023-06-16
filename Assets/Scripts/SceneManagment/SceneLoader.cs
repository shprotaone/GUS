using GUS.SceneManagment;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Fader _fader;
    [SerializeField] private AssetReference _hubSceneRef;

    private AsyncOperationHandle<SceneInstance> _handlePrevScene;
    private AsyncOperationHandle<SceneInstance> _handle;
    private void Start()
    {
        _handle = Addressables.LoadSceneAsync(_hubSceneRef,LoadSceneMode.Additive,true);
        Application.targetFrameRate = 75;
    }

    public void ChangeAdditiveScene(AssetReference reference)
    {
        StartCoroutine(DelayRoutine(reference));       
    }

    private IEnumerator DelayRoutine(AssetReference reference)
    {      
        yield return _fader.FadeOut();
        yield return PrevSceneUnload();
        
        _handle = Addressables.LoadSceneAsync(reference, LoadSceneMode.Additive);
        if (!_handle.IsDone)
            yield return _handle;

        yield return ChangeScene(_handle);

    }
    private IEnumerator ChangeScene(AsyncOperationHandle<SceneInstance> obj)
    {       
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {            
            Debug.Log(obj.Result.Scene.name);    
            
        }
        else
        {
            Debug.LogError("Ассет не найден");
        }

        if(obj.IsDone) { 
            
            _fader.FadeIn();
            SceneManager.SetActiveScene(obj.Result.Scene);
        }
        yield return null;   //плохое решение
    }

    private IEnumerator PrevSceneUnload()
    {
        Addressables.UnloadSceneAsync(_handle).Completed += op =>
        {
            if(op.Status == AsyncOperationStatus.Succeeded)
            {               
                Debug.Log("UnloadComplete");
            }
        };

        yield return null;
    }
}
