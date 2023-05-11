using GUS.Core.Locator;
using GUS.Core.UI;
using GUS.LevelBuild;
using GUS.Player;
using System;
using System.Collections;
using TMPro;
using UnityEngine.UI;

namespace GUS.Core.GameState
{
    public class ClickerState : IState
    {
        private CameraController _cameraController;
        private WorldController _worldController;
        private UIController _uiController;

        public UIController UIController => _uiController;
        public ClickerState(IServiceLocator serviceLocator) 
        {
            _cameraController = serviceLocator.Get<CameraController>();
            _worldController = serviceLocator.Get<WorldController>();
            _uiController= serviceLocator.Get<UIController>();
        }
        public void Enter()
        {
            _cameraController.ClickerCamera();
            _uiController.HPSliderActivate(true);
            _worldController.WorldStopper(true);
        }

        public IEnumerator Execute()
        {
            yield return null;
        }

        public void Exit()
        {            
            _uiController.HPSliderActivate(false);
            _worldController.WorldStopper(false);
        }

        public void FixedUpdate()
        {
           
        }

        public void Update()
        {
            
        }

        public Slider GetClickerSlider()
        {
            return _uiController.GetClickerSlider();
        }
    }
}