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
        public readonly StartState _start;
        public readonly InGameState _session;
        public readonly PitStopState _pitStop;
        public readonly EndGameState _endGame;
        public readonly ResultState _result;
        public readonly PauseState _pause;

        public event Action stateChanged;

        public GameStateMachine(IServiceLocator serviceLocator,bool isHub)
        {
            _pitStop = new PitStopState(serviceLocator);
            initState = new InitGameState();
        }

        public GameStateMachine(TMP_Text text,IServiceLocator serviceLocator)
        {
            //Каждый экземпляр Стейта в который должны передаваться настройки?
            initState = new InitGameState(text);
            _start = new StartState(text,serviceLocator);
            _session = new InGameState(text,serviceLocator);         
            _endGame = new EndGameState(serviceLocator);
            _result = new ResultState();
            _pause = new PauseState(text);
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


