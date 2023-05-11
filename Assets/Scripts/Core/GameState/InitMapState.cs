using GUS.Core.Locator;
using System.Collections;
using TMPro;
using UnityEngine;

namespace GUS.Core.GameState
{
    public class InitMapState : IState
    {
        private CameraController _cameraController;
        private SceneHandler _sceneHandler;
        public InitMapState(IServiceLocator serviecLocator, TMP_Text text)
        {
            _cameraController = serviecLocator.Get<CameraController>();
            _sceneHandler = serviecLocator.Get<SceneHandler>();
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