using GUS.Core;
using GUS.Core.Locator;
using GUS.Core.Pool;
using GUS.Core.SaveSystem;
using GUS.Objects.PowerUps;
using UnityEngine;

public class CollectableFactory : MonoBehaviour
{
    private ObjectPool _pool;
    private StorageService _storageService;
    public void Init(IServiceLocator serviceLocator)
    {
        _pool = serviceLocator.Get<RunLocator>()
                .GetPool(PoolTypeEnum.Collectable);

        foreach(var collectable in _pool.ObjectsInfo)
        {
            if(collectable.prefab.TryGetComponent(out IPowerUp powerUp))
            {
                if (powerUp is Magnet) powerUp.SetUp(5);
                if (powerUp is Multiply) powerUp.SetUp(6);
            }
        }
    }
}
