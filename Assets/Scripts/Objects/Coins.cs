using GUS.Core.Pool;
using UnityEngine;

namespace GUS.Objects
{
    public class Coins : MonoBehaviour, IPoolObject
    {
        [SerializeField] private PoolObjectType _objectPoolType;
        [SerializeField] private Coin[] _coins;
        public PoolObjectType Type => _objectPoolType;

        private void OnEnable()
        {
            foreach(var coin in _coins)
            {
                if (!coin.gameObject.activeSelf)
                {
                    coin.gameObject.SetActive(true);
                }
            }
        }
    }
}

