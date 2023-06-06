using GUS.Core.Data;
using GUS.Core.Locator;
using System;
using UnityEngine;

public class BuildsSystem : MonoBehaviour
{
    [SerializeField] private BuildSlot[] _slots;

    private Wallet _wallet;
    public void Init(IServiceLocator serviceLocator)
    {
        _wallet = serviceLocator.Get<Wallet>();
    }

    private void RefreshStore()
    {
        foreach (var slot in _slots)
        {
            slot.LoadProgress();
        }
    }
    public void Buy(BuildEnum build)
    {
        
    }
}
