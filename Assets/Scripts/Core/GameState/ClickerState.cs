using GUS.Core.Locator;
using GUS.Core.UI;
using GUS.LevelBuild;
using System.Collections;
using UnityEngine.UI;

namespace GUS.Core.GameState
{
    public class ClickerState : IState
    {
        private CameraController _cameraController;
        private WorldController _worldController;
        private UIController _uiController;

        private bool _isDynamicClicker = true;

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
            _worldController.CreateOnlyFreePlatforms(true);
            //if(_isDynamicClicker) _worldController.WorldStopper(false);
            //else _worldController.WorldStopper(true);

        }

        public IEnumerator Execute()
        {
            yield return null;
        }

        public void Exit()
        {            
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

        public Slider GetClickerSlider()
        {
            return _uiController.GetClickerSlider();
        }

        public void SetMovementClicker(bool flag)
        {
            _isDynamicClicker = flag;
        }
    }
}