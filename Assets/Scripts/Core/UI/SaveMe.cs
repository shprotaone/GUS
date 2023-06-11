using GUS.Core.Locator;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core.UI
{
    public class SaveMe : MonoBehaviour
    {
        [SerializeField] private Transform _panel;
        [SerializeField] private Button _honkReviveButton;
        [SerializeField] private Button _adsReviveButton;
        [SerializeField] private TMP_Text _timerText;

        [SerializeField] private int _timer = 5;

        private GameStateController _gameStateController;
        private int _currentTime;
        private bool _isActive = true;
        private void Start()
        {
            _honkReviveButton.onClick.AddListener(HonkRevive);
            _adsReviveButton.onClick.AddListener(AdsRevive);
        }

        public void Init(IServiceLocator serviceLocator)
        {
            _gameStateController = serviceLocator.Get<GameStateController>();
        }

        public void Execute()
        {
            if(_isActive) 
            {
                _panel.gameObject.SetActive(true);
                StartCoroutine(TimerCoroutine());
            }            
        }

        private IEnumerator TimerCoroutine()
        {
            _currentTime = _timer;
            _isActive = false;
            while (_currentTime > 0)
            {
                _timerText.text = _currentTime.ToString();
                _currentTime -= 1;
                yield return new WaitForSeconds(1);
            }
            _panel.gameObject.SetActive(false);
            _gameStateController.Result();
            _isActive = true;
            yield break; 
        }

        public void HonkRevive()
        {
            Debug.Log("Restart");
            _panel.gameObject.SetActive(false);
            _gameStateController.SecondChanceGame();
            StopAllCoroutines();
            _isActive = true;
        }

        public void AdsRevive() 
        {
            Debug.Log("ADS");
            _panel.gameObject.SetActive(false);
            _gameStateController.SecondChanceGame();
            StopAllCoroutines();
            _isActive = true;
        }

        public void DecreaseTime()
        {
            if(_currentTime > 0)
            {
                _currentTime--;
                _timerText.text = _currentTime.ToString();
            }         
        }
    }
}

