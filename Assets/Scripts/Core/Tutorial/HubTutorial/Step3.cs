using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core.Tutorial
{
    public class Step3 : MonoBehaviour, ITutorialStep
    {
        [SerializeField] private GameObject _showHand;
        [SerializeField] private Button[] _hideButtons;
        [SerializeField] private Button _click;
        private TutorialSystemHUB _tutorial;
        public void Activate(TutorialSystemHUB tutorial)
        {
            _tutorial = tutorial;
            _showHand.SetActive(true);
            _click.onClick.AddListener(Next);
           foreach(var button in _hideButtons)
            {
                button.interactable = false;
            }
        }

        public void Deactivate()
        {
            _showHand.SetActive(false);
        }

        public void Next()
        {
            _click.onClick.RemoveListener(Next);
            _tutorial.CallNextStep();
        }

        public void ShowText(string text)
        {
           
        }
    }
}

