using UnityEngine;

public class ChangeLightSettings : MonoBehaviour
{
    [Header("��������� ������ ����")]
    [SerializeField] private float _fogStart;
    [SerializeField] private float _fogEnd;

    [Header("��������� ������ ������")]
    [SerializeField] private float _fogStartRun;
    [SerializeField] private float _fogEndRun;
    
    [SerializeField] private bool _isHub;


    //TODO �������������
    private void Start()
    {
        if (_isHub)
        {
            RenderSettings.fogStartDistance= _fogStart;
            RenderSettings.fogEndDistance= _fogEnd;
        }
        else
        {
            RenderSettings.fogStartDistance= _fogStartRun;
            RenderSettings.fogEndDistance= _fogEndRun;
        }
    }
}
