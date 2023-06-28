using GUS.Core.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawnCatcher
{
    private Dictionary<PowerUpEnum, int> _twiceBlock;
    private Dictionary<PowerUpEnum, int> _dryBlock;
    private int _spawnedCount;

    public BonusSpawnCatcher() 
    {
        _twiceBlock = new Dictionary<PowerUpEnum, int>();
        _dryBlock= new Dictionary<PowerUpEnum, int>();   
        
        _dryBlock.Add(PowerUpEnum.Magnet, 0);
        _dryBlock.Add(PowerUpEnum.Multiply, 0);
    }

    public void CatchBonus(PoolObjectType powerUp)
    {
        if(powerUp == PoolObjectType.Magnet)
        {
            _dryBlock[PowerUpEnum.Magnet] += _spawnedCount++;
        }
        else if(powerUp == PoolObjectType.Multiply)
        {
            _dryBlock[PowerUpEnum.Multiply] += _spawnedCount++;
        }

        _spawnedCount++;      
    }

    public void CatchSpawn()
    {
        _spawnedCount++;
    }
}
