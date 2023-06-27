using Cysharp.Threading.Tasks;
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

        public async void Activate(TutorialSystemHUB tutorial)
        {
            _tutorial = tutorial;           
            _mainCanvas.sortingOrder = 100;
            _click.onClick.AddListener(Next);
            foreach(var obj in _hideObjects)
            {
                obj.SetActive(false);
            }

            await UniTask.Delay(2000);
            _showHand.SetActive(true);
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
