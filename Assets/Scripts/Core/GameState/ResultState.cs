using GUS.Core.SaveSystem;
using GUS.Core.Tutorial;
using GUS.Core.UI;
using System.Collections;

namespace GUS.Core.GameState
{
    public class ResultState : IState
    {
        private UIController _UIcontroller;
        private bool _isTutorialComplete;
        public IStateMachine StateMachine { get; private set; }

        public ResultState(IStateMachine stateMachine, Locator.IServiceLocator serviceLocator) 
        {
            StateMachine = stateMachine;
            _UIcontroller = serviceLocator.Get<UIController>();
            _isTutorialComplete = serviceLocator.Get<StorageService>().Data._tutorialSteps[1];
        }
        
        public void Enter()
        {
            _UIcontroller.UIEndGame.Panel(true);
            if (!_isTutorialComplete)
            {
                _UIcontroller.Tutorial.CallStep(6);
            }
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