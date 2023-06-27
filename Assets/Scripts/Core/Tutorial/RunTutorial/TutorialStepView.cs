using System;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core.Tutorial
{
    public class TutorialStepView : MonoBehaviour
    {
        [SerializeField] private Image _arrowImage;
        [SerializeField] private Animator _animator;
        [SerializeField] private Animator _handAnimator;

        public virtual void Enable()
        {
            gameObject.SetActive(true);
            _animator.enabled = true;
            _handAnimator.enabled = true;
        }

        public virtual void Disable()
        {
            gameObject.SetActive(false);
            _handAnimator.enabled = false;
            _animator.enabled= false;
        }

    }
}