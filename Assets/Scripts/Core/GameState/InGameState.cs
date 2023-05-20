using GUS.Core.Locator;
using GUS.LevelBuild;
using GUS.Player;
using GUS.Player.Movement;
using System.Collections;
using TMPro;
using UnityEngine;

namespace GUS.Core.GameState
{
    public class InGameState : IState
    {
        private IServiceLocator _serviceLocator;
        private TMP_Text _stateText;
        private CameraController _cameraController;
        private WorldController _worldController;
        private Wallet _wallet;
        private PlayerActor _actor;

        private bool _isTimer;

        public InGameState(TMP_Text stateText, IServiceLocator serviceLocator)
        {
            _stateText = stateText;
            _serviceLocator = serviceLocator;
            _cameraController = serviceLocator.Get<CameraController>();
            _worldController = serviceLocator.Get<WorldController>();
            _wallet = serviceLocator.Get<Wallet>();
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
                _wallet.SetDistancePoint(1);
                yield return new WaitForSeconds(1);
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