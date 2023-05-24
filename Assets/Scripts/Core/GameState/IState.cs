using GUS.Core.GameState;
using System.Collections;

namespace GUS.Core
{
    public interface IState
    {
        IStateMachine StateMachine { get; }
        void Enter();
        IEnumerator Execute();
        void Update();
        void FixedUpdate();
        void Exit();
    }
}