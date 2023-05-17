using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class UIStartGame : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private PlayableDirector _playable;
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private int _startTime;

    public IEnumerator StartTimer()
    {
        _panel.SetActive(true);
        ViewActivate();
        while(_startTime > 0)
        {
            _text.text = _startTime.ToString();
            _startTime--;
            yield return new WaitForSeconds(1);
        }
        ViewDeactivate();
        
        _panel.SetActive(false);
    }

    private void ViewActivate()
    {
        _camera.enabled = true;
        _camera.Priority = 15;
        _playable.Play();
    }

    private void ViewDeactivate()
    {
        _camera.Priority = 0;
        _playable.Stop();
        _camera.enabled = false;
    }
}
