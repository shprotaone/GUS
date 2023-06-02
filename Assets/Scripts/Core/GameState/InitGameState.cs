using GUS.Core.Data;
using GUS.Core.Locator;
using GUS.LevelBuild;
using System.Collections;
using TMPro;

namespace GUS.Core.GameState
{
    public class InitGameState : IState
    {
        private WorldController _worldController;
        private Wallet _wallet;
        private TMP_Text _stateText;

        public IStateMachine StateMachine { get; private set; }
        public InitGameState(IStateMachine stateMachine, IServiceLocator serviceLocator, TMP_Text text) 
        {           
            _stateText = text;
            _wallet = serviceLocator.Get<Wallet>();
            StateMachine = stateMachine;
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