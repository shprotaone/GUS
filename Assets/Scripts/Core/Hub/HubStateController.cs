using Cysharp.Threading.Tasks;
using GUS.Core.GameState;
using GUS.Core.Locator;
using GUS.Player;
using GUS.Player.State;
using UnityEngine;

namespace GUS.Core.Hub
{
    public class HubStateController : IStateChanger
    {
        private PlayerStateMachine _playerStateMachine;
        private GameStateMachine _gameStateMachine;
        private SceneHandler _sceneHandler;
        private PlayerActor _playerActor;
        private Vector3 _startPos;

        public void Init(IServiceLocator serviceLocator)
        {
            _playerStateMachine = serviceLocator.Get<PlayerStateMachine>();
            _gameStateMachine = serviceLocator.Get<GameStateMachine>();
            _sceneHandler = serviceLocator.Get<SceneHandler>();
            _playerActor= serviceLocator.Get<PlayerActor>();
        }

        public void Idle ()
        {
            _gameStateMachine.InitGameLoop(_gameStateMachine.idleHubState);               
            _playerActor.AnimatorController.RunActivate(false);
        }

        public void Explore()
        {
            _gameStateMachine.TransitionTo(_gameStateMachine.explore);
            _playerStateMachine.InitGameLoop(_playerStateMachine.exploreState);
        }

        public async UniTask ResetPosition()
        {
            _playerActor.gameObject.transform.position = _startPos;
            _playerActor.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
            await UniTask.Yield();
        }
        
        public void SetStartPosition(Vector3 startPos)
        {
            _startPos = startPos;
        }

        public void SceneLoadToRun()
        {
            _sceneHandler.LoadOtherScene();
        }
    }
}
