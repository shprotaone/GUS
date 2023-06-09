﻿using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;

namespace GUS.Core.Tutorial
{
    public class UITutorial :MonoBehaviour
    {

        [SerializeField] private Canvas _tutorialCanvas;
        [SerializeField] private RectTransform _endPanel;
        [SerializeField] private RectTransform _upBlocker;
        [SerializeField] private TutorialStepView[] _steps;

        private TutorialStepView _currentViewStep;

        public TutorialStepView CurrentViewStep => _currentViewStep;
        public void Init()
        {
            _tutorialCanvas.gameObject.SetActive(true);
            _upBlocker.gameObject.SetActive(true);
            _tutorialCanvas.sortingOrder= 110;
        }

        public void CallStep(int index)
        {
            _currentViewStep = _steps[index];
            _currentViewStep.Enable();
        }

        public async void CallEndPanel()
        {
            _endPanel.DOAnchorPosX(-490, 1).SetEase(Ease.InCirc);
            await UniTask.Delay(3000);
            _endPanel.gameObject.SetActive(false);
            _upBlocker.gameObject.SetActive(false);
            //_tutorialCanvas.gameObject.SetActive(false);
        }
    }
}