using GUS.Core.Pool;
using GUS.LevelBuild;
using GUS.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner
{
    public Vector3 GetPos(List<Transform> spawnPoints)
    {
        int index = Random.Range(0, spawnPoints.Count);
        return spawnPoints[index].position;
    }

    public ObjectInfo GetTypeBonus(PoolObjectStorage bonusPool)
    {
        RandomLogic collectable = new RandomLogic(bonusPool.parts);
        int index = collectable.GetDigit();
        return bonusPool.parts[index].objectInfo;
    }
}
