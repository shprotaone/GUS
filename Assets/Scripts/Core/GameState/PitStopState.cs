using GUS.Core.Locator;
using GUS.LevelBuild;
using System.Collections;
using TMPro;

namespace GUS.Core.GameState
{
    public class PitStopState : IState
    {

        public PitStopState(IServiceLocator serviceLocator) 
        {
            
        
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