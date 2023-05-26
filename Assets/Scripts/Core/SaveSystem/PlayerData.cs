using Newtonsoft.Json;
using System;

namespace GUS.Core.SaveSystem
{
    [Serializable]
    public class PlayerData
    {
        [JsonProperty(PropertyName = "name")]
        public string playerName;
        [JsonProperty(PropertyName = "coin")]
        public int coins;
        [JsonProperty(PropertyName = "dist")]
        public float commonDistance;

        public PlayerData() { }
        public PlayerData(string playerName, int coins, float distance)
        {
            this.playerName = playerName;
            this.coins = coins;
            this.commonDistance = distance;
        }

        public PlayerData(PlayerData data)
        {
            this.playerName = data.playerName;
            this.coins = data.coins;
            this.commonDistance = data.commonDistance;
        }
    }
}

