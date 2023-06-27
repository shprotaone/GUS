using GUS.Core.Tutorial;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core.Tutorial
{
    public class Step4 : MonoBehaviour, ITutorialStep
    {
        [SerializeField] private GameObject _showHand;
        [SerializeField] private GameObject _exitButton;
        [SerializeField] private Button _secondBuyButton;
        [SerializeField] private Button _click;

        private TutorialSystemHUB _tutorialHub;
        public void Activate(TutorialSystemHUB tutorial)
        {
            _tutorialHub = tutorial;
            _click.onClick.AddListener(Next);
            _showHand.SetActive(true);
            _click.interactable = true;
        }

        public void Deactivate()
        {
            _showHand.SetActive(false);
            _exitButton.SetActive(true);
            _secondBuyButton.interactable = true;
            _click.onClick.RemoveListener(Next);
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

