using GUS.Core.Locator;
using GUS.Core.UI;
using System.Collections;
using TMPro;

namespace GUS.Core.GameState
{
    public class PauseState : IState
    {
        private AudioService _audioService;
        private UIController _controller;
        private TMP_Text _stateText;
        private PauseHandle _pauseHandle;
        private GameStateController _gameStateController;
        public IStateMachine StateMachine {get; private set;}

        public PauseState(IStateMachine stateMachine, IServiceLocator serviceLocator)
        {
            _pauseHandle = serviceLocator.Get<PauseHandle>();
            _controller = serviceLocator.Get<UIController>();
            _audioService= serviceLocator.Get<AudioService>();
            _gameStateController= serviceLocator.Get<GameStateController>();
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