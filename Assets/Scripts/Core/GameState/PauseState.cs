using GUS.Core.Locator;
using GUS.Core.UI;
using System.Collections;
using TMPro;

namespace GUS.Core.GameState
{
    public class PauseState : IState
    {
        private UIController _controller;
        private TMP_Text _stateText;

        public PauseState(IServiceLocator serviceLocator, TMP_Text text)
        {
            _stateText = text;
            _controller = serviceLocator.Get<UIController>();
        }

        public void Enter()
        {
            _stateText.text = "Pause " + this.GetType().Name;
            _controller.PausePanel(true);
        }

        public IEnumerator Execute()
        {
            yield return null;
        }

        public void Exit()
        {
            _controller.PausePanel(false);
        }

        public void FixedUpdate()
        {
            
        }

        public void Update()
        {
            
        }
    }
}