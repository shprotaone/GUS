using GUS.Core.Locator;
using GUS.Core.UI;
using GUS.LevelBuild;
using System.Collections;
using UnityEngine;

namespace GUS.Core.GameState
{
    public class ClickerState : IState
    {
        private CameraRunController _cameraController;
        private WorldController _worldController;
        private UIController _uiController;
        private ClickerGame _clicker;
        public IStateMachine StateMachine { get; private set; }

        public ClickerState(IStateMachine stateMachine, IServiceLocator serviceLocator) 
        {
            if(serviceLocator.Get<ICamera>() is CameraRunController cam)
            {
                _cameraController = cam;
            }
            _worldController = serviceLocator.Get<WorldController>();
            _uiController= serviceLocator.Get<UIController>();
            StateMachine = stateMachine;
        }        

        public void Enter()
        {
            Debug.Log("Call");
            _clicker = _worldController.PlatformBuilder.NextClickerGame;
            _clicker.InitSlider(_uiController.GetClickerSlider());
            _cameraController.ClickerCamera();
            _uiController.HPSliderActivate(true);
            _worldController.CreateOnlyFreePlatforms(true);
            _clicker.Paused(false);

        }

        public IEnumerator Execute()
        {
            yield return null;
        }

        public void Exit()
        {    
            _clicker.Paused(true);
            _uiController.HPSliderActivate(false);
            //_worldController.WorldStopper(false);
            _worldController.CreateOnlyFreePlatforms(false);
        }

        public void FixedUpdate()
        {
           
        }

        public void Update()
        {
            _worldController.Move();
        }
    }
}