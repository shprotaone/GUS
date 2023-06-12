using GUS.Core.Locator;
using System.Collections;

namespace GUS.Core.GameState
{
    public class InitMapState : IState
    {
        private UiHubController _uiController;
        private AudioService _audioService;
        private CameraHubController _cameraController;
        private SceneHandler _sceneHandler;

        public IStateMachine StateMachine {get; private set;}

        public InitMapState(IStateMachine stateMachine, IServiceLocator serviceLocator)
        {
            _cameraController = serviceLocator.Get<ICamera>() as CameraHubController;
            _sceneHandler = serviceLocator.Get<SceneHandler>();
            _audioService = serviceLocator.Get<AudioService>();
            _uiController= serviceLocator.Get<UiHubController>();
            StateMachine = stateMachine;
        }

        public void Enter()
        {
            _cameraController.IdleCamera();
            _uiController.MainPanel(true);
            //_audioService.PlayMusic(_audioService.Data.mainMenu);
        }

        public IEnumerator Execute()
        {          
            yield return null;
        }

        public void Exit()
        {
            //_audioService.StopMusic();
            _sceneHandler.FadeOutHandle();
        }

        public void FixedUpdate()
        {
           
        }

        public void Update()
        {
            
        }
    }
}