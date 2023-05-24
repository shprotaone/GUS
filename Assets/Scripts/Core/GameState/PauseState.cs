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

        public IStateMachine StateMachine {get; private set;}

        public PauseState(IStateMachine stateMachine, IServiceLocator serviceLocator, TMP_Text text)
        {
            _stateText = text;
            _controller = serviceLocator.Get<UIController>();
            _audioService= serviceLocator.Get<AudioService>();
            StateMachine = stateMachine;
        }

        public void Enter()
        {
            _stateText.text = "Pause " + this.GetType().Name;
            _controller.PausePanel(true);
            _audioService.Pause();
        }

        public IEnumerator Execute()
        {
            yield return null;
        }

        public void Exit()
        {
            _controller.PausePanel(false);
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