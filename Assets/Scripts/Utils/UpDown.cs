using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    [SerializeField] private float _power;
    private void OnEnable()
    {
        transform.DOLocalMoveY(_power, 1).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        DOTween.Kill(this);
    }
}
