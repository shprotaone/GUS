using GUS.Core.SaveSystem;

namespace GUS.Core.Data
{
    public interface IData
    {
        StorageService StorageService { get; }
        void UpdateCoins();
    }
}