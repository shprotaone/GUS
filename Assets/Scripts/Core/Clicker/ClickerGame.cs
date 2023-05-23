using DG.Tweening;
using GUS.Core;
using GUS.Core.GameState;
using GUS.Objects.Enemies;
using GUS.Player;
using GUS.Player.Movement;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClickerGame : MonoBehaviour
{
    private event Action OnClickerEnd;

    [SerializeField] private BossSettings _settings;
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private bool _isDynamicClicker;
    [SerializeField] private Transform _bossTransform;
    [SerializeField] private Transform _runPos;
    [SerializeField] private Transform _endPos;

    private GameObject _enemyObj;
    private Wallet _wallet;
    private ClickerMovement _clickerMovement;
    
    private IEnemy _enemy;
    private float _hp;

    public float HP => _hp;
    public Transform RunPoint => _runPos;

    private void OnEnable()
    {
        if(_enemyObj == null)
        {
            _enemyObj = Instantiate(_settings.BossPrefab, _bossTransform);
        }
        _enemyObj.transform.SetParent(_bossTransform);
        _enemyObj.transform.position = _bossTransform.position;
    }

    public IEnumerator Init(PlayerActor actor)
    {
        _enemyObj.SetActive(true);
        actor.GameStateController.ClickerGame();
        OnClickerEnd += actor.GameStateController.StartGame;
        _wallet = actor.Wallet;
        SetEnemy(actor);
        SetHp();
       
        yield return new WaitForSeconds(0.5f);
        SetMovement(actor);

        yield return null;
    }

    private void SetHp()
    {
        _hp = _settings.MaxHP;
        _hpSlider.maxValue = _hp;
        _hpSlider.value = _hp;
    }

    private void SetEnemy(PlayerActor actor)
    {
        //TODO перебросить в фабрику
        _enemyObj.transform.SetParent(actor.BossPosition);

        _enemy = _enemyObj.GetComponent<IEnemy>();
        _enemy.Init(this);
    }

    private void SetMovement(PlayerActor actor)
    {
        if (actor.MovementType is ClickerMovement clickerMovement)
        {
            _clickerMovement = clickerMovement;
            _clickerMovement.OnClick += GetDamage;
        }
    }

    public void InitSlider(Slider slider)
    {
        _hpSlider = slider;
    }

    public void GetDamage()
    {
        _hp -= _settings.damage;
        RefreshSlider();
        _enemy.Behaviour(GetStage());

        if (_hp < 0)
        {        
            OnClickerEnd?.Invoke();
            _wallet.AddDistancePoint(_settings.reward);
            _clickerMovement.OnClick -= GetDamage;
            OnClickerEnd = null;
            _enemy.Death();
        }
    }

    private void RefreshSlider()
    {
        _hpSlider.value = _hp;
    }
    private EnemyStage GetStage()
    {
        if (_hp < _settings.stages[3]) return EnemyStage.LAST;
        else if (_hp < _settings.stages[2]) return EnemyStage.SECOND;
        else if (_hp < _settings.stages[1]) return EnemyStage.FIRST;
        else return EnemyStage.FULL;
    }


    public void Paused(bool flag)
    {
        if(_enemy!= null) _enemy.Paused(flag);
    }
}
