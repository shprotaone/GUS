using DG.Tweening;
using GUS.Core;
using GUS.Core.GameState;
using GUS.Core.Hub;
using GUS.Core.Locator;
using GUS.Core.SaveSystem;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainHub : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _commonDistance;
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private MultiplicatorViewer _multiplicatorViewer;
    [Title("")]
    [SerializeField] private RectTransform _upPanel;
    [SerializeField] private RectTransform _downPanel;
    [Title("Кнопки")]
    [SerializeField] private Button _startRun;
    [SerializeField] private Button _shop;
    [SerializeField] private Button _build;
    [SerializeField] private Button _settings;
    

    private HubStateController _stateController;
    private StorageService _storageService;
    private UiHubController _uiHubController;

    public void Init(IServiceLocator serviceLocator)
    {
        _stateController = serviceLocator.Get<IStateChanger>() as HubStateController;
        _storageService = serviceLocator.Get<StorageService>();
        _uiHubController = serviceLocator.Get<UiHubController>();
        _multiplicatorViewer.Init(serviceLocator);

        _settings.onClick.AddListener(Settings);
        _shop.onClick.AddListener(ShopPanel);
        _build.onClick.AddListener(BuildShop);
        _startRun.onClick.AddListener(StartHandle);

        RefreshDistancePointCount();
    }

    private void StartHandle()
    {
        _stateController.SceneLoadToRun();
    }

    private void BuildShop()
    {
        _uiHubController.BuildActivate(true);
    }

    private void ShopPanel()
    {
        _uiHubController.ShopPanel(true);
    }

    private void Settings()
    {
        _uiHubController.SettingsActivate(true);
    }

    private void RefreshDistancePointCount()
    {
        _commonDistance.text = _storageService.Data.commonDistance.ToString();
        _playerName.text = _storageService.Data.playerName.ToString();
    }

    public void Activate(bool flag)
    {
        _panel.SetActive(flag);
    }

    public void UpPanelActivate(bool flag)
    {
        if (!flag) _upPanel.DOAnchorPosY(600, 1);
        else _upPanel.DOAnchorPosY(0, 1);
    }

    public void DownPanelActivate(bool flag)
    {
        if (!flag) _downPanel.DOAnchorPosY(-600, 1);
        else _downPanel.DOAnchorPosY(0, 1);
    }
}
