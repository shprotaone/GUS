using Cysharp.Threading.Tasks;
using GUS.Core.Data;
using GUS.Core.Locator;
using GUS.Core.SaveSystem;
using System;
using UnityEngine;

namespace GUS.Core.Tutorial
{
    public class TutorialSystemHUB : MonoBehaviour
    {
        [SerializeField] private GameObject _parent;
        [SerializeField] private Canvas _tutorialCanvas;

        private ITutorialStep[] _steps;
        private StorageService _storageService;
        private Wallet _wallet;
        private int _tutorialStep = 0;
        private bool _isActive;

        public bool IsActive => _isActive;
        public void Init(IServiceLocator serviceLocator)
        {
            _storageService = serviceLocator.Get<StorageService>();
            _wallet = serviceLocator.Get<Wallet>();
            _steps = GetComponentsInChildren<ITutorialStep>();

            if (!_storageService.Data._tutorialSteps[0])
            {
                _parent.SetActive(true);
                _tutorialCanvas?.gameObject.SetActive(true);              
                _steps[0].Activate(this);
                _storageService.Data._tutorialSteps[0] = true;
                _storageService.Save();
                _isActive = true;
            }
            else if (!_storageService.Data._tutorialSteps[2])
            {
                _tutorialStep = 1;
                if (_wallet.Coins < 100)
                {
                    _wallet.AddCoins(100);
                    _wallet.AddCoinsToData();
                }
                _parent.SetActive(true);
                _steps[1].Activate(this);
            }
            else
            {
                _tutorialCanvas?.gameObject.SetActive(false);
            }
        }

        public void CallNextStep()
        {
            _steps[_tutorialStep].Deactivate();

            _tutorialStep++;
            _steps[_tutorialStep].Activate(this);
        }

        public void Disable()
        {
            _isActive = false;
        }

        public void Complete()
        {
            _steps[_tutorialStep].Deactivate();
            _tutorialCanvas?.gameObject.SetActive(false);
            _storageService.Data._tutorialSteps[2] = true;
            _storageService.Save();
        }
    }
}


