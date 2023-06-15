using GUS.Core.Pool;
using GUS.Utils;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner
{
    private ObjectPool _pool;
    public BonusSpawner(ObjectPool pool)
    {
        _pool = pool;
    }
    public Transform GetPos(List<Transform> spawnPoints)
    {
        int index = Random.Range(0, spawnPoints.Count);
        if (spawnPoints[index].childCount > 0)
        {
            Debug.Log("Место уже занято");
            return null;
        }
        return spawnPoints[index];
    }

    public GameObject GetObject(ObjectInfo obj)
    {
        obj.prefab.TryGetComponent(out IPoolObject poolObject);
        return _pool.GetObject(poolObject.Type);
    }

    public ObjectInfo GetTypeBonus()
    {
        RandomLogic collectable = new RandomLogic(_pool);
        int index = collectable.GetDigit();
        return _pool.Storage.parts[index];
    }
}
