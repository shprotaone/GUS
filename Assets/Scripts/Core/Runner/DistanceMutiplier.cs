using GUS.Core;
using GUS.Core.Locator;
using GUS.Core.SaveSystem;
using GUS.Core.UI;
using System;

public class DistanceMutiplier
{
    public event Action<int> OnMultiplyChanged;
    private StorageService _storageService;

    public int Multiplier { get; private set; }
    public int ResultMulty { get; private set; }

    public void Init(IServiceLocator locator)
    {
        _storageService = locator.Get<StorageService>();

        CalculateBonus();
        OnMultiplyChanged.Invoke(Multiplier);
    }

    private void CalculateBonus()
    {
        var datas = _storageService.Data.buildDatas;
        int mult = 0;
        for (int i = 0; i < datas.Count; i++)
        {
            mult += datas[i].state;
        }

        Multiplier = mult;
        ResultMulty = mult +1;
    }

    public void ChangeBonusAnimation()
    {
        CalculateBonus();
        OnMultiplyChanged.Invoke(Multiplier);
    }
}
