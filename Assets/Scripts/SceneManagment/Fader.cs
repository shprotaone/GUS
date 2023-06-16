using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUS.SceneManagment
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] private float _time;
        [SerializeField] private CanvasGroup _canvasGroup;
        void Start()
        {    
            //FadeIn();
        }

        public IEnumerator FadeOut()
        {
            _canvasGroup.DOFade(1,_time);
            yield return new WaitForSeconds(_time);
            yield return null;
        }

        public async UniTask FadeIn()
        {       
            _canvasGroup.alpha = 1;
            _canvasGroup.DOFade(0,_time);
            await UniTask.Yield();

        }
    }
}

