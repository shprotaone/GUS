using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUS.SceneManagment
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] private int _time;
        [SerializeField] private CanvasGroup _canvasGroup;
        void Start()
        {
            _canvasGroup.alpha = 1;
        }

        public async UniTask FadeOut()
        {
            _canvasGroup.DOFade(1,_time/1000);
            await UniTask.Delay(_time);
            await UniTask.Yield();
        }

        public async UniTask FadeIn()
        {       
            _canvasGroup.alpha = 1;
            _canvasGroup.DOFade(0,_time/1000);
            await UniTask.Yield();

        }
    }
}

