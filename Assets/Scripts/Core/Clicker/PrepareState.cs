using GUS.Core.GameState;
using GUS.Core.Locator;
using GUS.Core.UI;
using GUS.LevelBuild;
using GUS.Player;
using System.Collections;
using UnityEngine;

namespace GUS.Core.Clicker
{
    public class PrepareState : IState
    {
        private IServiceLocator _serviceLocator;
        private ClickerStateMachine _clickerStateMachine;
        private CameraRunController _cameraController;
        private WorldController _worldController;
        private UIController _uiController;
        private ClickerGame _game;
        private RoutineExecuter _routineExecuter;

        private float _prepareTime;
        public IStateMachine StateMachine {get; private set;}
        public PrepareState(ClickerStateMachine stateMachine, IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
            _clickerStateMachine = stateMachine;
            _worldController = serviceLocator.Get<WorldController>();
            _uiController = serviceLocator.Get<UIController>();

            if (serviceLocator.Get<ICamera>() is CameraRunController cam)
            {
                _cameraController = cam;
            }          
        }

        public void Enter()
        {
            Debug.Log("Подготовительный");
            if(_game == null)
            {
                _game = _serviceLocator.Get<ClickerGame>();
                _game.SetStateMachine(_clickerStateMachine);
            }
            
            _cameraController.ClickerCamera();
            _uiController.HPSliderActivate(true);                  
            _prepareTime = _game.Settings.prepareTime;

            if (_game.Enemy.IsAlive)
            {
                _clickerStateMachine.CallRoutine();
                _game.Enemy?.MoveToDamage(true, _prepareTime);
            }

            _game.Paused(false);
        }

        public IEnumerator Execute()
        {
            float time = _prepareTime;
            while (time > 0)
            {
                yield return new WaitForSeconds(1);
                time -= 1;
                Debug.Log(time.ToString());
            }

            _clickerStateMachine.TransitionTo(_clickerStateMachine.biteState);

            yield break;
        }

        public void Exit()
        {
            //_clicker.Paused(true);
            //_worldController.CreateOnlyFreePlatforms(false);
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