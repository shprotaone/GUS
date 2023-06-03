using GUS.Core.GameState;
using GUS.Core.Locator;
using GUS.LevelBuild;
using GUS.Player.Movement;
using GUS.Player;
using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace GUS.Core.Clicker
{
    public class BiteState : IState
    {
        private PlayerActor _actor;
        private CameraRunController _cameraController;
        private WorldController _worldController;
               
        private ClickerGame _game;
        private ClickerMovement _movement;

        private ClickerStateMachine _clickerStateMachine;
        private IServiceLocator _serviceLocator;
        public IStateMachine StateMachine { get; private set; }
        public BiteState(ClickerStateMachine stateMachine, IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
            _clickerStateMachine = stateMachine;
            _worldController = serviceLocator.Get<WorldController>();           

            if (serviceLocator.Get<ICamera>() is CameraRunController cam)
            {
                _cameraController = cam;
            }
        }

        public void Enter()
        {
            Debug.Log("Кусательный");
            _actor = _serviceLocator.Get<PlayerActor>();
            if (_game == null) _game = _serviceLocator.Get<ClickerGame>();
            _clickerStateMachine.CallRoutine();
            _game.Enemy.MoveToDamage(false,_game.Settings.prepareTime);

        }

        public IEnumerator Execute()
        {
            if (_actor.MovementType is ClickerMovement clickerMovement)
            {
                _movement = clickerMovement;
            }
            _cameraController.BiteCamera();
            yield return new WaitForSeconds(_game.Settings.prepareTime);

            _movement.CanAttack(true);
            _movement.OnClick += _game.GetDamage;
            _movement.OnClick += () => _cameraController.ShackeCameraHandle(5, 0.1f);        
            _actor.ChangeModelPos(0.6f, 0.2f);
            //_actor.AnimatorController.Pause(true);
            yield return null;
        }

        public void Exit()
        {
            Debug.Log("Выход из Кусательного");
            //_actor.AnimatorController.Pause(false);
            _movement.OnClick -= _game.GetDamage;
            _movement.OnClick -= () => _cameraController.ShackeCameraHandle(5, 0.1f);
            _movement.CanAttack(false);
            _actor.ChangeModelPos(-0.3f, 0.2f);
            _clickerStateMachine.StopRoutine(Execute());
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