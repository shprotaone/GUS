using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonusSpawnViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinSpawn;
    [SerializeField] private TMP_Text _magnetSpawn;
    [SerializeField] private TMP_Text _multipleSpawn;
    [SerializeField] private TMP_Text _common;

    private BonusSpawnCatcher _catcher;
    public void Init(BonusSpawnCatcher bonusSpawnCatcher)
    {
        _catcher = bonusSpawnCatcher;
    }

    private void Update()
    {
        _coinSpawn.text = _catcher.Coins.ToString();
        _magnetSpawn.text = _catcher.Magnets.ToString();
        _multipleSpawn.text = _catcher.Multiply.ToString();
        _common.text = _catcher.Common.ToString();

    }
}
