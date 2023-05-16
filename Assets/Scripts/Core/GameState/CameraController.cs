using Cinemachine;
using GUS.Player.Movement;
using GUS.Player;
using System;
using UnityEngine;
using DG.Tweening;

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

        [SerializeField] private Transform _playerTarget;
        [SerializeField] private Transform _pointView;

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

        public void CameraCalculate(RunMovement movement)
        {
            float posX = gameObject.transform.position.x;
            float viewX = 0;

            if (movement.IsLeft)
            {
               // DOVirtual.Float(0, 20, 1, v => SetCamera(v));
                posX = -2f;
            }
            else if (movement.IsRight)
            {
                //DOVirtual.Float(0, -20, 1, v => SetCamera(v));
                posX = 2f;
            }
            else
            {
                //DOVirtual.Float(0, 20, 1, v => SetCamera(v));
                posX = 0;
            }



            _playerTarget.DOMove(new Vector3(posX,1,0), 1);

            //_pointView.DOMove(new Vector3(posX, _pointView.position.y, _pointView.position.z), 1);
        }

        private void SetCamera(float value)
        {
            _runCamera.m_Heading.m_Bias = value;
        }
    }
}