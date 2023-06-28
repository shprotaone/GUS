using GUS.Core.Pool;
using GUS.Utils;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner
{
    private ObjectPool _pool;
    private BonusSpawnCatcher _spawnCatcher;

    private int _spawnCount = 0;
    public BonusSpawner(ObjectPool pool)
    {
        _pool = pool;
        _spawnCatcher = new BonusSpawnCatcher();
    }

    public Transform GetPos(List<Transform> spawnPoints)
    {
        Transform point;

        do
        {
            int index = Random.Range(0, spawnPoints.Count);

            if (spawnPoints[index].childCount > 0)
            {
                point = null;
            }
            else
            {
                point = spawnPoints[index];
            }
        }
        while(point == null);

        return point;
    }

    public GameObject GetObject(ObjectInfo obj)
    {
        obj.prefab.TryGetComponent(out IPoolObject poolObject);
        _spawnCatcher.CatchSpawn();
        return _pool.GetObject(poolObject.Type);
    }

    public ObjectInfo GetTypeBonus()
    {
        RandomLogic collectable = new RandomLogic(_pool);
        _spawnCount++;
        //if(_spawnCount > 10)
        //{
        //    _spawnCount= 0;
        //    Debug.Log("Принудительно");
        //    return Reposition();
        //}

        int index = collectable.GetDigit();
        _spawnCatcher.CatchBonus(_pool.ObjectsInfo[index].ObjectType);

        return _pool.Storage.parts[index];
    }

    private ObjectInfo Reposition()
    {
        if(_spawnCatcher.Common - _spawnCatcher.Multiply > 5)
        {
            return GetBonus(PoolObjectType.Multiply);
        }
        else
        {
            return GetBonus(PoolObjectType.Magnet);
        }
    }

    public ObjectInfo GetBonus(PoolObjectType poolObject)
    {
        if (poolObject == PoolObjectType.Multiply)
            return _pool.Storage.parts[2];
        else if (poolObject == PoolObjectType.Magnet)
            return _pool.Storage.parts[1];
        else if (poolObject == PoolObjectType.HonkCoin)
            return _pool.Storage.parts[4];

        else return _pool.Storage.parts[3];
    }
}
