namespace GUS.Core.GameState
{
    public interface IStateMachine
    {
        public IState CurrentState { get; }
        public IState PreviousState { get; }

        public void InitGameLoop(IState state);
        public void TransitionTo(IState nextState);
        public void Update();
        public void FixedUpdate();
    }
}