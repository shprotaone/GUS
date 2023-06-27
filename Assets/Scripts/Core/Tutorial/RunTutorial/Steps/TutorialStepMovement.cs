using Cysharp.Threading.Tasks;
using GUS.Core.Locator;
using GUS.Core.UI;
using GUS.LevelBuild;
using GUS.Player;
using UnityEngine;

namespace GUS.Core.Tutorial
{
    public class TutorialStepMovement : MonoBehaviour, IStepTrigger
    {
        [SerializeField] private int _stepIndex;
        [SerializeField] private EnumBind _direction;

        private UITutorial _view;
        private WorldController _worldContrtoller;
        private PlayerActor _player;
        private TutorialSystemRun _tutorialRun;
        private bool _isActive = true;

        public void Init(IServiceLocator serviceLocator)
        {
            _view = serviceLocator.Get<UIController>().Tutorial;
            _tutorialRun= serviceLocator.Get<TutorialSystemRun>();
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
            _player.InputType.Blocker(false);
            _player.MovementType.CanMove(false);
        }

        public void Waiter()
        {
            if (_player.InputType.Movement() == _direction)
            {
                _player.MovementType.CanMove(true);
                _player.InputType.Blocker(false);

                _worldContrtoller.WorldStopper(false);

                _player.AnimatorController.Pause(false);
                _player.MovementType.CallMove(_direction);
                _player.InputType.Blocker(true);

                _view.CurrentViewStep.Disable();
                CheckLastStep();
                _view.OnWaiter-= Waiter;
            }
        }

        private void CheckLastStep()
        {
            if(_stepIndex == _tutorialRun.TutorialSteps - 1)
            {
                _view.CallEndPanel();
                _tutorialRun.EndTutorial();
                Debug.Log("END");
            }
        }
    }
}

