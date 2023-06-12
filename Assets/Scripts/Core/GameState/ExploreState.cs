﻿using GUS.Core.Locator;
using GUS.Player;
using System.Collections;
using UnityEngine;

namespace GUS.Core.GameState
{
    public class ExploreState : IState
    {
        private CameraHubController _cameraController;
        private SceneHandler _sceneHandler;
        private UiHubController _uiHubController;

        public IStateMachine StateMachine { get; private set; }
        public ExploreState(IStateMachine stateMachine, IServiceLocator serviceLocator)
        {
            if (serviceLocator.Get<ICamera>() is CameraHubController cam)
            {
                _cameraController = cam;
            }
            _uiHubController= serviceLocator.Get<UiHubController>();
            _sceneHandler = serviceLocator.Get<SceneHandler>();
            StateMachine = stateMachine;
        }
       
        public void Enter()
        {
            _cameraController.ExploreCamera();
            _sceneHandler.FadeInHandle();
            _uiHubController.MainPanel(true);
        }

        public IEnumerator Execute()
        {
            yield return new WaitForSeconds(1);
            
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