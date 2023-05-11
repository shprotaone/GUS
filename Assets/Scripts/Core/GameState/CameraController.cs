using Cinemachine;
using System;
using UnityEngine;

namespace GUS.Core.GameState
{
    public class CameraController: MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineBrain _brain;
        [SerializeField] private CinemachineFreeLook _hubWalkCamera;
        [SerializeField] private CinemachineVirtualCamera _hubMapCamera;
        [SerializeField] private CinemachineVirtualCamera _clickerCamera;
        [SerializeField] private CinemachineFreeLook _runCamera;

        public void ClickerCamera()
        {
            _clickerCamera.enabled = true;
            _runCamera.enabled = false;
        }

        public void RunCamera()
        {
            _clickerCamera.enabled = false;
            _runCamera.enabled = true;
        }

        public void ExploreCamera()
        {           
            _hubWalkCamera.enabled = true;
            _hubMapCamera.enabled = false;
        }

        public void MapCamera()
        {
            //_hubWalkCamera.m_Lens.ModeOverride = LensSettings.OverrideModes.Orthographic;
            _hubMapCamera.enabled = true;
            _hubWalkCamera.enabled = false;
        }
    }
}