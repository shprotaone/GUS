using UnityEngine;

namespace GUS.Objects.PowerUps
{
    [CreateAssetMenu]
    public class Collectable : ScriptableObject
    {
        public string nameCollectable;
        public string descriptionCollectable;
        public PowerUpEnum powerUpEnum;
        public CoinType coinType;
        public GameObject model;
        public Sprite icon;
        public int[] costs;
        public int[] value;
    }
}

