using Cinemachine;
using GUS.Player.Movement;
using GUS.Player;
using System;
using UnityEngine;
using DG.Tweening;
using System.Collections;

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

        [SerializeField] private float _height;

        private float _shakeTime;
        public IEnumerator ShakeCamera(float intensity, float time)
        {   
            CinemachineBasicMultiChannelPerlin channelPerlin = _runCamera.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
            channelPerlin.m_AmplitudeGain = intensity;
            yield return new WaitForSeconds(time);
            channelPerlin.m_AmplitudeGain = 0;
            
        }
        public void ClickerCamera()
        {
            _clickerCamera.enabled = true;
            _runCamera.enabled = false;
        }

        public void RunCamera()
        {
            _clickerCamera.enabled = false;
            _runCamera.enabled = true;
            _hubWalkCamera.enabled = false;
        }

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

        public void CameraCalculate(RunMovement movement)
        {
            float posX;

            if (movement.Line == Utils.Line.Left)
            {
                posX = -2f;
            }
            else if (movement.Line == Utils.Line.Right)
            {
                posX = 2f;
            }
            else
            {
                posX = 0;
            }
            _playerTarget.DOMoveX(posX,1);
        }
    }
}