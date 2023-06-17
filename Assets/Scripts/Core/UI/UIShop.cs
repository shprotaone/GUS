﻿using GUS.Core.Data;
using GUS.Core.Hub.BonusShop;
using GUS.Core.Locator;
using GUS.Core.UI;
using GUS.Objects.PowerUps;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core
{
    public class UIShop : MonoBehaviour
    {
        [SerializeField] private Button _close;
        [SerializeField] private List<ShopSlotView> _slotView;
        [SerializeField] private GameObject _shopPanel;

        private CoinView _coinView;
        public void Init(IServiceLocator locator)
        {
            _coinView = locator.Get<ICoinView>() as CoinView;
            _close.onClick.AddListener(Close);
        }

        public void Activate(bool flag)
        {
            _shopPanel.SetActive(flag);
            _coinView.Activate(flag);
        }

        public void InitSlots(ShopSystem shop, List<Collectable> collectables, List<BonusData> bonusData)
        {
            for (int i = 0; i < collectables.Count; i++)
            {
                _slotView[i].Init(shop, collectables[i], bonusData[i]);
            }
        }

        public void UpdateSlots(List<Collectable> collectables, List<BonusData> bonusData)
        {
            for (int i = 0; i < collectables.Count; i++)
            {
                _slotView[i].UpdateData(bonusData[i]);
            }
        }

        private void Close()
        {
            _coinView.Activate(false);
            _shopPanel.gameObject.SetActive(false);
        }
    }
}