using GUS.Core.Locator;
using System.Collections;
using TMPro;
using UnityEngine;

namespace GUS.Core.GameState
{
    public class InitMapState : IState
    {
        private CameraHubController _cameraController;
        private SceneHandler _sceneHandler;
        public InitMapState(IServiceLocator serviceLocator, TMP_Text text)
        {
            if(serviceLocator.Get<ICamera>() is CameraHubController cam)
            {
                _cameraController = cam;
            }
            _sceneHandler = serviceLocator.Get<SceneHandler>();
        }

        public void Enter()
        {
            _cameraController.MapCamera();
        }

        public IEnumerator Execute()
        {          
            yield return null;
        }

        public void Exit()
        {
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