using UnityEngine;

namespace GUS.Core.Tutorial
{
    public class TutorialSystem : MonoBehaviour
    {
        [SerializeField] private Canvas _tutorialCanvas;

        private ITutorialStep[] _steps;
        private int _tutroialStep = 0;
        private bool _isActive;

        public bool IsActive => _isActive;
        public void Init(bool flag)
        {
            if (flag)
            {
                _tutorialCanvas?.gameObject.SetActive(true);
                _steps = GetComponentsInChildren<ITutorialStep>();
                _steps[0].Activate(this);
                _isActive = true;
            }
            else
            {
                _tutorialCanvas?.gameObject.SetActive(false);
            }
        }

        public void CallNextStep()
        {
            if (_isActive)
            {
                _steps[_tutroialStep].Deactivate();

                _tutroialStep++;
                _steps[_tutroialStep].Activate(this);
            }
        }

        public void Disable()
        {
            _isActive = false;
        }
    }
}


