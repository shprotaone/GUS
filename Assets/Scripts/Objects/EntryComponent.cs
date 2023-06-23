using GUS.Player;
using UnityEngine;
using System;
using System.Collections.Generic;

public class EntryComponent : MonoBehaviour
{
    [SerializeField] private List<GameObject> _dynObstacles;
    [SerializeField] private bool _isActive;

    private List<IDynamicObstacle> _obstacles;

    private void OnEnable()
    {
        if(_obstacles == null)
        {
            _obstacles = new List<IDynamicObstacle>();

            for (int i = 0; i < _dynObstacles.Count; i++)
            {
                _obstacles.Add(_dynObstacles[i].GetComponent<IDynamicObstacle>());
            }
        }
        _isActive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out PlayerActor actor) && _isActive)
        {
            float multiply = actor.ServiceLocator.Get<LevelSettings>().tractorSpeedMult;

            foreach (var obstacle in _obstacles)
            {
                obstacle.Init(actor.WorldController.CurrentSpeed,multiply);
                obstacle.Move();
            }

            _isActive = false;
        }
    }
}
