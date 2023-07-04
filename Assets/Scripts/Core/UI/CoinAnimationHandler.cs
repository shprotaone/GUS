using Cysharp.Threading.Tasks;
using DG.Tweening;
using GUS.Core.Locator;
using UnityEngine;

public class CoinAnimationHandler : MonoBehaviour
{
    [SerializeField] private Transform _cornsObj;
    [SerializeField] private Transform _enpPos;
    [SerializeField] private CoinAnim[] _coins;
    [SerializeField] private RectTransform[] _mainCorns;
    [SerializeField] private RectTransform[] _stickers;

    private AudioService _audioService;
    public void Init(IServiceLocator serviceLocator)
    {
        _audioService = serviceLocator.Get<AudioService>();
    }

    public async void Animate()
    {
        await UniTask.Delay(1000);
        _cornsObj.gameObject.SetActive(true);
        await CallExplosion();        

        foreach (CoinAnim coinAnim in _coins)
        {
            await UniTask.Delay(200);
            coinAnim.gameObject.SetActive(true);
            coinAnim.Movement(_enpPos.position);
        }

        foreach (var sticker in _stickers)
        {
            sticker.gameObject.SetActive(false);
        }
    }

    private async UniTask CallExplosion()
    {
        for (int i = 0; i < _mainCorns.Length; i++)
        {
            MainCornAnim(_mainCorns[i]);
            StickerAnim(_stickers[i]);
            await UniTask.Delay(200);
        }

        await UniTask.Yield();
    }
    private void MainCornAnim(RectTransform corn)
    {
        corn.localScale = Vector3.zero;
        corn.gameObject.SetActive(true);
        corn.DOScale(Vector3.one * 3, 1).OnComplete(() => 
        {
            _audioService.PlaySFX(_audioService.Data.collectBonus);
            corn.gameObject.SetActive(false);
        });
    }

    private async void StickerAnim(RectTransform sticker)
    {
        await UniTask.Delay(1000);
        sticker.localScale = Vector3.zero;
        sticker.gameObject.SetActive(true);
        sticker.DOScale(Vector3.one * 5, 0.8f).OnComplete(() => sticker.DOScale(Vector3.zero, 0.2f));
    }
}