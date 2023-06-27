using GUS.Core.Locator;
using GUS.Core.UI;
using GUS.LevelBuild;
using GUS.Player;
using System.Collections;
using TMPro;

namespace GUS.Core.GameState
{
    public class StartState : IState
    {
        private CameraRunController _cameraController;
        private GameStateController _gameState;
        private PlayerActor _playerActor;
        private AudioService _audioService;
        private UIStartGame _view;
        private UIInGame _inGameView;
        private WorldController _worldController;

        public IStateMachine StateMachine {get; private set;}

        public StartState(IStateMachine stateMachine,IServiceLocator serviceLocator)
        {
            _view = serviceLocator.Get<UIController>().UIStartGame;
            _inGameView = serviceLocator.Get<UIController>().UiInGame;
            _worldController = serviceLocator.Get<WorldController>();
            _audioService= serviceLocator.Get<AudioService>();
            _cameraController = serviceLocator.Get<ICamera>() as CameraRunController;
            _playerActor = serviceLocator.Get<PlayerActor>();
            StateMachine = stateMachine;
        }

        public void Init(GameStateController stateController)
        {
            _gameState = stateController;
        }

        public void Enter()
        {
            _audioService.StopMusic();
            _worldController.InitStart();
            _playerActor.RestartPosition();
            _cameraController.RunCamera();
            _inGameView.RefreshDistancePointCount(0);
            _inGameView.Hide(true);
            _audioService.PlayMusic(_audioService.Data.runner);
        }

        public IEnumerator Execute()
        {
            yield return _view.StartTimer();
            _inGameView.Hide(false);
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
            _worldController.Move();
        }

        public void WithStartCut(bool flag) => _view.WithIntro(flag);
    }
}