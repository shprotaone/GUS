using GUS.Core.Locator;
using System.Collections;

namespace GUS.Core.GameState
{
    public class ShopHubState : IState
    {
        public IStateMachine StateMachine { get;private set; }

        public ShopHubState(IStateMachine stateMachine, IServiceLocator serviceLocator)
        {
            StateMachine = stateMachine;

        }

        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator Execute()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }

        public void FixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}