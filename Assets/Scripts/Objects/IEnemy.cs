using GUS.Core.Clicker;
using UnityEngine;

namespace GUS.Objects.Enemies
{
    public interface IEnemy
    {
        bool IsAlive { get; }
        void Init(ClickerGame clicker);
        void Behaviour(EnemyStage stage);
        public void Move(bool flag, Vector3 pos);
        void MoveToDamage(bool flag, float time);
        void Death();
        void Paused(bool flag);
    }
}

