using GUS.Core.Data;
using GUS.Core.Locator;
using GUS.Core.UI;
using GUS.LevelBuild;
using System.Collections;
using UnityEngine;

namespace GUS.Core.GameState
{
    public class EndGameState : IState
    {
        private GameStateController _gameStateController;
        private WorldController _worldCotroller;
        private UIController _uiController;

        public IStateMachine StateMachine {get; private set;}
        public EndGameState(IStateMachine stateMachine, IServiceLocator serviceLocator)
        {
            _worldCotroller = serviceLocator.Get<WorldController>();
            _uiController= serviceLocator.Get<UIController>();
            _gameStateController = serviceLocator.Get<GameStateController>();
            StateMachine = stateMachine;
        }

        public void Enter()
        {           
            _worldCotroller.WorldStopper(true);
            _uiController.UiInGame.Hide(true);
        }

        public IEnumerator Execute()
        {
            yield return new WaitForSeconds(1);
            if (_gameStateController.SecondChance) _uiController.SaveMe(true);
            else _gameStateController.Result();
            
            yield return null;
        }

        public void Exit()
        {
            _uiController.UiInGame.Hide(false);
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