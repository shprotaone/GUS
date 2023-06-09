using DG.Tweening;
using GUS.Core.Data;
using GUS.Core.Locator;
using GUS.Core.UI;
using GUS.Objects.Enemies;
using GUS.Player;
using GUS.Player.Movement;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace GUS.Core.Clicker
{
    public class ClickerGame
    {
        public event Action OnRestart;
        private UIController _uiController;
        private GameStateController _gameStateController;
        private Wallet _wallet;
        private ClickerStateMachine _clickerStateMachine;
        private PlayerActor _actor;
        private IServiceLocator _serviceLocator;
        private ClickerMovement _movement;

        private BossSettings _settings;
        private BossPositions _positions;
        private IEnemy _enemy;
        private float _hp;
        private int _stageIndex;

        public bool IsActive { get; private set; }
        public float HP => _hp;
        public IEnemy Enemy => _enemy;
        public BossSettings Settings => _settings;
        public ClickerStateMachine StateMachine => _clickerStateMachine;

        public void Init(IServiceLocator serviceLocator)
        {
            _positions = serviceLocator.Get<BossPositions>();
            _uiController = serviceLocator.Get<UIController>();
            _gameStateController = serviceLocator.Get<GameStateController>();
            _wallet = serviceLocator.Get<Wallet>();
            _actor = serviceLocator.Get<PlayerActor>();
            _serviceLocator = serviceLocator;
        }

        public IEnumerator Init(BossSettings settings,GameObject enemy)
        {
            Debug.Log("������������� ClickerGame");

            IsActive = true;
            _settings = settings;
            _uiController.ClickerGame.ResetClickerUI();
            _clickerStateMachine = new ClickerStateMachine(_serviceLocator);
            _gameStateController.ClickerGame();

            SetEnemy(enemy);
            _uiController.ClickerGame.InitSlider(settings.MaxHP);
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
            DisablePanel();
            _hp -= _settings.damage;
            _uiController.ClickerGame.UpdateSlider(_settings.damage);
            _enemy.Behaviour(GetStage());

            if (_hp < 0)
            {
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
                //_clickerStateMachine.TransitionTo(_clickerStateMachine.prepareState);
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
            _uiController.ClickerGame.PanelActivate(false);
            //_uiController.ClickerGame.SliderActivate(false);
            StateMachine.CurrentState.Exit();
            OnRestart = null;
        }

        public void Complete()
        {
            IsActive = false;
        }

        private void DisablePanel()
        {
            _uiController.ClickerGame.TutorialPanel(false);
        }
    }

}
