﻿using Cinemachine;
using GUS.Player.Movement;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using System;

namespace GUS.Core.GameState
{
    public class CameraRunController: MonoBehaviour,ICamera
    {
        [SerializeField] private CinemachineBrain _brain;
        [SerializeField] private CinemachineVirtualCamera _clickerCamera;
        [SerializeField] private CinemachineVirtualCamera _biteCamera;
        [SerializeField] private CinemachineFreeLook _runCamera;

        [SerializeField] private Transform _playerTarget;
        [SerializeField] private Transform _pointView;

        [SerializeField] private float _height;
        [SerializeField] private float _moveDistance;
        [SerializeField] private float _speedMovement;

        private CinemachineVirtualCameraBase _currentCamera;
        private CinemachineVirtualCameraBase _prevCamera;
        private float _shakeTime;

        public void ClickerCamera()
        {
            _currentCamera.enabled= false;
            _currentCamera = _clickerCamera;
            _currentCamera.enabled= true;
        }

        public void RunCamera()
        {
            if(_currentCamera != null)
            {
                _currentCamera.enabled = false;
            }
            _currentCamera = _runCamera;
            _currentCamera.enabled = true;
        }

        public void CameraCalculate(RunMovement movement)
        {
            float posX;

            if (movement.Line == Utils.Line.Left)
            {
                posX = -_moveDistance;
                
            }
            else if (movement.Line == Utils.Line.Right)
            {
                posX = _moveDistance;
            }
            else
            {
                posX = 0;
            }
            _playerTarget.DOMoveX(posX,_speedMovement).SetEase(Ease.OutQuint);
            _pointView.DOMoveX(posX, _speedMovement);
        }

        public void SecondFloor(bool flag)
        {
            if(flag)
            {
                _playerTarget.DOMoveY(4.5f, _speedMovement);
            }
            else
            {
                DOVirtual.DelayedCall(0.5f, () => _playerTarget.DOMoveY(2.5f, _speedMovement));
            }
        }

        public void BiteCamera()
        {
            _currentCamera.enabled = false;
            _currentCamera = _biteCamera;
            _currentCamera.enabled = true;
        }

        public void FOVIncrement(float value)
        {
            _biteCamera.m_Lens.FieldOfView -= value;
        }

        public void FOVReset()
        {
            _biteCamera.m_Lens.FieldOfView = 80;
        }

        public void ShackeCameraHandle(float intensity, float time)
        {
            StartCoroutine(ShakeCamera(intensity, time));
        }

        private IEnumerator ShakeCamera(float intensity, float time)
        {
            CinemachineBasicMultiChannelPerlin channelPerlin = _currentCamera.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
            channelPerlin.m_AmplitudeGain = intensity;
            yield return new WaitForSeconds(time);
            channelPerlin.m_AmplitudeGain = 0;

        }
    }
}