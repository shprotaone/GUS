using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitStopComponent : MonoBehaviour
{
    [SerializeField] private bool _activatePit;

    public bool ActivatePit => _activatePit;
}
