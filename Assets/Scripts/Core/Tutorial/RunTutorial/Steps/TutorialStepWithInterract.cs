using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core.Tutorial
{
    public class TutorialStepWithInterract : TutorialStepView
    {
        [SerializeField] private Button[] _disableButton;
        [SerializeField] private Button _completeButton;
        
        public override void Enable()
        {
            gameObject.SetActive(true);
            foreach (var button in _disableButton)
            {
                button.interactable = false;
            }
        }
    }
}

