namespace GUS.Core.UI
{
    public interface ICoinView
    {
        public void RefreshCoinsCount(int count);
        public void RefreshCoinWithAnim(int count,int prevValue);
    }
}
