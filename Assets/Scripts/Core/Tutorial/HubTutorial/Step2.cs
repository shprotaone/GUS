using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core.Tutorial
{
    public class Step2 : MonoBehaviour,ITutorialStep
{
        [SerializeField] private GameObject[] _hideObjects;
        [SerializeField] private GameObject _showHand;
        [SerializeField] private Button _click;
        [SerializeField] private Canvas _mainCanvas;

        private TutorialSystemHUB _tutorial;
        private CancellationTokenSource _cancellationTokenSource;
        public void Activate(TutorialSystemHUB tutorial)
        {
            _tutorial = tutorial;           
            _mainCanvas.sortingOrder = 100;
            
            _click.onClick.AddListener(Next);
            foreach(var obj in _hideObjects)
            {
                obj.SetActive(false);
            }

            ShowHandDelay();
        }

        private async void ShowHandDelay()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = _cancellationTokenSource.Token;

            try
            {
                await UniTask.Delay(2000, false, PlayerLoopTiming.Update, token);
                _showHand.SetActive(true);
            }
            catch
            {
                _showHand.SetActive(false);
            }
        }

        public void Deactivate()
        {
            _showHand.SetActive(false);
        }

        public void Next()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _click.onClick.RemoveListener(Next);
            _tutorial.CallNextStep();
        }

        public void ShowText(string text)
        {
            
        }
    }
}

