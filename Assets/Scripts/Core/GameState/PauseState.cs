using GUS.Core.Locator;
using GUS.Core.UI;
using GUS.LevelBuild;
using System.Collections;
using TMPro;

namespace GUS.Core.GameState
{
    public class PauseState : IState
    {
        private const float AfterPauseSpeed = 0.7f;

        private AudioService _audioService;
        private UIController _controller;
        private TMP_Text _stateText;
        private PauseHandle _pauseHandle;
        private GameStateController _gameStateController;
        private WorldController _worldController;
        public IStateMachine StateMachine {get; private set;}

        public PauseState(IStateMachine stateMachine, IServiceLocator serviceLocator)
        {
            _pauseHandle = serviceLocator.Get<PauseHandle>();
            _controller = serviceLocator.Get<UIController>();
            _audioService= serviceLocator.Get<AudioService>();
            _gameStateController= serviceLocator.Get<GameStateController>();
            _worldController = serviceLocator.Get<WorldController>();
            StateMachine = stateMachine;
        }

        public void Enter()
        {
            _controller.PausePanel(true);
            _gameStateController.TimePause(true);
            //_pauseHandle.SetPause(true);
            _audioService.Pause();
        }

        public IEnumerator Execute()
        {
            yield return null;
        }

        public void Exit()
        {
            _controller.PausePanel(false);
            _gameStateController.TimePause(false);
            _worldController.ChangeAcceleration(AfterPauseSpeed);
            //_pauseHandle.SetPause(false);
            _audioService.Resume();
        }

        public void FixedUpdate()
        {
            
        }

        public void Update()
        {
            
        }
    }
}