using GUS.Core.GameState;
using GUS.Core.Locator;
using System;
using System.Collections;

namespace GUS.Core.Clicker
{
    public class ClickerStateMachine : IStateMachine
    {
        public IState CurrentState { get; private set; }
        public IState PreviousState { get; private set; }

        public readonly PrepareState prepareState;
        public readonly BiteState biteState;
        public readonly EndState endState;

        public event Action stateChanged;

        private RoutineExecuter _routineExecuter;

        public ClickerStateMachine(IServiceLocator serviceLocator)
        {
            prepareState = new PrepareState(this, serviceLocator);
            biteState = new BiteState(this, serviceLocator);
            endState = new EndState(this, serviceLocator);

            _routineExecuter = serviceLocator.Get<RoutineExecuter>();
        }

        public void InitGameLoop(IState state)
        {
            CurrentState = state;
            CurrentState.Enter();
            stateChanged?.Invoke();
        }

        public void CallRoutine()
        {
            _routineExecuter.Execute(CurrentState.Execute());
        }

        public void StopRoutine(IEnumerator routine)
        {
            _routineExecuter.Stop(routine);
        }

        public void FixedUpdate()
        {
            CurrentState?.FixedUpdate();
        }

        public void TransitionTo(IState nextState)
        {
            CurrentState.Exit();
            CurrentState = nextState;
            CurrentState.Enter();
            stateChanged?.Invoke();
        }

        public void Update()
        {
            CurrentState?.Update();
        }
    }
}
