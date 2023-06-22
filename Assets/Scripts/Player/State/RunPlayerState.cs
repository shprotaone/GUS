using GUS.Core;
using GUS.Core.GameState;
using GUS.Player.Movement;
using System.Collections;

namespace GUS.Player.State
{
    public class RunPlayerState : IState
    {
        private PlayerActor _player;
        private AnimatorController _animatorController;
        private RunMovement _movement;
        private float _steerSpeed;

        public IStateMachine StateMachine {get; private set;}

        public RunPlayerState(IStateMachine statemMachine, LevelSettings settings, PlayerActor player)
        {       
            _player = player;
            _steerSpeed = settings.steerSpeed;
            StateMachine = statemMachine;
            _animatorController = player.AnimatorController;
            _movement = player.ServiceLocator.Get<RunMovement>();
            _movement.Init(_player, (PlayerStateMachine)StateMachine, _steerSpeed);
            SetMoveSettings(settings);
        }

        public void SetMoveSettings(LevelSettings settings)
        {
            _movement.SetDistance(settings.distanceToMovement);          
            _movement.SetGravity(settings.gravity);
            _movement.SetGravityScale(settings.gravityScale);
            _animatorController.ChangeAnimationSpeed(settings.maxWorldSpeed);
        }

        public void Enter()
        {          
            _player.SetMovementType(_movement);
            _player.ResetDeath();
            _movement.CanMove(true);           
            _animatorController.RunActivate(true);
            _player.CameraHandler(_movement);           
        }

        public IEnumerator Execute()
        {

            yield return null;
        }

        public void Exit()
        {
            _animatorController.RunActivate(false);
        }

        public void Update()
        {
            _movement.Update();
        }

        public void FixedUpdate()
        {
            _movement.FixedUpdate();
        }
    }
}