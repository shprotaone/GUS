using GUS.Core;
using GUS.Core.GameState;
using GUS.Player;
using GUS.Player.Movement;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIClicker : MonoBehaviour
{
    [SerializeField] private BossSettings _settings;
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private Transform _enemyPosition;

    private GameStateController _controller;
    private Wallet _wallet;
    private ClickerState _clickerState;
    private ClickerMovement _clickerMovement;
    private float _hp;
    private IEnemy _enemy;
    private bool _canAttack;

    public float HP => _hp;

    public IEnumerator Init(PlayerActor actor)
    {
        _controller = actor.GameStateController;
        _wallet= actor.Wallet;
        IState state = _controller.GameStateMachine.CurrentState;

        if(state is ClickerState clicker)
        {
            _clickerState = clicker;     
        }

        InitSlider();
        CreateEnemy();

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
        GameObject enemy = Instantiate(_settings.BossPrefab, _enemyPosition);
        _enemy = enemy.GetComponent<IEnemy>();
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
