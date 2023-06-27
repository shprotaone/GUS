using Cysharp.Threading.Tasks;
using GUS.Core.Tutorial;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core.Tutorial
{
    public class Step1 : MonoBehaviour, ITutorialStep
    {
        [SerializeField] private GameObject[] _hideObjects;
        [SerializeField] private GameObject _show;
        [SerializeField] private Button _startButon;
        private TutorialSystemHUB _tutorial;

        public void ShowText(string text)
        {

        }

        public async void Activate(TutorialSystemHUB tutorial)
        {
            _tutorial = tutorial;
            _startButon.onClick.AddListener(Deactivate);
            foreach (var obj in _hideObjects)
            {
                obj.SetActive(false);
            }
            await UniTask.Delay(2000);
            _show.SetActive(true);
        }

        public void Deactivate()
        {
            _show.SetActive(false);
            _startButon.onClick.RemoveListener(Deactivate);
        }

        public void Next()
        {
            _tutorial.CallNextStep();
        }
    }
}

