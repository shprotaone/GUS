using GUS.Core.Locator;
using System;
using TMPro;

namespace GUS.Core.GameState
{
    [Serializable]
    public class GameStateMachine
    {
        public IState CurrentState { get; private set; }

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
            initMapState = new InitMapState(serviceLocator,text);
            explore = new ExploreState(serviceLocator);
        }

        public GameStateMachine(TMP_Text text,IServiceLocator serviceLocator)
        {
            //Каждый экземпляр Стейта в который должны передаваться настройки?
            initState = new InitGameState(serviceLocator,text);
            start = new StartState(text,serviceLocator);
            session = new InGameState(text,serviceLocator);
            clicker = new ClickerState(serviceLocator);
            endGame = new EndGameState(serviceLocator);
            result = new ResultState();
            pause = new PauseState(serviceLocator,text);
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


