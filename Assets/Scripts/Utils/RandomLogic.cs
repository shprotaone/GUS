using System.Collections.Generic;
using UnityEngine;

namespace GUS.Utils
{
    public class RandomLogic
    {
        private List<RandomPart> _parts;
        private int _digit;
        private int _totalWeight;

        public List<RandomPart> Parts => _parts;
        public RandomLogic(List<RandomPart> parts)
        {
            _parts = parts;
            SetMaxWeight();
        }

        public void SetMaxWeight()
        {
            foreach(var part in _parts)
            {
                _totalWeight += part.Weight;
            }
        }

        public int GetDigit()
        {
            _digit = Random.Range(0, _totalWeight);

            for (int i = 0; i < _parts.Count; i++)
            {
                if (_parts[i].Weight >= _digit) return i;
                _digit -= _parts[i].Weight;
            }

            Debug.LogAssertion("Не найден элемент");
            return 0;
        }
    }
}

