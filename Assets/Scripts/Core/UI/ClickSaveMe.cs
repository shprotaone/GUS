using GUS.Core.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickSaveMe : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private SaveMe _saveMe;
    public void OnPointerClick(PointerEventData eventData)
    {
        _saveMe.DecreaseTime();
    }
}
