using GUS.Core.Tutorial;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core.Tutorial
{
    public class Step1 : MonoBehaviour, ITutorialStep
    {
        [SerializeField] private GameObject[] _hideObjects;
        [SerializeField] private GameObject _show;
        private TutorialSystemHUB _tutorial;

        public void ShowText(string text)
        {

        }

        public void Activate(TutorialSystemHUB tutorial)
        {
            _tutorial = tutorial;
            _show.SetActive(true);
            foreach (var obj in _hideObjects)
            {
                obj.SetActive(false);
            }
        }

        public void Deactivate()
        {
            _show.SetActive(false);
        }

        public void Next()
        {
            _tutorial.CallNextStep();
        }
    }
}

