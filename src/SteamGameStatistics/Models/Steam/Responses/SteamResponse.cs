using System.Text.Json.Serialization;

namespace SteamGameStatistics.Models.Steam.Responses
{
    public class SteamResponse
    {
        [JsonPropertyName("response")]
        public ListOfGamesResponse Response { get; set; }
    }

    public class ListOfGamesResponse
    {
        [JsonPropertyName("total_count")]
        public long TotalCount { get; set; }

        [JsonPropertyName("games")]
        public Game[] Games { get; set; }
    }
}
