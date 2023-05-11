using GUS.Core.Locator;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core.UI
{
    public class UIInGame : MonoBehaviour
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Image _bonusImage;
        [SerializeField] private TMP_Text _coinTextValue;
        [SerializeField] private TMP_Text _distanceTextValue;

        private GameStateController _gamestateController;

        public void Init(GameStateController gameStateController)
        {
            _gamestateController = gameStateController;
            _pauseButton.onClick.AddListener(_gamestateController.Pause);
        }

        public void RefreshCoinsCount(int count)
        {
            _coinTextValue.text = count.ToString();
        }

        public void RefreshDistancePointCount(float count)
        {
            _distanceTextValue.text = count.ToString();
        }

        public void SetBonusImage(Sprite sprite)
        {
            _bonusImage.enabled = true;
            _bonusImage.sprite = sprite;
        }

        public void DisableBonusImage()
        {
            _bonusImage.enabled = false;
            _bonusImage.sprite = null;
        }
    }
}

