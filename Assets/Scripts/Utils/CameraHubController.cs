using Cinemachine;
using Cysharp.Threading.Tasks;
using GUS.Core.GameState;
using System.Threading.Tasks;
using UnityEngine;

public class CameraHubController : MonoBehaviour,ICamera
{
    [SerializeField] private CinemachineFreeLook _hubWalkCamera;
    [SerializeField] private CinemachineVirtualCamera _idleCamera;
    [SerializeField] private CinemachineVirtualCamera _mapCamera;

    public async UniTask ExploreCamera()
    {
        _hubWalkCamera.enabled = true;
        _mapCamera.enabled = false;
        await Task.Delay(50);
    }

    public async UniTask IdleCamera()
    {
        _idleCamera.enabled = true;
        _hubWalkCamera.enabled = false;
        await Task.Delay(50);
    }

    public void MapCamera()
    {
        _mapCamera.enabled = true;
        _idleCamera.enabled = false;
    }
}
