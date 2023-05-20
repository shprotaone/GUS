using Cinemachine;
using GUS.Core.GameState;
using UnityEngine;

public class CameraHubController : MonoBehaviour,ICamera
{
    [SerializeField] private CinemachineFreeLook _hubWalkCamera;
    [SerializeField] private CinemachineVirtualCamera _hubMapCamera;

    public void ExploreCamera()
    {
        _hubWalkCamera.enabled = true;
        _hubMapCamera.enabled = false;
    }

    public void MapCamera()
    {
        _hubMapCamera.enabled = true;
        _hubWalkCamera.enabled = false;
    }
}
