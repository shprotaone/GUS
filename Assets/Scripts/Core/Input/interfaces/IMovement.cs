using GUS.Player.State;

namespace GUS.Player
{
    /// <summary>
    /// Отвечает за биндинг управления
    /// </summary>
    public interface IMovement
    {
        void Init(PlayerActor player, PlayerStateMachine playerState, float speedMovement);
        void Update();
        void FixedUpdate();
        void Move();
        void Fire();
        void CanMove(bool flag);
        void CallMove(EnumBind enumBind);
        void ReturnPosition();

    }
}