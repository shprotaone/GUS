using GUS.Core.Locator;
using GUS.Core.UI;
using GUS.LevelBuild;
using System.Collections;
using TMPro;

namespace GUS.Core.GameState
{
    public class StartState : IState
    {
        private CameraRunController _cameraController;
        private GameStateController _gameState;
        private AudioService _audioService;
        private UIStartGame _view;
        private TMP_Text _stateText;
        private WorldController _worldController;

        public IStateMachine StateMachine {get; private set;}

        public StartState(IStateMachine stateMachine,IServiceLocator serviceLocator)
        {
            _view = serviceLocator.Get<UIController>().UIStartGame;
            _worldController = serviceLocator.Get<WorldController>();
            _audioService= serviceLocator.Get<AudioService>();
            _cameraController = serviceLocator.Get<ICamera>() as CameraRunController;
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
            _cameraController.RunCamera();
            _audioService.PlayMusic(_audioService.Data.runner);
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
            _worldController.Move();
        }

        public void WithStartCut(bool flag) => _view.WithIntro(flag);
    }
}