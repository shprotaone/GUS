using GUS.Core.Locator;
using GUS.Core.Tutorial;
using GUS.Core.UI;
using GUS.LevelBuild;
using GUS.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step2Run : MonoBehaviour,IStepTrigger
{
    [SerializeField] private int _stepIndex;
    private UITutorial _view;
    private WorldController _worldContrtoller;
    private PlayerActor _player;
    private bool _isActive = true;

    public void Init(IServiceLocator serviceLocator)
    {
        _view = serviceLocator.Get<UIController>().Tutorial;
        _worldContrtoller = serviceLocator.Get<WorldController>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerActor actor) && _isActive)
        {
            _isActive = false;
            _view.Init();
            Execute(actor);
        }
    }

    private void Execute(PlayerActor actor)
    {
        _player = actor;
        _view.CallStep(_stepIndex);
        _view.OnWaiter += Waiter;
        _worldContrtoller.WorldStopper(true);
        _player.AnimatorController.Pause(true);
        _player.MovementType.CanMove(false);
    }

    public void Waiter()
    {
        if (_player.InputType.Movement() == EnumBind.Down)
        {
            _player.MovementType.CanMove(true);
            _worldContrtoller.WorldStopper(false);
            _player.AnimatorController.Pause(false);
            _player.MovementType.CallMove(EnumBind.Down);
            _view.CurrentViewStep.Disable();
            _view.OnWaiter -= Waiter;
        }
    }
}
