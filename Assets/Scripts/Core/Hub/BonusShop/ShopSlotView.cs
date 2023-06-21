using GUS.Core.Data;
using GUS.Objects.PowerUps;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core.Hub.BonusShop
{
    public class ShopSlotView : MonoBehaviour
    {
        [SerializeField] private Button _buy;
        [SerializeField] private TMP_Text _costText;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _desriptionText;
        [SerializeField] private Image _icon;     
        [SerializeField] private Image[] _images;

        private ShopSystem _shopSystem;
        private Collectable _collectable;
        private BonusData _bonusData;

        private int _currentCost;
        public void Init(ShopSystem shopSystem, Collectable collectable,BonusData bonusData)
        {
            _collectable = collectable;
            _shopSystem = shopSystem;
            _bonusData = bonusData;           
            _nameText.text = _collectable.nameCollectable;
            _desriptionText.text = _collectable.descriptionCollectable;
            _icon.sprite = _collectable.icon;

            UpdateData(bonusData);
            CheckState();
            _buy.onClick.AddListener(Buy);
        }

        public void UpdateData(BonusData bonusData)
        {
            _bonusData = bonusData;
            UpdateCost();
            RefreshProgress(_bonusData.state);
        }

        private void Buy()
        {
            _shopSystem.Buy(_collectable.powerUpEnum, _currentCost, _bonusData.state);
            CheckState();
            UpdateCost();
            RefreshProgress(_bonusData.state);
        }

        private void CheckState()
        {
            if (_shopSystem.Wallet.Coins < _currentCost)
            {
                _buy.interactable = false;
            }
            else
            {
                _buy.interactable = true;
            }
        }

        private void UpdateCost()
        {
            if (_collectable.powerUpEnum != PowerUpEnum.HonkCoin) 
            {
                _currentCost = _collectable.costs[_bonusData.state];
                
                if (_bonusData.state >= _collectable.costs.Length)
                {
                    _buy.interactable = false;
                    _costText.text = "";
                }
            }
            else
            {
                _currentCost = _collectable.costs[0];
            }

            _costText.text = _currentCost.ToString();           
        }

        private void RefreshProgress(int index)
        {
            if (index == 0) return;
            if (_collectable.powerUpEnum == PowerUpEnum.HonkCoin) return;

            for (int i = 0; i < index; i++)
            {
                _images[i].color = Color.green;
            }
        }
    }
}

