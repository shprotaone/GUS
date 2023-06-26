using GUS.Core.InputSys.Joiystick;
using UnityEngine;

namespace GUS.Core.Tutorial
{
    public class Step5 : MonoBehaviour, ITutorialStep
    {
        [SerializeField] private GameObject _showHand;
        [SerializeField] private FloatingJoystick _joystick;

        private TutorialSystemHUB _tutorialSystem;
        public void Activate(TutorialSystemHUB tutorial)
        {
            _tutorialSystem = tutorial;
            _joystick.OnActive += Deactivate;
            _showHand.SetActive(true);
        }

        public void Deactivate()
        {
            _joystick.OnActive -= Deactivate;
            _showHand.SetActive(false);
            _tutorialSystem.Complete();
        }

        public void Next()
        {
            throw new System.NotImplementedException();
        }

        public void ShowText(string text)
        {
            throw new System.NotImplementedException();
        }
    }
}

