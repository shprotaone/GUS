using GUS.Core.Locator;
using GUS.Core.UI;
using GUS.LevelBuild;
using System.Collections;
using UnityEngine;

namespace GUS.Core.GameState
{
    public class EndGameState : IState
    {
        private WorldController _worldCotroller;
        private UIController _uiController;

        public IStateMachine StateMachine {get; private set;}
        public EndGameState(IStateMachine stateMachine, IServiceLocator serviceLocator)
        {
            _worldCotroller = serviceLocator.Get<WorldController>();
            _uiController= serviceLocator.Get<UIController>();
            StateMachine = stateMachine;
        }

        public void Enter()
        {
            _worldCotroller.WorldStopper(true);
        }

        public IEnumerator Execute()
        {
            yield return new WaitForSeconds(1);
            _uiController.UIEndGame.Panel(true);
            yield return null;
        }

        public void Exit()
        {
            _uiController.UIEndGame.Panel(false);
            _worldCotroller.WorldStopper(false);
        }

        public void FixedUpdate()
        {
            
        }

        public void Update()
        {
            
        }
    }
}