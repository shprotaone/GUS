using GUS.Core.Locator;
using GUS.Core.UI;
using System;

public class Wallet
{
    private UIInGame _uiController;
    private int _coins;
    private float _distancePoint;
    private float _bossCompletePoint;
    private float _multiply = 1;
    public int Coins => _coins;

    public Wallet(IServiceLocator locator)
    {
        _uiController = locator.Get<UIController>().UiInGame;
    }
    public void AddCoin()
    {
        _coins++;
        _uiController.RefreshCoinsCount(_coins);
    }

    public void AddDistancePoint(int reward)
    {
        _distancePoint += reward;
        _uiController.RefreshCoinsCount(_coins);
    }

    public void ResetCounter()
    {
        _coins = 0;
        _uiController.RefreshCoinsCount(_coins);
    }

    public void SetDistancePoint(int distance)
    {
        _distancePoint += (distance * _multiply);
        _uiController.RefreshDistancePointCount(_distancePoint);
    }

    public void SetMultiply(bool flag)
    {
        if (flag) _multiply = 2;
        else _multiply = 1;
    }
}
