using GUS.Core.GameState;
using GUS.Core.Locator;
using GUS.Player.State;

namespace GUS.Core.Hub
{
    public class HubStateController : IStateChanger
    {
        private PlayerStateMachine _playerStateMachine;
        private GameStateMachine _gameStateMachine;
        private SceneHandler _sceneHandler;

        public void Init(IServiceLocator serviceLocator)
        {
            _playerStateMachine = serviceLocator.Get<PlayerStateMachine>();
            _gameStateMachine = serviceLocator.Get<GameStateMachine>();
            _sceneHandler = serviceLocator.Get<SceneHandler>();
        }

        public void Idle ()
        {
            _gameStateMachine.InitGameLoop(_gameStateMachine.initMapState);
        }

        public void Explore()
        {
            _playerStateMachine.InitGameLoop(_playerStateMachine.initState);
            _gameStateMachine.TransitionTo(_gameStateMachine.explore);
            _playerStateMachine.TransitionTo(_playerStateMachine.exploreState);
        }

        public void SceneLoadToRun()
        {
            _sceneHandler.LoadOtherScene();
        }
    }
}
