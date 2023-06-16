using GUS.Core.Hub;
using GUS.Core.InputSys;
using GUS.Core.InputSys.Joiystick;
using GUS.Core.Locator;
using GUS.Player;
using GUS.Player.State;
using System.Collections;

namespace GUS.Core.GameState
{
    public class IdleHubState : IState
    {
        private UiHubController _uiController;
        private CameraHubController _cameraController;
        private PlayerStateMachine _playerStateMachine;
        private SceneHandler _sceneHandler;
        private IInputType _input;
        private HubStateController _hubStateController;

        public IStateMachine StateMachine {get; private set;}

        public IdleHubState(IStateMachine stateMachine, IServiceLocator serviceLocator)
        {
            _cameraController = serviceLocator.Get<ICamera>() as CameraHubController;
            _sceneHandler= serviceLocator.Get<SceneHandler>();
            _playerStateMachine = serviceLocator.Get<PlayerStateMachine>();
            _uiController= serviceLocator.Get<UiHubController>();
            _input = serviceLocator.Get<IInputType>();
            _hubStateController = serviceLocator.Get<HubStateController>();
            StateMachine = stateMachine;
        }

        public void Enter()
        {    
            if(_input is FloatingJoystick joystick)
            {
                joystick.gameObject.SetActive(false);
            }

            Delay();
            _playerStateMachine.TransitionTo(_playerStateMachine.idleState);         
            _uiController.MainPanel(true);
        }

        public IEnumerator Execute()
        {          
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
            
        }

        private async void Delay()
        {
            await _sceneHandler.FadeOutHandle();
            await _cameraController.IdleCamera();
            await _hubStateController.ResetPosition();
            _uiController.UIMainHub.UpPanelActivate(true);
            _uiController.UIMainHub.DownPanelActivate(true);
            await _sceneHandler.FadeInHandle();

        }
    }
}