using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core.UI
{
    public class BonusSlotView : MonoBehaviour,IPaused
    {
        [SerializeField] private Image _bonusImage;
        [SerializeField] private Slider _bonusSlider;
        [SerializeField] private float _sliderStep;

        private PowerUpEnum _type = PowerUpEnum.Empty;
        private CancellationTokenSource _cancellationTokenSource;
        public PowerUpEnum Type => _type;

        public bool IsPaused {get;private set;}

        public void SetBonus(Sprite sprite, float maxTime, PowerUpEnum type)
        {
            _bonusImage.sprite = sprite;
            _bonusSlider.maxValue = maxTime;
            _bonusSlider.value = maxTime;          
            _type = type;
            
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
            StartBonusAnim(maxTime,_cancellationTokenSource.Token);           
        }

        public void SetPaused(bool paused)
        {
            IsPaused = paused;
        }

        public void Disable()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            transform.localScale = Vector3.zero;
            _cancellationTokenSource = null;
            gameObject.SetActive(false);
            _type = PowerUpEnum.Empty;

        }

        private async void StartBonusAnim(float value,CancellationToken token)
        {           
            float duration = value;            
            gameObject.SetActive(true);
            transform.localScale = Vector3.one;
            while (duration > 0)
            {
                if (!IsPaused) duration -= _sliderStep;
                try
                {
                    await UniTask.Delay(100, false, PlayerLoopTiming.FixedUpdate, token);
                    _bonusSlider.value = duration;

                }
                catch (OperationCanceledException)
                {
                    
                }                                              
            }

            if (!token.IsCancellationRequested)
            {
                gameObject.SetActive(false);
                _type = PowerUpEnum.Empty;
            }
            
        }
    }
}

