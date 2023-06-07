using GUS.Core.Data;
using GUS.Core.Locator;
using GUS.Core.SaveSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildsSystem : MonoBehaviour
{
    [SerializeField] private Build[] _builds;

    private Wallet _wallet;
    private StorageService _storageService;

    private List<BuildData> _buildsData;

    public void Init(IServiceLocator serviceLocator)
    {
        _wallet = serviceLocator.Get<Wallet>();
        _storageService= serviceLocator.Get<StorageService>();

        _buildsData = _storageService.Data.buildDatas;
        LoadListBuild();
        UpdateBuilds();
    }

    private void LoadListBuild() 
    { 
        if(_buildsData.Count == 0) 
        {
            foreach (var build in _builds)
            {
                _buildsData.Add(new BuildData(build.Container.buildName, BuildStateEnum.None));
            }
        }
       
    }
    private void UpdateBuilds()
    {
        for (int i = 0; i < _builds.Length; i++)
        {
            _builds[i].Init(this, _buildsData[i]);
        }
    }

    public void Buy(BuildNameEnum buildName, int cost)
    {
        if (_wallet.Coins < cost) return;

        _wallet.DecreaseCoins(cost);
        for (int i = 0; i < _buildsData.Count; i++)
        {
            if (_buildsData[i].nameEnum == buildName)
            {
                _buildsData[i].state++;
                _builds[i].RefreshData();
            }
        }
        
        _storageService.Save();
    }
}
