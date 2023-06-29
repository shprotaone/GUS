using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings")]
public class LevelSettings : ScriptableObject
{
    [Header("����������� ��������")]
    public float maxWorldSpeed;
    public float acceleration;
    [Header("���������� ����������")]   
    public float steerSpeed;
    public float distanceToMovement;
    public float downSlideTime;    
    public float gravityScale;
    public float jumpHeight;
    public float gravity;
    public float downSlideHeight;
    public float forceLandingPower;
    [Header("���������� � ����")]
    public float exploreSpeed;
    [Range(0f, 1f)]
    public float tractorSpeedMult;
    public int dryRange;
}
