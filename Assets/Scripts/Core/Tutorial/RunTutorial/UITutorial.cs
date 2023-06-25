using System;
using UnityEngine;

namespace GUS.Core.Tutorial
{
    public class UITutorial :MonoBehaviour
    {
        public event Action OnWaiter;

        [SerializeField] private Canvas _tutorialCanvas;
        [SerializeField] private TutorialStepView[] _steps;

        private TutorialStepView _currentViewStep;

        public TutorialStepView CurrentViewStep => _currentViewStep;
        public void Init()
        {
            _tutorialCanvas.gameObject.SetActive(true);
        }

        public void CallStep(int index)
        {
            _currentViewStep = _steps[index];
            _currentViewStep.Enable();
        }

        private void Update()
        {
            OnWaiter?.Invoke();
        }
    }
}