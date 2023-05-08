using GUS.Core.Locator;
using GUS.LevelBuild;
using System.Collections;
using UnityEngine;

namespace GUS.Core.GameState
{
    public class EndGameState : IState
    {
        private WorldController _worldCotroller;
        private UIController _uiController;
        public EndGameState(IServiceLocator serviceLocator)
        {
            _worldCotroller = serviceLocator.Get<WorldController>();
            _uiController= serviceLocator.Get<UIController>();
        }
        public void Enter()
        {
            _worldCotroller.WorldStopper(true);
        }

        public IEnumerator Execute()
        {
            yield return new WaitForSeconds(1);
            _uiController.RetryButton.gameObject.SetActive(true);
            yield return null;
        }

        public void Exit()
        {
            _uiController.RetryButton.gameObject.SetActive(false);
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