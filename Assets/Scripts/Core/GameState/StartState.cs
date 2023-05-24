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
        private WorldController _controller;

        public IStateMachine StateMachine {get; private set;}

        public StartState(IStateMachine stateMachine,IServiceLocator serviceLocator,TMP_Text stateText)
        {
            _view = serviceLocator.Get<UIController>().UIStartGame;
            _stateText = stateText;
            _controller = serviceLocator.Get<WorldController>();
            _audioService= serviceLocator.Get<AudioService>();
            
            if (serviceLocator.Get<ICamera>() is CameraRunController camera)
            {
                _cameraController = camera;
            }

            StateMachine = stateMachine;
        }

        public void Init(GameStateController stateController)
        {
            _gameState = stateController;
        }

        public void Enter()
        {
            _audioService.StopMusic();
            _stateText.text = "Enter to " + this.GetType().Name;
            _controller.InitStart();
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
            _controller.Move();
        }

        public void WithStartCut(bool flag) => _view.WithIntro(flag);
    }
}