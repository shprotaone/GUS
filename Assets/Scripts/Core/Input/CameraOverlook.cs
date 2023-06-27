using Cinemachine;
using GUS.Core.InputSys.Joiystick;
using UnityEngine;

public class CameraOverlook : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private CinemachineFreeLook _freelook;

    void Update()
    {
        if (_joystick.IsActive)
        {
            _freelook.m_XAxis.m_InputAxisValue = _joystick.Horizontal;
        }
        else
        {
            _freelook.m_XAxis.m_InputAxisValue = 0;
        }       
    }
}
