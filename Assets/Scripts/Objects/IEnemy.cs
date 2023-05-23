namespace GUS.Objects.Enemies
{
    public interface IEnemy
    {
        bool IsAlive { get; }
        void Init(ClickerGame clicker);
        void Behaviour(EnemyStage stage);
        void Death();
        void Paused(bool flag);
    }
}

