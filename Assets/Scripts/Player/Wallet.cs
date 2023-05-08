using GUS.Core.Locator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet
{
    private UIController _uiController;
    private int _coins;
    public int Coins => _coins;

    public Wallet(IServiceLocator locator)
    {
        _uiController = locator.Get<UIController>();
    }
    public void AddCoin()
    {
        _coins++;
        _uiController.RefreshCoinsCount(_coins);
    }
}
