using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings")]
public class LevelSettings : ScriptableObject
{
    [Header("Перемещение платформ")]
    public float maxWorldSpeed;
    public float acceleration;
    [Header("Управление персонажем")]   
    public float steerSpeed;
    public float distanceToMovement;
    public float downSlideTime;    
    public float gravityScale;
    public float jumpHeight;
    public float gravity;
    public float downSlideHeight;
    public float forceLandingPower;
    [Header("Управление в хабе")]
    public float exploreSpeed;
}
