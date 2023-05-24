using GUS.Core.Locator;
using GUS.Player;
using System.Collections;
using UnityEngine;

namespace GUS.Core.GameState
{
    public class ExploreState : IState
    {
        private CameraHubController _cameraController;
        private SceneHandler _sceneHandler;

        public IStateMachine StateMachine { get; private set; }
        public ExploreState(IStateMachine stateMachine, IServiceLocator serviceLocator)
        {
            if (serviceLocator.Get<ICamera>() is CameraHubController cam)
            {
                _cameraController = cam;
            }
            _sceneHandler = serviceLocator.Get<SceneHandler>();
            StateMachine = stateMachine;
        }
       
        public void Enter()
        {
            _cameraController.ExploreCamera();
            
        }

        public IEnumerator Execute()
        {
            yield return new WaitForSeconds(1);
            _sceneHandler.FadeInHandle();
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
    }
}