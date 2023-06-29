using GUS.Core.Pool;
using System.Collections.Generic;

public class BonusSpawnCatcher
{
    private Dictionary<PowerUpEnum, int> _twiceBlock;
    private Dictionary<PowerUpEnum, int> _dryBlock;
    private int _spawnedCount;

    public int Coins { get; private set; }
    public int Magnets { get; private set; }
    public int Multiply { get; private set; }
    public int Common { get; private set; }
    public int DryCount { get; set; }
    public int DryRange { get; private set; }

    public BonusSpawnCatcher() 
    {
        //_twiceBlock = new Dictionary<PowerUpEnum, int>();
        //_dryBlock= new Dictionary<PowerUpEnum, int>();   
        DryCount= 0;
        //_dryBlock.Add(PowerUpEnum.Magnet, 0);
        //_dryBlock.Add(PowerUpEnum.Multiply, 0);
        //_dryBlock.Add(PowerUpEnum.Empty, 0);

        //Singleton<BonusSpawnViewer>.Instance.Init(this); для тестов
    }

    public void SetDryRange(int range)
    {
        DryRange = range;
    }

    public void Reset()
    {
        DryCount = 0;
    }

    public void CatchBonus(PoolObjectType obj)
    {
        if(obj == PoolObjectType.Magnet)
        {
            //_dryBlock[PowerUpEnum.Magnet] += 1;
            //Magnets = _dryBlock[PowerUpEnum.Magnet];
            DryCount= 0;
        }
        else if(obj == PoolObjectType.Multiply)
        {
            //_dryBlock[PowerUpEnum.Multiply] += 1;
            //Multiply = _dryBlock[PowerUpEnum.Multiply];
            DryCount= 0;
        }
        //else if(obj == PoolObjectType.Coin)
        //{
        //    _dryBlock[PowerUpEnum.Empty] += 1;
        //    Coins = _dryBlock[PowerUpEnum.Empty];
        //}

        Common++;      
    }
}
