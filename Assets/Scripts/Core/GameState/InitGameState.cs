using GUS.Core.Data;
using GUS.Core.Locator;
using GUS.Player;
using GUS.Player.Movement;
using System.Collections;

namespace GUS.Core.GameState
{
    public class InitGameState : IState
    {
        private Wallet _wallet;
        private HonkCoinWallet _honkCoinWallet;
        private PlayerActor _playerActor;
        private RunMovement _run;
        public IStateMachine StateMachine { get; private set; }
        public InitGameState(IStateMachine stateMachine, IServiceLocator serviceLocator) 
        {           
            _wallet = serviceLocator.Get<Wallet>();
            _honkCoinWallet = serviceLocator.Get<HonkCoinWallet>();
            _run = serviceLocator.Get<RunMovement>();
            _playerActor= serviceLocator.Get<PlayerActor>();
            StateMachine = stateMachine;
        }      

        public void Enter()
        {
           _run.ReturnPosition();
            _playerActor.PowerUpHandler.ResetPoweraUps();
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