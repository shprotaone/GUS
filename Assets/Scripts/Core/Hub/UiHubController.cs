using GUS.Core;
using GUS.Core.GameState;
using GUS.Core.Locator;
using GUS.Core.SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiHubController : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private TMP_Text _nameText;

    [SerializeField] private Button _settings;
    [SerializeField] private Button _shop;
    [SerializeField] private Button _exploreMode;
    [SerializeField] private Button _startButton;

    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _shopPanel;

    private GameStateController _controller;
    private StorageService _storage;
    private void Start()
    {
        _settings.onClick.AddListener(Settings);
        _shop.onClick.AddListener(ShopPanel);
        _exploreMode.onClick.AddListener(ExploreHandle);
        _startButton.onClick.AddListener(StartHandle);

    }
    public void Init(IServiceLocator locator)
    {
        _controller = locator.Get<GameStateController>();
        _storage = locator.Get<StorageService>();
        _storage.Load();
        _coinText.text = _storage.Data.coins.ToString();  
        _nameText.text = _storage.Data.playerName.ToString();
    }

    public void ExploreHandle() => _controller.Explore();
    public void StartHandle() => _controller.SceneLoadToRun();
    public void Settings() => _settingsPanel.SetActive(true);
    public void ShopPanel() => _shopPanel.SetActive(true);

}