using GUS.Core.Locator;
using GUS.Objects;
using System.Collections.Generic;
using UnityEngine;

namespace GUS.LevelBuild
{
    public class WorldController
    {
        private Transform _startPoint;

        private PlatformBuilder _platformBuilder;
        private float _maxSpeed;
        private float _acceleration;
        private float _currentSpeed;
        private bool _worldIsStopped;
        public WorldController(Transform startPoint, IServiceLocator serviceLocator)
        {
            LevelSettings settings = serviceLocator.Get<LevelSettings>();
            _platformBuilder = new PlatformBuilder(startPoint, serviceLocator);
            _maxSpeed = settings.maxWorldSpeed;
            _acceleration = settings.acceleration;
            _startPoint = startPoint;
        }

        public void InitStart()//TODO: Добавить свежие платформы в очередь
        {
            _startPoint.position = Vector3.zero;
            _platformBuilder.ClearBuilder();
            _platformBuilder.CreateStartSection();
        }

        public void Move()
        {
            if(!_worldIsStopped ) 
            {
                SpeedController();
                _platformBuilder.DeletePlatform();
                _platformBuilder.CreateNextPlatform();

                float step = _currentSpeed * Time.deltaTime;
                Vector3 target = _startPoint.position - Vector3.forward;
                _startPoint.position = Vector3.MoveTowards(_startPoint.position, target, step);
            }           
        }

        private void SpeedController()
        {
            if (_currentSpeed < _maxSpeed)
            {
                _currentSpeed += Time.deltaTime * _acceleration;
            }
            else
            {
                _currentSpeed -= Time.deltaTime * _acceleration;
            }
        }

        public void WorldStopper(bool flag)
        {
            _worldIsStopped = flag;
            _currentSpeed = 0;
        }
    }
}

