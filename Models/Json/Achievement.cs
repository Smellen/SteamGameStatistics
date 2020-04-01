using Newtonsoft.Json;

namespace SteamGameStatistics.Models.Json
{
    public class Achievement
    {
        [JsonProperty("apiname")]
        public string Name { get; set; }

        [JsonProperty("achieved")]
        public bool Unlocked { get; set; }

        [JsonProperty("unlocktime")]
        public string UnlockedTime { get; set; }
    }
}
