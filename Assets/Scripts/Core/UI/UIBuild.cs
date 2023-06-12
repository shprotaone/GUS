using GUS.Core.Data;
using GUS.Core.GameState;
using GUS.Core.Hub;
using GUS.Core.Locator;
using GUS.Core.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBuild : MonoBehaviour
{
    [SerializeField] private RectTransform _panel;
    [SerializeField] private Button _explore;
    [SerializeField] private Button _close;
    [SerializeField] private TMP_Text _walletText;

    private Wallet _wallet;
    private BuildsSystem _buildSystem;
    private HubStateController _controller;
    private CameraHubController _cameraController;
    public void Init(IServiceLocator serviceLocator)
    {
        _controller = serviceLocator.Get<HubStateController>();
        _cameraController = serviceLocator.Get<ICamera>() as CameraHubController;
        _wallet = serviceLocator.Get<Wallet>();
        _buildSystem = serviceLocator.Get<BuildsSystem>();
        _buildSystem.OnBuyed += RefreshWallet;

        _close.onClick.AddListener(Close);
        _explore.onClick.AddListener(Explore);
    }

    public void Activate(bool flag)
    {
        _panel.gameObject.SetActive(flag);
        RefreshWallet();
        _cameraController.MapCamera();
    }

    private void Explore()
    {
        _controller.Explore();
        _panel.gameObject.SetActive(false);
    }

    private void RefreshWallet()//TODO: есть интерфейс, вернуть!
    {
        _walletText.text = _wallet.Coins.ToString();
    }

    private void Close()
    {
        _controller.Idle();
        _panel.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _buildSystem.OnBuyed -= RefreshWallet;
    }
}
