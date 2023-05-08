using Cinemachine;
using GUS.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BuildViewer : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playable;
    [SerializeField] private CinemachineVirtualCamera _camera;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerActor actor))
        {
            ViewActivate();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.TryGetComponent(out PlayerActor actor))
        {
            ViewDeactivate();
        }
    }

    private void ViewActivate()
    {
        _camera.gameObject.SetActive(true);
        _playable.Play();
    }

    private void ViewDeactivate() 
    {
        _camera.Priority = 9;
        _camera.gameObject.SetActive(false);
    }
}
