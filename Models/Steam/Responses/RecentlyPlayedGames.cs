using System.Text.Json.Serialization;

namespace SteamGameStatistics.Models.Steam.Responses
{
    public class RecentlyPlayedGames
    {
        [JsonPropertyName("response")]
        public RecentlyPlayedGamesResponse Response { get; set; }
    }

    public class RecentlyPlayedGamesResponse
    {
        [JsonPropertyName("total_count")]
        public long TotalCount { get; set; }

        [JsonPropertyName("games")]
        public Game[] Games { get; set; }
    }
}
