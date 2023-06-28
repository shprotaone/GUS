using GUS.Core.Pool;
using GUS.Utils;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner
{
    private ObjectPool _pool;
    private BonusSpawnCatcher _spawnCatcher;

    public BonusSpawner(ObjectPool pool)
    {
        _pool = pool;
        _spawnCatcher = new BonusSpawnCatcher();
    }
    public Transform GetPos(List<Transform> spawnPoints)
    {
        int index = Random.Range(0, spawnPoints.Count);
        if (spawnPoints[index].childCount > 0)
        {
            return null;
        }
        return spawnPoints[index];
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
        int index = collectable.GetDigit();
        _spawnCatcher.CatchBonus(_pool.Storage.parts[index].ObjectType);
        return _pool.Storage.parts[index];
    }
}
