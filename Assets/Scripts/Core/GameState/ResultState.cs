using System.Collections;

namespace GUS.Core.GameState
{
    public class ResultState : IState
    {
        public IStateMachine StateMachine { get; private set; }

        public ResultState(IStateMachine stateMachine) 
        {
            StateMachine = stateMachine;
        }
        
        public void Enter()
        {
            
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