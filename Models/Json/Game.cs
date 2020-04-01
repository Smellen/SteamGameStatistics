using System.Collections.Generic;
using Newtonsoft.Json;

namespace SteamGameStatistics.Models.Json
{
    public class Game
    {
        [JsonProperty("SteamID")]
        public string ID { get; set; }

        [JsonProperty("GameName")]
        public string Name { get; set; }

        [JsonProperty("Achievements")]
        public List<Achievement> Achievements { get; set; }
    }
}
