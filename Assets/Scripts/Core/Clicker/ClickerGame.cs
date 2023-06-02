using DG.Tweening;
using GUS.Core.Data;
using GUS.Core.Locator;
using GUS.Core.UI;
using GUS.Objects.Enemies;
using GUS.Player;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace GUS.Core.Clicker
{
    public class ClickerGame
    {
        public event Action OnRestart;
        private UIClickerGame _clickerUI;
        private GameStateController _gameStateController;
        private Wallet _wallet;
        private ClickerStateMachine _clickerStateMachine;
        private IServiceLocator _serviceLocator;

        private BossSettings _settings;
        private BossPositions _positions;
        private IEnemy _enemy;
        private float _hp;
        private int _stageIndex;

        public float HP => _hp;
        public IEnemy Enemy => _enemy;
        public BossSettings Settings => _settings;
        public ClickerStateMachine StateMachine => _clickerStateMachine;

        public void Init(IServiceLocator serviceLocator)
        {
            _positions = serviceLocator.Get<BossPositions>();
            _clickerUI = serviceLocator.Get<UIController>().ClickerGame;
            _gameStateController = serviceLocator.Get<GameStateController>();
            _wallet = serviceLocator.Get<Wallet>();
            _serviceLocator = serviceLocator;
        }

        public IEnumerator Init(BossSettings settings,GameObject enemy)
        {
            _settings = settings;
            _clickerStateMachine = new ClickerStateMachine(_serviceLocator);
            _gameStateController.ClickerGame();
            
            //yield return new WaitForSeconds(0.2f);
            SetEnemy(enemy);
            _clickerUI.InitSlider(settings.MaxHP);
            _hp = settings.MaxHP;

            _stageIndex = 0;
            _clickerStateMachine.InitGameLoop(_clickerStateMachine.prepareState);

            yield return null;
        }

        public void SetStateMachine(ClickerStateMachine stateMachine)
        {
            _clickerStateMachine = stateMachine;
        }

        private void SetEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(_positions.Start);
            enemy.transform.localPosition = Vector3.zero;

            _enemy = enemy.GetComponent<IEnemy>();
            _enemy.Init(this);
            _enemy.Move(true, _positions.Run.position);     
        }


        public void GetDamage()
        {
            _hp -= _settings.damage;
            _clickerUI.UpdateSlider(_settings.damage);
            _enemy.Behaviour(GetStage());

            if (_hp < 0)
            {
                _wallet.AddCoins(_settings.reward);
                _clickerStateMachine.TransitionTo(_clickerStateMachine.endState);
                _enemy.Death();
            }
        }

        private EnemyStage GetStage()
        {
            if (_stageIndex >= _settings.stages.Length) return EnemyStage.FULL;

            float stageHp = _settings.stages[_stageIndex];
            if (_hp < stageHp)
            {
                 _stageIndex++;
                _clickerStateMachine.TransitionTo(_clickerStateMachine.prepareState);
                return (EnemyStage)_stageIndex;
            }
            else { return EnemyStage.FULL; }
        }

        public void Paused(bool flag)
        {
            if (_enemy != null) _enemy.Paused(flag);
        }   
        
        public void Restart()
        {
            OnRestart?.Invoke();
            _clickerUI.DisableSlider();
            StateMachine.CurrentState.Exit();
            OnRestart = null;
        }
    }

}
