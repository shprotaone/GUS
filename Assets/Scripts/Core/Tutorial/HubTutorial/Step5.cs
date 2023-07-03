using Cysharp.Threading.Tasks;
using GUS.Core.InputSys.Joiystick;
using System.Threading;
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

        private CancellationTokenSource _cancellationTokenSource;
        private TutorialSystemHUB _tutorialSystem;
        private bool _flag = true;

        public async void Activate(TutorialSystemHUB tutorial)
        {
            _tutorialSystem = tutorial;
            _hideExit.gameObject.SetActive(false);
            _cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = _cancellationTokenSource.Token;
            _joystick.OnActive += Deactivate;

            try
            {
                await UniTask.Delay(1000,false, PlayerLoopTiming.Update, token);
                _showHand.SetActive(true);
            }
            catch
            {
                _showHand.SetActive(false);
            }
            
        }

        public void Deactivate()
        {
            if(_flag)
            {
                _flag = false;

                _showHand.SetActive(false);

                ShowHandDelay();

                foreach (GameObject go in _showHiddenObjects)
                {
                    go.SetActive(true);
                }
               
                _joystick.OnActive -= Deactivate;             
                _hideExit.onClick.AddListener(ExitTutorial);
            }                  
        }

        private async void ShowHandDelay()
        {
            CancellationToken token = _cancellationTokenSource.Token;
            try
            {
                await UniTask.Delay(5000,false,PlayerLoopTiming.Update,token);
                _showExitHand.SetActive(true);
            }
            catch
            {
                _showExitHand.SetActive(false);
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
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _showExitHand.SetActive(false);
        }
    }
}

