using GUS.Core.Locator;
using GUS.LevelBuild;
using GUS.Player;
using GUS.Player.Movement;
using GUS.Player.State;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progressive : MonoBehaviour
{
    [SerializeField] private List<ProgressivePart> _parts;
    
    private WorldController _worldController;
    private LevelSettings _currentLevelSettings;
    private PlayerStateMachine _playerStateMachine;
    private int _stageCount = 0;
    
    public void Init(IServiceLocator locator)
    {
        _worldController = locator.Get<WorldController>();
        _playerStateMachine = locator.Get<PlayerStateMachine>();
        _worldController.PlatformBuilder.OnPlatformAdded += CheckStage;
    }

    public void CheckStage(int value)
    {
        if (_stageCount < _parts.Count && _parts[_stageCount].stageCount < value)
        {
            _currentLevelSettings = _parts[_stageCount].settings;
            SetNewSettings();
            _stageCount++;
        }
    }

    private void SetNewSettings()
    {
        _worldController.UpdateSettings(_currentLevelSettings);
        _playerStateMachine.UpdateSettings(_currentLevelSettings);
    }

    private void OnDisable()
    {
        _worldController.PlatformBuilder.OnPlatformAdded -= CheckStage;
    }
}
