using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core.UI
{
    public class UIInGame : MonoBehaviour,ICoinView,IDistanceView
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Image _bonusImage;
        [SerializeField] private Image _bonusImage2;
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

        public int SetBonusImage(Sprite sprite)
        {
            if (!_bonusImage.enabled)
            {
                _bonusImage.enabled = true;
                _bonusImage.sprite = sprite;
                return 1;
            }
            else if(_bonusImage.enabled)
            {
                _bonusImage2.enabled = true;
                _bonusImage2.sprite = sprite;
                return 2;
            }

            return 0;
        }

        public void DisableBonusImage(int index)
        {
            if(index == 1)
            {
                _bonusImage.enabled = false;
                _bonusImage.sprite = null;
            }
            else if(index == 2)
            {
                _bonusImage2.enabled = false;
                _bonusImage2.sprite = null;
            }
            
        }
    }
}

