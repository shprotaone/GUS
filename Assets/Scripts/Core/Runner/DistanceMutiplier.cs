using GUS.Core.Locator;
using GUS.Core.SaveSystem;
using GUS.Core.UI;

public class DistanceMutiplier
{
    private int _multiply = 0;
    private StorageService _storageService;
    private UIInGame _UIinGame;

    public int Multiplier => _multiply;

    public void Init(IServiceLocator locator)
    {
        _storageService = locator.Get<StorageService>();
        _UIinGame = locator.Get<UIController>().UiInGame;
        CalculateBonus();
        _UIinGame.SetMultiplyImage(_multiply);
    }

    private void CalculateBonus()
    {
        var datas = _storageService.Data.buildDatas;

        for (int i = 0; i < datas.Count; i++)
        {
            _multiply += datas[i].state;
        }

        if (_multiply == 0) _multiply = 1;
        else _multiply += 1;
    }
}
