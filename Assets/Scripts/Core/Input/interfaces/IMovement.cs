using GUS.Player.State;

namespace GUS.Player
{
    /// <summary>
    /// Отвечает за биндинг управления
    /// </summary>
    public interface IMovement
    {
        void Init(PlayerActor player, PlayerStateMachine playerState, float speedMovement);
        public void Update();
        public void FixedUpdate();
        public void Move(bool withForce);
        public void Fire();
        public void CanMove(bool flag);
        public void CallMove(EnumBind enumBind);

    }
}