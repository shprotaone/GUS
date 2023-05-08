using GUS.Core.Locator;
using GUS.LevelBuild;
using System.Collections;
using TMPro;

namespace GUS.Core.GameState
{
    public class InGameState : IState
    {
        private TMP_Text _stateText;
        private WorldController _worldController;

        public InGameState(TMP_Text stateText, IServiceLocator serviceLocator)
        {
            _stateText = stateText;
            _worldController = serviceLocator.Get<WorldController>();
        }
        public void Enter()
        {
            _stateText.text = "Enter to " + this.GetType().Name;
            _worldController.Move();
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
            _worldController.Move();
        }
    }
}