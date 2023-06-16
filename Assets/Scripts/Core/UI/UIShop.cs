using GUS.Core.Hub.BonusShop;
using GUS.Core.Locator;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GUS.Core
{
    public class UIShop : MonoBehaviour
    {
        [SerializeField] private GameObject _shopPanel;
        

        public void Activate(bool flag)
        {
            _shopPanel.SetActive(flag);
        }

        public void Init(IServiceLocator locator)
        {
            
        }
    }
}