using GUS.Player;
using GUS.Player.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerActor player = other.GetComponent<PlayerActor>();
            if(player.MovementType is RunMovement run)
            {
                run.ReturnPosition();
            }
        }
    }
}
