using TMPro;
using UnityEngine;

namespace GUS.Core.UI
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _value;

        public void Set(string name, string value)
        {
            _name.text = name;
            _value.text = value;
        }
    }
}

