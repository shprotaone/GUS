using GUS.Core.Locator;
using GUS.Core.UI;
using GUS.LevelBuild;
using System.Collections;
using TMPro;

namespace GUS.Core.GameState
{
    public class StartState : IState
    {
        private GameStateController _gameState;
        private UIStartGame _view;
        private TMP_Text _stateText;
        private WorldController _controller;

        public StartState(TMP_Text stateText,IServiceLocator serviceLocator)
        {
            _view = serviceLocator.Get<UIController>().UIStartGame;
            _stateText = stateText;
            _controller = serviceLocator.Get<WorldController>();
        }

        public void Init(GameStateController stateController)
        {
            _gameState = stateController;
        }

        public void Enter()
        {
            _stateText.text = "Enter to " + this.GetType().Name;
            _controller.InitStart();
        }

        public IEnumerator Execute()
        {
            yield return _view.StartTimer();            
            _gameState.StartGame();
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
            _controller.Move();
        }

        public void WithStartCut(bool flag) => _view.WithIntro(flag);
    }
}