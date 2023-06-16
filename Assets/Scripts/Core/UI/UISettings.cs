using GUS.Core.Locator;
using GUS.Core.SaveSystem;
using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _resetProgress;
    [SerializeField] private Button _languageButton;

    private DeleteService _deleteService;

    private void Start()
    {
        _resetProgress.onClick.AddListener(DeleteData);
    }
    public void Init(IServiceLocator serviceLocator)
    {
        _deleteService = serviceLocator.Get<DeleteService>();
    }

    public void Activate(bool flag)
    {
        _panel.SetActive(flag);
    }

    public void DeleteData()
    {
        _deleteService.Execute();
    }
}
