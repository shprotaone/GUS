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

    private bool _introInclude;
    private int _currentTime;
    private void Start()
    {
        _currentTime = _startTime;
    }
    public IEnumerator StartTimer()
    {
        _panel.SetActive(true);
        ViewActivate();
        while(_currentTime > 0)
        {
            _text.text = _currentTime.ToString();
            _currentTime--;
            yield return new WaitForSeconds(1);
        }
        ViewDeactivate();
        
        _panel.SetActive(false);
        ResetTimer();
    }

    private void ResetTimer()
    {
        _currentTime = _startTime;
    }

    private void ViewActivate()
    {
        if (_introInclude)
        {
            _camera.enabled = true;
            _camera.Priority = 15;
            _playable.Play();
        }
    }

    private void ViewDeactivate()
    {
        _camera.Priority = 0;
        _playable.Stop();
        _camera.enabled = false;
    }

    public void WithIntro(bool flag)
    {
        _introInclude = flag;
    }
}
