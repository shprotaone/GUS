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
        private HonkCoinWallet _honkCoinWallet;
        private TMP_Text _stateText;

        public IStateMachine StateMachine { get; private set; }
        public InitGameState(IStateMachine stateMachine, IServiceLocator serviceLocator) 
        {           
            _wallet = serviceLocator.Get<Wallet>();
            _honkCoinWallet = serviceLocator.Get<HonkCoinWallet>();
            StateMachine = stateMachine;
        }      

        public void Enter()
        {
            //_stateText.text = "Enter to " + this.GetType().Name;
            _wallet.ResetCounter();
            _honkCoinWallet.ResetCounter();
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