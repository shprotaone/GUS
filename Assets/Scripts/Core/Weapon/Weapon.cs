using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GUS.Core.Weapon
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        // Start is called before the first frame update
        public void Fire()
        {
            Debug.Log("Fire");
        }

        public void UnFire()
        {
            throw new System.NotImplementedException();
        }
    }
}

