using GUS.Player;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerActor player))
        {
            player.Death();
        }
    }
}

