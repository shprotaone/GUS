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

        public readonly InitGameState initState;
        public readonly InitMapState initMapState;
        public readonly StartState start;
        public readonly InGameState session;
        public readonly ClickerState clicker;
        public readonly EndGameState endGame;
        public readonly ResultState result;
        public readonly PauseState pause;
        public readonly ExploreState explore;

        public event Action stateChanged;

        public GameStateMachine(TMP_Text text, IServiceLocator serviceLocator,bool isHub)
        {
            initMapState = new InitMapState(this, serviceLocator,text);
            explore = new ExploreState(this, serviceLocator);
        }

        public GameStateMachine(TMP_Text text,IServiceLocator serviceLocator)
        {
            initState = new InitGameState(this,serviceLocator,text);
            start = new StartState(this,serviceLocator, text);
            session = new InGameState(this, serviceLocator,text);
            clicker = new ClickerState(this, serviceLocator);
            endGame = new EndGameState(this, serviceLocator);
            result = new ResultState(this);
            pause = new PauseState(this, serviceLocator,text);
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


