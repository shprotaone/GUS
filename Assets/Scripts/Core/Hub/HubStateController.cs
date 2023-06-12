using GUS.Core.GameState;
using GUS.Core.Locator;
using GUS.Player;
using GUS.Player.State;

namespace GUS.Core.Hub
{
    public class HubStateController : IStateChanger
    {
        private PlayerStateMachine _playerStateMachine;
        private GameStateMachine _gameStateMachine;
        private SceneHandler _sceneHandler;
        private PlayerActor _playerActor;

        public void Init(IServiceLocator serviceLocator)
        {
            _playerStateMachine = serviceLocator.Get<PlayerStateMachine>();
            _gameStateMachine = serviceLocator.Get<GameStateMachine>();
            _sceneHandler = serviceLocator.Get<SceneHandler>();
            _playerActor= serviceLocator.Get<PlayerActor>();
        }

        public void Idle ()
        {
            _gameStateMachine.InitGameLoop(_gameStateMachine.initMapState);            
            _playerActor.AnimatorController.RunActivate(false);
        }

        public void Explore()
        {
            _gameStateMachine.TransitionTo(_gameStateMachine.explore);
            _playerStateMachine.InitGameLoop(_playerStateMachine.exploreState);
        }

        public void SceneLoadToRun()
        {
            _sceneHandler.LoadOtherScene();
        }
    }
}
