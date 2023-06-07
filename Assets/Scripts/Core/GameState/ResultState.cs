using GUS.Core.UI;
using System.Collections;

namespace GUS.Core.GameState
{
    public class ResultState : IState
    {
        private UIController _UIcontroller;
        public IStateMachine StateMachine { get; private set; }

        public ResultState(IStateMachine stateMachine, Locator.IServiceLocator serviceLocator) 
        {
            StateMachine = stateMachine;
            _UIcontroller = serviceLocator.Get<UIController>();
        }
        
        public void Enter()
        {
            _UIcontroller.UIEndGame.Panel(true);
        }

        public IEnumerator Execute()
        {
            yield return null;
        }

        public void Exit()
        {
            _UIcontroller.UIEndGame.Save();
            _UIcontroller.UIEndGame.Panel(false);
        }

        public void FixedUpdate()
        {
            
        }

        public void Update()
        {
            
        }
    }
}