using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
        public List<BuildData> buildDatas;
        public List<BonusData> bonusDatas;

        public PlayerData() { }
    }
}

