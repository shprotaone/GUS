using DG.Tweening;
using GUS.Player;
using GUS.Player.Movement;
using UnityEngine;

public class ObstacleBack : MonoBehaviour
{
    private bool _isActive = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _isActive)
        {
            Debug.Log("Enter");
            PlayerActor player = other.GetComponent<PlayerActor>();
            if(player.MovementType is RunMovement run)
            {
                run.ReturnObstaclePosition();
                _isActive = false;
                DOVirtual.DelayedCall(0.2f, () => _isActive = true);
            }
        }
    }
}
