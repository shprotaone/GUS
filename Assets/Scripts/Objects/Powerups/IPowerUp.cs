using GUS.Objects.PowerUps;
using UnityEngine;

public interface IPowerUp
{
    float Duration { get; }
    Sprite Sprite { get; }
    ParticleSystem Particle { get; }
    void Execute(PowerUpHandler handler);
}
