using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core.Hub.BuildShop
{
    public class BuildSlotView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _cost;
        [SerializeField] private Button _buy;
        [SerializeField] private Image[] _progressImages;

        private int _currentCost;
        private BuildsSystem _buildsSystem;
        public BuildNameEnum BuildName { get; private set; }
        public int BuildState { get; private set; }

        public void Init(BuildsSystem buildSystem, BuildData buildData)
        {
            _buildsSystem = buildSystem;
            BuildName = buildData.nameEnum;
            BuildState = buildData.state;
            
            _buy.onClick.AddListener(Buy);
        }

        private void Buy()
        {
            _buildsSystem.Buy(BuildName, _currentCost);
        }

        public void RefreshProgress(int buildState, int step)
        {
            if (buildState == 0) return;

            for (int i = 0; i < (int)buildState; i++)
            {
                _progressImages[i].color = Color.blue;
            }

            CheckButtonState();
            if (buildState < step) _buy.interactable = false;
          
        }

        private void CheckButtonState()
        {
            if (_buildsSystem.Wallet.Coins <= _currentCost) _buy.interactable = false;
            else _buy.interactable = true;
        }

        public void SetCost(int cost)
        {
            _cost.text = cost.ToString();
            _currentCost = cost;
            CheckButtonState();
        }

        public void Disable()
        {
            _cost.text = "";
            _buy.interactable = false;
        }
    }
}

