using GUS.Core.Locator;
using GUS.Core.SaveSystem;
using GUS.Core.UI;
using System;

public class Wallet
{
    private const string key = "coins";
    private StorageService _storageService;

    private UIInGame _uiController;
    private int _coins;
    private float _distancePoint;
    private float _bossCompletePoint;
    private int _multiply = 1;
    public int Coins => _coins;

    public Wallet(IServiceLocator locator)
    {
        _uiController = locator.Get<UIController>().UiInGame;
        _storageService= locator.Get<StorageService>();
    }
    public void AddCoin()
    {
        _coins += 1 * _multiply;
        _uiController.RefreshCoinsCount(_coins);
    }

    public void AddDistancePoint(int reward)
    {
        _distancePoint += reward;
        _uiController.RefreshCoinsCount(_coins);
    }

    public void SaveDatas()
    {
        _storageService.Data.coins += _coins;
        _storageService.Data.commonDistance += _distancePoint;
        _storageService.Save();
    }

    public void ResetCounter()
    {
        _coins = 0;
        _distancePoint = 0;
        _uiController.RefreshDistancePointCount(_distancePoint);
        _uiController.RefreshCoinsCount(_coins);
    }

    public void SetDistancePoint(int distance)
    {
        _distancePoint = (distance);
        _uiController.RefreshDistancePointCount(_distancePoint);
    }

    public void SetMultiply(bool flag)
    {
        if (flag) _multiply = 2;
        else _multiply = 1;
    }
}
