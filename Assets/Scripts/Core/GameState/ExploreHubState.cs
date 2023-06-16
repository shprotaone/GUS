using GUS.Core.InputSys;
using GUS.Core.InputSys.Joiystick;
using GUS.Core.Locator;
using GUS.Player;
using System.Collections;
using UnityEngine;

namespace GUS.Core.GameState
{
    public class ExploreHubState : IState
    {
        private CameraHubController _cameraController;
        private SceneHandler _sceneHandler;
        private UiHubController _uiHubController;
        private IInputType _inputType;

        public IStateMachine StateMachine { get; private set; }
        public ExploreHubState(IStateMachine stateMachine, IServiceLocator serviceLocator)
        {
            if (serviceLocator.Get<ICamera>() is CameraHubController cam)
            {
                _cameraController = cam;
            }

            _sceneHandler = serviceLocator.Get<SceneHandler>();
            _uiHubController= serviceLocator.Get<UiHubController>();
            _inputType = serviceLocator.Get<IInputType>();
            StateMachine = stateMachine;
        }
       
        public void Enter()
        {
            if(_inputType is FloatingJoystick joystick)
            {
                joystick.gameObject.SetActive(true);
            }

            Delay();
            _uiHubController.MainPanel(true);
            _uiHubController.UIMainHub.UpPanelActivate(false);
            _uiHubController.UIMainHub.DownPanelActivate(false);
            _uiHubController.ExplorePanelActivate(true);

        }

        public IEnumerator Execute()
        {
            yield return new WaitForSeconds(1);
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
            await _cameraController.ExploreCamera();
            await _sceneHandler.FadeInHandle();           
        }
    }
}