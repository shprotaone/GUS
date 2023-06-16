using GUS.Core.Hub;
using GUS.Core.Locator;
using GUS.Core.UI;
using GUS.SceneManagment;
using UnityEngine;
using UnityEngine.UI;

public class UIExplore : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private GameObject _panel;

    private HubStateController _hubController;
    private SceneHandler _fader;
    public void Init(IServiceLocator serviceLocator)
    {
        _hubController = serviceLocator.Get<HubStateController>();
        _fader = serviceLocator.Get<SceneHandler>();
        _exitButton.onClick.AddListener(Exit);
    }

    public void Activate(bool flag)
    {
        _panel.SetActive(flag);
    }

    private void Exit()
    {
        _hubController.Idle();

        Activate(false);
    }

}
