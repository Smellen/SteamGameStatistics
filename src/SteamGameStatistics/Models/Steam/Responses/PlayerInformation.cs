using System.Text.Json.Serialization;

namespace SteamGameStatistics.Models.Steam.Responses
{
    public class PlayerInformation
    {
        [JsonPropertyName("response")]
        public PlayerInformationResponse Response { get; set; }
    }

    public class PlayerInformationResponse
    {
        [JsonPropertyName("players")]
        public User[] Users { get; set; }
    }
}
