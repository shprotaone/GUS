using GUS.Core;
using GUS.Core.Locator;
using GUS.Player.Movement;
using System;

namespace GUS.Player.State
{
    public class PlayerStateMachine
    {
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

        public event Action stateChanged;

        public PlayerStateMachine(IServiceLocator service)
        {            
            LevelSettings settings = service.Get<LevelSettings>();
            _player = service.Get<PlayerActor>();

            initState = new InitPlayerState(_player,this);
            runState = new RunPlayerState(settings,_player,this);            
            jumpState = new JumpPlayerState(settings,_player,this);
            downslide = new DownSlideState(settings.downSlideTime,_player,this);
            attackState = new AttackPlayerState(_player);
            //flyState = new FlyPlayerState(settings.steerSpeed,_movement,_player,this);
            exploreState = new ExplorePlayerState(settings.steerSpeed,_player, this);
            deathState = new DeathPlayerState(_player);
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
    }
}

