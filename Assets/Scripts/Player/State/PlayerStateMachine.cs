using GUS.Core;
using GUS.Core.Locator;
using System;

namespace GUS.Player.State
{
    public class PlayerStateMachine
    {
        private AnimatorController _animator;
        private PlayerActor _player;
        public IState CurrentState { get; private set; }
        public IState ActionState { get; private set; }

        public readonly InitPlayerState initState;
        public readonly IdlePlayerState startState;
        public readonly RunPlayerState runState;
        public readonly JumpPlayerState jumpState;
        public readonly AttackPlayerState attackState;
        public readonly FlyPlayerState flyState;
        public readonly ExplorePlayerState exploreState;
        public readonly DeathPlayerState deathState;
        public readonly DownSlideState downslide;
        public readonly ClickerPlayerState clicker;

        public event Action stateChanged;

        public PlayerStateMachine(IServiceLocator service)
        {            
            LevelSettings settings = service.Get<LevelSettings>();
            _player = service.Get<PlayerActor>();

            initState = new InitPlayerState(_player,this);
            //base movement in runner
            runState = new RunPlayerState(settings,_player,this);            
            jumpState = new JumpPlayerState(settings,_player,this);
            downslide = new DownSlideState(settings.downSlideTime,_player,this);
            attackState = new AttackPlayerState(_player);
            deathState = new DeathPlayerState(_player);
            //special movement
            clicker = new ClickerPlayerState(_player,this);
            //flyState = new FlyPlayerState(settings.steerSpeed,_movement,_player,this);
            exploreState = new ExplorePlayerState(settings.exploreSpeed,_player, this);
            
        }

        public void Initialize(IState state)
        {
            CurrentState = state;
            state.Enter();
            stateChanged?.Invoke();
        }

        public void TransitionTo(IState nextState)
        {
            CurrentState?.Exit();
            CurrentState = nextState;
            CurrentState.Enter();

            stateChanged?.Invoke();
        }

        public void ToActionState(IState state)
        {
            ActionState = state;
            state.Enter();
        }

        public void Update()
        {
            CurrentState?.Update();                
            ActionState?.Update();
        }

        public void FixedUpdate()
        {
            CurrentState?.FixedUpdate();
        }

        public void UpdateSettings(LevelSettings settings)
        {
            runState.SetMoveSettings(settings.distanceToMovement, settings.gravity, settings.gravityScale);
            jumpState.SetJump(settings.jumpHeight);
            downslide.SetCrouch(settings.downSlideTime);
        }
    }
}

