using DG.Tweening;
using GUS.Core;
using GUS.Core.GameState;
using GUS.Player;
using GUS.Player.Movement;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClickerGame : MonoBehaviour
{
    [SerializeField] private BossSettings _settings;
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private bool _isDynamicClicker;
    [SerializeField] private Transform _bossTransform;
    [SerializeField] private Transform _runPos;
    [SerializeField] private Transform _endPos;

    private GameStateController _controller;
    private GameObject _enemyObj;
    private Wallet _wallet;
    private ClickerState _clickerState;
    private ClickerMovement _clickerMovement;
    private float _hp;
    private IEnemy _enemy;
    private bool _canAttack;

    public float HP => _hp;
    public Transform RunPoint => _runPos;
    public Transform EndPoint => _endPos;

    private void OnEnable()
    {
        if(_enemyObj == null)
        {
            _enemyObj = Instantiate(_settings.BossPrefab, _bossTransform);
        }
        _enemyObj.transform.SetParent(_bossTransform);
    }

    public IEnumerator Init(PlayerActor actor)
    {
        
        _controller = actor.GameStateController;
        CreateEnemy();
        _wallet = actor.Wallet;
        IState state = _controller.GameStateMachine.CurrentState;

        if(state is ClickerState clicker)
        {
            _clickerState = clicker;
        }

        InitSlider();
      
        yield return new WaitForSeconds(0.5f);
        if (actor.MovementType is ClickerMovement clickerMovement)
        {
            _clickerMovement = clickerMovement;
            _clickerMovement.OnClick += GetDamage;
        }

        yield return null;
    }

    private void CreateEnemy()
    {
        //TODO перебросить в фабрику
        _enemyObj.transform.SetParent(_controller.ClickerBossPos);
        _enemyObj.transform.DOMove(_controller.ClickerBossPos.position, 1);
        
                   
        _enemy = _enemyObj.GetComponent<IEnemy>();
        _enemy.Init(this);
    }

    private void InitSlider()
    {
        _hpSlider = _clickerState.GetClickerSlider();
        _hp = _settings.MaxHP;
        _hpSlider.maxValue = _hp;
        _hpSlider.value = _hp;
    }

    public void GetDamage()
    {
        _hp -= _settings.damage;
        RefreshSlider();
        _enemy.Behaviour(_hp);

        if (_hp < 0)
        {
            _enemy.Death();
            _controller.StartGame();
            _wallet.AddDistancePoint(_settings.reward);
            _clickerMovement.OnClick -= GetDamage;
        }

    }

    private void RefreshSlider()
    {
        _hpSlider.value = _hp;
    }

}
