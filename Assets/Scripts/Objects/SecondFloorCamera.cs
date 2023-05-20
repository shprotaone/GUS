using GUS.Core.GameState;
using GUS.Player;
using UnityEngine;

public class SecondFloorCamera : MonoBehaviour
{
    private CameraController _camera;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerActor actor)){
            _camera = actor.CameraController;
            _camera.SecondFloor(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out PlayerActor actor))
        {
            _camera.SecondFloor(false);
        }
    }
}
