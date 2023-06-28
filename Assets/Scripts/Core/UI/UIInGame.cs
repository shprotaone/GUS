using Cysharp.Threading.Tasks;
using DG.Tweening;
using GUS.Core.Data;
using GUS.Core.Locator;
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
        
        [SerializeField] private TMP_Text _coinTextValue;
        [SerializeField] private TMP_Text _distanceTextValue;
        [SerializeField] private TMP_Text _honkTextValue;
        [SerializeField] private MultiplicatorViewer _multiplicatorViewer;
        
        [Title("Нотификация болшого количества монет")]
        [SerializeField] private RectTransform _bigRewardNotify;
        [SerializeField] private TMP_Text _valueReward;


        [Title("Бонусы")]
        [SerializeField] private BonusSlotView[] _bonusSlotViews;

        private GameStateController _gamestateController;
        private PauseHandle _pauseHandle;
        private Wallet _wallet;

        public MultiplicatorViewer MultiplicatorViewer => _multiplicatorViewer;

        public void Init(IServiceLocator serviceLocator, GameStateController gameStateController)
        {
            _gamestateController = gameStateController;
            _wallet = serviceLocator.Get<Wallet>();
            _multiplicatorViewer.Init(serviceLocator);
            _wallet.OnBigRewardNotify += CallBigCornNotify;
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

        public async void CallBigCornNotify(float value)
        {
            _valueReward.text = "+" + value.ToString();
            _bigRewardNotify.gameObject.SetActive(true);
            _bigRewardNotify.DOAnchorPosX(0, 1).SetEase(Ease.OutSine);
            await UniTask.Delay(3000);
            _bigRewardNotify.gameObject.SetActive(false);
            _bigRewardNotify.anchoredPosition = new Vector2(600,-500);
        }

        public void Hide(bool flag)
        {
            if (flag) _inGamePanel.DOAnchorPosY(400, 1);
            else _inGamePanel.DOAnchorPosY(0, 1);           
        }

        public void ActivateBonusView(IPowerUp powerUp)
        {
            foreach (var view in _bonusSlotViews)
            {
                if(view.Type == PowerUpEnum.Empty || view.Type == powerUp.PowerUpEnum)
                {
                    view.SetBonus(powerUp.Sprite, powerUp.Duration, powerUp.PowerUpEnum);                   
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

        private void OnDisable()
        {
            _wallet.OnBigRewardNotify -= CallBigCornNotify;
        }
    }
}

