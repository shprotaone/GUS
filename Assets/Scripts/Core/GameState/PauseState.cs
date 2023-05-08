using System.Collections;
using TMPro;

namespace GUS.Core.GameState
{
    public class PauseState : IState
    {
        private TMP_Text _stateText;

        public PauseState(TMP_Text text)
        {
            _stateText = text;
        }

        public void Enter()
        {
            _stateText.text = "Pause " + this.GetType().Name;
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