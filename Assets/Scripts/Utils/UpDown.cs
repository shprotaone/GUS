using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    [Title("Äëÿ 3D")]
    [SerializeField] private float _power;

    [Title("Äëÿ rect")]
    [SerializeField] private RectTransform _rect;
    [SerializeField] private Vector2 _toPosRect;   
    [SerializeField] private bool _isRect;

    private void OnEnable()
    {
        if(!_isRect) transform.DOLocalMoveY(_power, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        else _rect.DOAnchorPos(_toPosRect, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

    }

    private void OnDisable()
    {
        DOTween.Kill(this);
    }
}
