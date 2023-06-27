using GUS.Core.InputSys.Joiystick;
using UnityEngine;

namespace GUS.Core.Tutorial
{
    public class Step5 : MonoBehaviour, ITutorialStep
    {
        [SerializeField] private GameObject _hideExit;
        [SerializeField] private GameObject[] _showHiddenObjects;
        [SerializeField] private GameObject _showHand;
        [SerializeField] private FloatingJoystick _joystick;

        private TutorialSystemHUB _tutorialSystem;
        private bool _flag = true;
        public void Activate(TutorialSystemHUB tutorial)
        {
            _tutorialSystem = tutorial;
            _hideExit.SetActive(false);
            _joystick.OnActive += Deactivate;
            _showHand.SetActive(true);
        }

        public void Deactivate()
        {
            if(_flag)
            {
                _flag = false;

                _showHand.SetActive(false);

                foreach (GameObject go in _showHiddenObjects)
                {
                    go.SetActive(true);
                }

                _tutorialSystem.Complete();
                _joystick.OnActive -= Deactivate;
            }                  
        }

        public void Next()
        {
           
        }

        public void ShowText(string text)
        {
            
        }
    }
}

