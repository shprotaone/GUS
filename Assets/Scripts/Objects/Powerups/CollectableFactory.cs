using GUS.Core;
using GUS.Core.Locator;
using GUS.Core.Pool;
using GUS.Core.SaveSystem;
using GUS.LevelBuild;
using GUS.Objects.PowerUps;
using System.Collections.Generic;
using UnityEngine;

public class CollectableFactory : MonoBehaviour
{
    private StorageService _storageService;
    public void Init(PoolObjectStorage storage,IServiceLocator serviceLocator)
    {
        List<ObjectInfo> list = storage.parts;
        _storageService = serviceLocator.Get<StorageService>();

        foreach(var collectable in list)
        {
            if(collectable.prefab.TryGetComponent(out IPowerUp powerUp))
            {
                GetBonusTime(powerUp);
            }
        }
    }

    private void GetBonusTime(IPowerUp powerUp)
    {
        if(powerUp is Magnet)
        {
            foreach (var item in _storageService.Data.bonusDatas)
            {
                if(item.powerUp is PowerUpEnum.Magnet)
                {
                    powerUp.SetUp(item.powerTime);
                    break;
                }
            }
        }
        else if(powerUp is Multiply)
        {
            foreach (var item in _storageService.Data.bonusDatas)
            {
                if (item.powerUp is PowerUpEnum.Multiply)
                {
                    powerUp.SetUp(item.powerTime);
                    break;
                }
            }
        }
    }
}
