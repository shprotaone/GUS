using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GUS.Core.Data
{
    [Serializable]
    public class PlayerData
    {
        [JsonProperty(PropertyName = "name")]
        public string playerName;
        [JsonProperty(PropertyName = "coin")]
        public int coins;
        [JsonProperty(PropertyName = "honkCoin")]
        public int honkCoins;
        [JsonProperty(PropertyName = "dist")]
        public float commonDistance;
        public List<BuildData> buildDatas;
        public List<BonusData> bonusDatas;
        public List<bool> _tutorialSteps;

        

        //TODO: »нкапсул€ци€? 

        public PlayerData() { }
    }
}

