namespace GUS.Core.Pool
{
    public enum PoolObjectType
    {
       Platform, 
       LeftObstacle,
       RightObstacle,
       CenterObstacle,
       JumpObstacle,
       CrounchObstacle,
       LeftSecondLevel,
       RightSecondLevel,       
       Clicker,
       AfterClicker,
       FullSecondLevel,
       EdgeSecondLevel,
       FreeWithSpawn,

       Empty,

       Coin = 40,
       Magnet,
       Multiply,

       CoinUI = 100
    }
}