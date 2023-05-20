using Cinemachine;
using GUS.Player.Movement;
using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace GUS.Core.GameState
{
    public class CameraRunController: MonoBehaviour,ICamera
    {
        [SerializeField] private CinemachineBrain _brain;
        [SerializeField] private CinemachineVirtualCamera _clickerCamera;
        [SerializeField] private CinemachineFreeLook _runCamera;

        [SerializeField] private Transform _playerTarget;
        [SerializeField] private Transform _pointView;

        [SerializeField] private float _height;
        [SerializeField] private float _moveDistance;
        [SerializeField] private float _speedMovement;

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
    }
}