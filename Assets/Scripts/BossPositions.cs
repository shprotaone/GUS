using UnityEngine;

public class BossPositions : MonoBehaviour
{
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _runPosition;

    public Transform Start => _startPosition;
    public Transform Run => _runPosition;
}
