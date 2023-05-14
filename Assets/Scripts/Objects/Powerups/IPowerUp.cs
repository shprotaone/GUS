using GUS.Objects.PowerUps;
using UnityEngine;

public interface IPowerUp
{
    float Duration { get; }
    Sprite Sprite { get; }
    void Execute(PowerUpHandler handler);
}
