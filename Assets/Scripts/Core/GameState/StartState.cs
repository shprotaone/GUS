using GUS.Core.Locator;
using GUS.LevelBuild;
using System.Collections;
using TMPro;

namespace GUS.Core.GameState
{
    public class StartState : IState
    {
        private TMP_Text _stateText;
        private WorldController _controller;

        public StartState(TMP_Text stateText,IServiceLocator serviceLocator)
        {
            _stateText = stateText;
            _controller = serviceLocator.Get<WorldController>();
        }

        public void Enter()
        {
            _stateText.text = "Enter to " + this.GetType().Name; 
            _controller.InitStart();
        }

        public IEnumerator Execute()
        {
            yield return null;
        }

        public void Exit()
        {
            
        }

        public void FixedUpdate()
        {
            
        }

        public void Update()
        {

        }
    }
}