using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUS.Core.Weapon
{
    public class LaserWeapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private Transform _laserOrigin;
        [SerializeField] private float gunRange;
        [SerializeField] private float _duration;

        private Camera _camera;
        private LineRenderer _laserRenderer;

        private void Start()
        {
            _camera = Camera.main;
            _laserRenderer = GetComponentInChildren<LineRenderer>();
        }

        public void Fire()
        {
            _laserRenderer.SetPosition(0, _laserOrigin.position);
            Vector3 rayOrigin = _laserOrigin.position;
            Ray direction = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, direction.direction, out hit, gunRange))
            {
                _laserRenderer.SetPosition(1, hit.point);
                if (hit.transform.TryGetComponent<Destructable>(out Destructable destr))
                {
                    destr.Action();
                }
            }
            else
            {
                _laserRenderer.SetPosition(1, rayOrigin + (direction.direction * gunRange));
            }

            _laserRenderer.enabled = true;
        }

        public void UnFire()
        {
            _laserRenderer.enabled = false;
        }

    }
}

