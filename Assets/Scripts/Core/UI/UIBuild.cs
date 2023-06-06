using GUS.Core.GameState;
using GUS.Core.Hub;
using GUS.Core.Locator;
using GUS.Core.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuild : MonoBehaviour
{
    [SerializeField] private RectTransform _panel;
    [SerializeField] private Button _explore;

    private HubStateController _controller;
    private CameraHubController _cameraController;
    public void Init(IServiceLocator serviceLocator)
    {
        _controller = serviceLocator.Get<HubStateController>();
        _cameraController = serviceLocator.Get<ICamera>() as CameraHubController;
        _explore.onClick.AddListener(Explore);
    }

    public void Activate(bool flag)
    {
        _panel.gameObject.SetActive(flag);
        _cameraController.MapCamera();
    }

    private void Explore()
    {
        _controller.Explore();
        _panel.gameObject.SetActive(false);
    }
}
