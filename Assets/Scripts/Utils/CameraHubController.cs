using Cinemachine;
using GUS.Core.GameState;
using UnityEngine;

public class CameraHubController : MonoBehaviour,ICamera
{
    [SerializeField] private CinemachineFreeLook _hubWalkCamera;
    [SerializeField] private CinemachineVirtualCamera _idleCamera;
    [SerializeField] private CinemachineVirtualCamera _mapCamera;

    public void ExploreCamera()
    {
        _hubWalkCamera.enabled = true;
        _mapCamera.enabled = false;
    }

    public void IdleCamera()
    {
        _idleCamera.enabled = true;
        _hubWalkCamera.enabled = false;
    }

    public void MapCamera()
    {
        _mapCamera.enabled = true;
        _idleCamera.enabled = false;
    }
}
