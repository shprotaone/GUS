using GUS.Core;
using GUS.Core.GameState;
using GUS.Core.Locator;
using System;

namespace GUS.Player.State
{
    public class PlayerStateMachine : IStateMachine
    {
        public event Action stateChanged;

        public InitPlayerState initState { get; private set; }
        public IdlePlayerState startState { get; private set; }
        public RunPlayerState runState { get; private set; }
        public JumpPlayerState jumpState { get; private set; }
        public AttackPlayerState attackState { get; private set; }
        public FlyPlayerState flyState { get; private set; }
        public ExplorePlayerState exploreState { get; private set; }
        public DeathPlayerState deathState { get; private set; }
        public DownSlideState downslide { get; private set; }
        public ClickerPlayerState clicker { get; private set; }
        public PausePlayerState pauseState { get; private set; }

        private AnimatorController _animator;
        private PlayerActor _player;

        public IState CurrentState { get; private set; }
        public IState ActionState { get; private set; }
        public LevelSettings LevelSettings { get; private set; }
        public IState PreviousState { get; private set; }

        public void Init(IServiceLocator service)
        {
            LevelSettings = service.Get<LevelSettings>();
            _player = service.Get<PlayerActor>();

            initState = new InitPlayerState(this, _player);
            pauseState = new PausePlayerState(this, _player);
            //base movement in runner
            runState = new RunPlayerState(this, LevelSettings, _player);
            jumpState = new JumpPlayerState(this, LevelSettings, _player);
            downslide = new DownSlideState(LevelSettings.downSlideTime, _player, this);
            attackState = new AttackPlayerState(this, _player);
            deathState = new DeathPlayerState(this, _player);

            //special movement
            clicker = new ClickerPlayerState(_player, this);
            //flyState = new FlyPlayerState(settings.steerSpeed,_movement,_player,this);
            exploreState = new ExplorePlayerState(LevelSettings.exploreSpeed, _player, this);
        }

        public void InitGameLoop(IState state)
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
            runState.SetMoveSettings(settings);
            jumpState.SetJump(settings.jumpHeight);
            downslide.SetCrouch(settings.downSlideTime);
        }
    }
}

