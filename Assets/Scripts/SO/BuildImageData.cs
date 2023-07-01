using UnityEngine;

namespace GUS.Core.Hub.BuildShop
{
    [CreateAssetMenu]
    public class BuildImageData : ScriptableObject
    {
        [SerializeField] private Sprite[] _disableImages;
        [SerializeField] private Sprite[] _enableImages;

        public Sprite[] DisableImages => _disableImages;
        public Sprite[] EnableImages => _enableImages;
    }
}

