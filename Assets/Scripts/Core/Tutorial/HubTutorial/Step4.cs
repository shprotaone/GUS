using GUS.Core.Tutorial;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core.Tutorial
{
    public class Step4 : MonoBehaviour, ITutorialStep
    {
        [SerializeField] private GameObject _showHand;
        [SerializeField] private Button _click;

        private TutorialSystemHUB _tutorialHub;
        public void Activate(TutorialSystemHUB tutorial)
        {
            _tutorialHub = tutorial;
            _showHand.SetActive(true);
            _click.interactable = true;
        }

        public void Deactivate()
        {
            _showHand.SetActive(false);
        }

        public void Next()
        {
            _tutorialHub.CallNextStep();
        }

        public void ShowText(string text)
        {
           
        }
    }
}

