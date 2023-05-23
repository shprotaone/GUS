using UnityEngine;

[CreateAssetMenu]
public class BossSettings : ScriptableObject
{
    public float MaxHP;
    public GameObject BossPrefab;
    public float damage;
    public int reward;
    public int[] stages;
}