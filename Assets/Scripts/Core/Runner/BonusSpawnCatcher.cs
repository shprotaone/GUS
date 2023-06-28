using GUS.Core.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawnCatcher
{
    private Dictionary<PowerUpEnum, int> _twiceBlock;
    private Dictionary<PowerUpEnum, int> _dryBlock;
    private int _spawnedCount;

    public int Coins { get; private set; }
    public int Magnets { get; private set; }
    public int Multiply { get; private set; }
    public int Common { get; private set; }

    public BonusSpawnCatcher() 
    {
        _twiceBlock = new Dictionary<PowerUpEnum, int>();
        _dryBlock= new Dictionary<PowerUpEnum, int>();   
        
        _dryBlock.Add(PowerUpEnum.Magnet, 0);
        _dryBlock.Add(PowerUpEnum.Multiply, 0);
        _dryBlock.Add(PowerUpEnum.Empty, 0);

        Singleton<BonusSpawnViewer>.Instance.Init(this);
    }

    public void CatchBonus(PoolObjectType powerUp)
    {
        if(powerUp == PoolObjectType.Magnet)
        {
            _dryBlock[PowerUpEnum.Magnet] += 1;
            Magnets = _dryBlock[PowerUpEnum.Magnet];
        }
        else if(powerUp == PoolObjectType.Multiply)
        {
            _dryBlock[PowerUpEnum.Multiply] += 1;
            Multiply = _dryBlock[PowerUpEnum.Multiply];
        }
        else if(powerUp == PoolObjectType.Coin)
        {
            _dryBlock[PowerUpEnum.Empty] += 1;
            Coins = _dryBlock[PowerUpEnum.Empty];
        }

        Common++;      
    }

    public void CatchSpawn()
    {
        //Common++;
    }
}
