using DG.Tweening;
using GUS.Core.Data;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core.UI
{
    public class UIInGame : MonoBehaviour,ICoinView,IDistanceView
    {
        [SerializeField] private RectTransform _inGamePanel;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Image _bonusImage;
        [SerializeField] private Image _bonusImage2;
        [SerializeField] private Image _multiplyImage;
        [SerializeField] private TMP_Text _coinTextValue;
        [SerializeField] private TMP_Text _distanceTextValue;
        [SerializeField] private TMP_Text _honkTextValue;

        [Title("Ресурсы для мультипликатора")]
        [SerializeField] private Sprite[] _sprites;

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

        public void Hide(bool flag)
        {
            if (flag) _inGamePanel.DOAnchorPosY(400, 1);
            else _inGamePanel.DOAnchorPosY(0, 1);           
        }

        public void SetMultiplyImage(int val)
        {
            _multiplyImage.sprite = _sprites[val];
            //switch (val)
            //{
            //    case 1: _multiplyImage.color= Color.white; break;
            //    case 2: _multiplyImage.color = Color.green; break;
            //    case 3: _multiplyImage.color = Color.blue; break;
            //    case 4: _multiplyImage.color = Color.cyan; break;
            //    case 5: _multiplyImage.color = Color.magenta; break;
            //    case 6: _multiplyImage.color = Color.yellow; break;
            //    case 7: _multiplyImage.color = Color.red; break;
            //    default: _multiplyImage.color = Color.black; break;
            //}
        }

        #region пока не используем
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
        #endregion
    }
}

