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

        public ExploreState(IServiceLocator serviceLocator)
        {
            if (serviceLocator.Get<ICamera>() is CameraHubController cam)
            {
                _cameraController = cam;
            }
            _sceneHandler = serviceLocator.Get<SceneHandler>();
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