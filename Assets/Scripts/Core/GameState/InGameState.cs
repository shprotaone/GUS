using GUS.Core.Data;
using GUS.Core.Locator;
using GUS.LevelBuild;
using System.Collections;
using TMPro;
using UnityEngine;

namespace GUS.Core.GameState
{
    public class InGameState : IState
    {
        private CameraRunController _cameraController;
        private WorldController _worldController;
        private DistanceData _distanceData;

        private bool _isTimer;

        public IStateMachine StateMachine {get; private set;}
        public InGameState(IStateMachine stateMachine, IServiceLocator serviceLocator)
        {
            _cameraController = serviceLocator.Get<ICamera>() as CameraRunController;
            _worldController = serviceLocator.Get<WorldController>();
            _distanceData = serviceLocator.Get<DistanceData>();
            StateMachine = stateMachine;
        }
        
        public void Enter()
        {            
            _cameraController.RunCamera();
            _worldController.Move();            
            _isTimer = true;
        }

        public IEnumerator Execute()
        {
            while(_isTimer)
            {
                _distanceData.Set(-(int)_worldController.CurrentDistance);
                yield return new WaitForFixedUpdate();
            }                       
        }

        public void Exit()
        {
            _isTimer = false;
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