using DG.Tweening;
using GUS.Core.Locator;
using System;
using UnityEngine;

namespace GUS.LevelBuild
{
    public class WorldController
    {
        private Transform _startPoint;

        private LevelSettings _standartSettings;
        private PlatformBuilder _platformBuilder;
        private float _maxSpeed;
        private float _acceleration;
        private float _currentSpeed;
        private bool _worldIsStopped;

        public float CurrentDistance { get; private set; }

        public PlatformBuilder PlatformBuilder => _platformBuilder;

        public void Init(Transform startPoint, IServiceLocator serviceLocator)
        {
            _standartSettings = serviceLocator.Get<LevelSettings>();
            _platformBuilder = new PlatformBuilder(startPoint, serviceLocator);
            _startPoint = startPoint;
        }

        public void InitStart()
        {
            _maxSpeed = _standartSettings.maxWorldSpeed;
            _currentSpeed = 0;
            _acceleration = _standartSettings.acceleration;
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
                CurrentDistance = _startPoint.position.z;
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
            if(_worldIsStopped)
            {
                _worldIsStopped = flag;
                _currentSpeed = 0;
            }  
        }

        public void CreateOnlyFreePlatforms(bool flag)
        {
            _platformBuilder.FreePlatformsMode(flag);
        }

        public void UpdateSettings(LevelSettings settings)
        {
            _maxSpeed = settings.maxWorldSpeed;
            _acceleration = settings.acceleration;
        }

        public void ChangeAcceleration(float delay)
        {
            float prevSpeed = _currentSpeed;
            _currentSpeed = prevSpeed / 2;
        }
    }
}

