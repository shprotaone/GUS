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
        [SerializeField] private Image _multiplyImage;
        [SerializeField] private TMP_Text _coinTextValue;
        [SerializeField] private TMP_Text _distanceTextValue;
        [SerializeField] private TMP_Text _honkTextValue;

        [Title("Ресурсы для мультипликатора")]
        [SerializeField] private Sprite[] _sprites;

        [Title("Бонусы")]
        [SerializeField] private BonusSlotView[] _bonusSlotViews;

        private GameStateController _gamestateController;
        private PauseHandle _pauseHandle;
        public void Init(GameStateController gameStateController,PauseHandle pauseHandle)
        {
            _gamestateController = gameStateController;
            _pauseHandle = pauseHandle;
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
        }

        public void ActivateBonusView(IPowerUp powerUp)
        {
            foreach (var view in _bonusSlotViews)
            {
                if(view.Type == PowerUpEnum.Empty || view.Type == powerUp.PowerUpEnum)
                {
                    view.SetBonus(powerUp.Sprite, powerUp.Duration, powerUp.PowerUpEnum,_pauseHandle);                   
                    return;
                }
            }
        }

        public void DesactivateBonuses()
        {
            foreach (var view in _bonusSlotViews)
            {
                view.Disable();
            }
        }
    }
}

