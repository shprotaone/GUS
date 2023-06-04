using GUS.Core.Locator;
using GUS.Core.Pool;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GUS.Core.Clicker
{
    public class ClickerEndAnimationController
    {
        private List<GameObject> _coins;
        private ObjectPool _pool;

        public ClickerEndAnimationController(IServiceLocator serviceLocator) 
        {
                List<ObjectPool> pools = serviceLocator.GetAll<ObjectPool>().ToList();
                _pool = pools[1];

            _coins = new List<GameObject>();

            for (int i = 0; i < 10; i++)
            {
                GameObject coin = _pool.GetObject(PoolObjectType.CoinUI);
                _coins.Add(coin);
            }
        }

        public List<GameObject> GetCoins()
        {
            return _coins;
        }
    }
}