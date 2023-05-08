using GUS.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntrySecondLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerActor actor))
        {
            actor.SmoothSecondLevel(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerActor actor))
        {
            actor.SmoothSecondLevel(false);
        }
    }
}
