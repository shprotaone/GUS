using GUS.Core.Locator;
using System.Collections;
using TMPro;
using UnityEngine;

namespace GUS.Core.GameState
{
    public class InitMapState : IState
    {
        private AudioService _audioService;
        private CameraHubController _cameraController;
        private SceneHandler _sceneHandler;

        public IStateMachine StateMachine {get; private set;}

        public InitMapState(IStateMachine stateMachine, IServiceLocator serviceLocator, TMP_Text text)
        {
            if(serviceLocator.Get<ICamera>() is CameraHubController cam)
            {
                _cameraController = cam;
            }
            _sceneHandler = serviceLocator.Get<SceneHandler>();
            _audioService = serviceLocator.Get<AudioService>();
            StateMachine = stateMachine;
        }

        public void Enter()
        {
            _cameraController.MapCamera();
            _audioService.PlayMusic(_audioService.Data.mainMenu);
        }

        public IEnumerator Execute()
        {          
            yield return null;
        }

        public void Exit()
        {
            _audioService.StopMusic();
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