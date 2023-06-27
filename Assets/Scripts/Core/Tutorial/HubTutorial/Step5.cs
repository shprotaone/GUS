using Cysharp.Threading.Tasks;
using GUS.Core.InputSys.Joiystick;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core.Tutorial
{
    public class Step5 : MonoBehaviour, ITutorialStep
    {
        [SerializeField] private Button _hideExit;
        [SerializeField] private GameObject[] _showHiddenObjects;
        [SerializeField] private GameObject _showHand;
        [SerializeField] private GameObject _showExitHand;
        [SerializeField] private FloatingJoystick _joystick;

        private TutorialSystemHUB _tutorialSystem;
        private bool _flag = true;
        public async void Activate(TutorialSystemHUB tutorial)
        {
            _tutorialSystem = tutorial;
            _hideExit.gameObject.SetActive(false);
            _joystick.OnActive += Deactivate;

            await UniTask.Delay(1000);
            _showHand.SetActive(true);
        }

        public async void Deactivate()
        {
            if(_flag)
            {
                _flag = false;

                _showHand.SetActive(false);

                foreach (GameObject go in _showHiddenObjects)
                {
                    go.SetActive(true);
                }
               
                _joystick.OnActive -= Deactivate;

                await UniTask.Delay(5000);
                _showExitHand.SetActive(true);

                _hideExit.onClick.AddListener(ExitTutorial);
            }                  
        }

        public void Next()
        {
           
        }

        public void ShowText(string text)
        {
            
        }

        private void ExitTutorial()
        {
            _tutorialSystem.Complete();
            _showExitHand.SetActive(false);
        }
    }
}

