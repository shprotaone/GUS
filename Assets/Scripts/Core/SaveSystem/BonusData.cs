using System;

namespace GUS.Core.Data
{
    [Serializable]
    public class BonusData
    {
        public PowerUpEnum powerUp;
        public int state;
        public int powerTime;

        public BonusData(PowerUpEnum powerUp, int powerTime,int state)
        {
            this.powerUp = powerUp;
            this.powerTime = powerTime;
            this.state = state;
        }

        public void UpdateData(PowerUpEnum powerUp, int powerTime,int state)
        {
            this.powerUp = powerUp;
            this.powerTime = powerTime;
            this.state = state;
        }

        public void Increase(int state,int powerTime)
        {
            this.state += state;
            this.powerTime += powerTime;
        }
    }
}