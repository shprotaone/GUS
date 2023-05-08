using System.Collections;

namespace GUS.Core
{
    public interface IState
    {
        /// <summary>
        /// Вход в стейт
        /// </summary>
        void Enter();
        /// <summary>
        /// Исполнения входа с заданным порядком
        /// </summary>
        /// <returns></returns>
        IEnumerator Execute();
        /// <summary>
        /// Runtime
        /// </summary>
        void Update();
        /// <summary>
        /// Выход из стейта
        /// </summary>
        void FixedUpdate();
        void Exit();
    }
}