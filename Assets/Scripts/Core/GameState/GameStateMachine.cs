using GUS.Core.Locator;
using GUS.Player.State;
using System;
using TMPro;

namespace GUS.Core.GameState
{
    [Serializable]
    public class GameStateMachine : IStateMachine
    {
        public IState CurrentState { get; private set; }
        public IState PreviousState {get; private set;}

        public InitGameState initState { get; private set; }
        public InitMapState initMapState { get; private set; }
        public StartState start { get; private set; }
        public InGameState session { get; private set; }
        public ClickerState clicker { get; private set; }
        public EndGameState endGame { get; private set; }
        public ResultState result { get; private set; }
        public PauseState pause { get; private set; }
        public ExploreState explore { get; private set; }

        public event Action stateChanged;

        public void Init(IServiceLocator serviceLocator)
        {
            initState = new InitGameState(this, serviceLocator);
            start = new StartState(this, serviceLocator);
            session = new InGameState(this, serviceLocator);
            clicker = new ClickerState(this, serviceLocator);
            endGame = new EndGameState(this, serviceLocator);
            result = new ResultState(this);
            pause = new PauseState(this, serviceLocator);
        }

        public void InitHub(IServiceLocator serviceLocator)
        {
            initMapState = new InitMapState(this, serviceLocator);
            explore = new ExploreState(this, serviceLocator);
        }

        public void InitGameLoop(IState state)
        {            
            CurrentState = state;
            CurrentState.Enter();
            stateChanged?.Invoke();
        }

        public void TransitionTo(IState nextState)
        {
            CurrentState.Exit();
            CurrentState = nextState;
            nextState.Enter();

            stateChanged?.Invoke();
        }

        public void Update()
        {
            CurrentState?.Update();
        }

        public void FixedUpdate()
        {
            CurrentState?.FixedUpdate();
        }
    }
}


