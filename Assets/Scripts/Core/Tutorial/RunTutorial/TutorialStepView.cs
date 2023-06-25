using System;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core.Tutorial
{
    public class TutorialStepView : MonoBehaviour
    {
        [SerializeField] private Image _arrowImage;
        public void Enable()
        {
            _arrowImage.gameObject.SetActive(true);
        }

        public void Disable()
        {
            _arrowImage.gameObject.SetActive(false);
        }

    }
}