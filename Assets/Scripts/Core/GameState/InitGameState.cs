using System.Collections;
using TMPro;

namespace GUS.Core.GameState
{
    public class InitGameState : IState
    {
        private TMP_Text _stateText;

        public InitGameState()
        {
            
        }
        public InitGameState(TMP_Text text) 
        {
            _stateText = text;
        }
        public void Enter()
        {
            //_stateText.text = "Enter to " + this.GetType().Name;
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