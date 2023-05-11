using GUS.Core.Locator;
using System.Collections;
using TMPro;

namespace GUS.Core.GameState
{
    public class InitGameState : IState
    {
        private Wallet _wallet;
        private TMP_Text _stateText;

        public InitGameState(IServiceLocator serviecLocator, TMP_Text text) 
        {           
            _stateText = text;
            _wallet = serviecLocator.Get<Wallet>();
        }

        public void Enter()
        {
            //_stateText.text = "Enter to " + this.GetType().Name;
            _wallet.ResetCounter();
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