using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings")]
public class LevelSettings : ScriptableObject
{
    [Header("����������� ��������")]
    public float maxWorldSpeed;
    public float acceleration;
    [Header("���������� ����������")]
    public float exploreSpeed;
    public float steerSpeed;
    public float distanceToMovement;
    public float downSlideTime;    
    public float gravityScale;
    public float jumpHeight;
    public float gravity;
}
