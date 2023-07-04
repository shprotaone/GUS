using GUS.Core.Pool;
using GUS.Utils;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner
{
    private ObjectPool _pool;
    private BonusSpawnCatcher _spawnCatcher;

    private int _spawnCount = 0;
    public BonusSpawner(ObjectPool pool,BonusSpawnCatcher catcher)
    {
        _pool = pool;
        _spawnCatcher = catcher;
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
        return _pool.GetObject(poolObject.Type);
    }

    public ObjectInfo GetTypeBonus()
    {
        RandomLogic collectable = new RandomLogic(_pool);
        _spawnCatcher.DryCount++;
        if (_spawnCatcher.DryCount > _spawnCatcher.DryRange)
        {
            _spawnCount = 0;
            Debug.Log("Принудительно");

            return Reposition();
        }

        int index = collectable.GetDigit();
        _spawnCatcher.CatchBonus(_pool.ObjectsInfo[index].ObjectType);
        return _pool.ObjectsInfo[index];
    }

    private ObjectInfo Reposition()
    {
        int result = Random.Range(0,2);
        if(result == 0)
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
        ObjectInfo obj;
        if (poolObject == PoolObjectType.Multiply)
            obj= _pool.ObjectsInfo[1];
        else if (poolObject == PoolObjectType.Magnet)
            obj = _pool.ObjectsInfo[0];
        else if (poolObject == PoolObjectType.HonkCoin)
            obj = _pool.ObjectsInfo[4];

        else obj = _pool.ObjectsInfo[3];

        _spawnCatcher.CatchBonus(poolObject);
        return obj;
    }
}
