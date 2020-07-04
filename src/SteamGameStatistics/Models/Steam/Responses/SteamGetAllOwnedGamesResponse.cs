using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SteamGameStatistics.Models.Steam.Responses
{
    public class SteamGetAllOwnedGamesResponse
    {
        [JsonPropertyName("response")]
        public AllOwnedGames Response { get; set; }
    }

    public class AllOwnedGames
    {
        [JsonPropertyName("game_count")]
        public int TotalGames { get; set; }

        [JsonPropertyName("games")]
        public List<OwnedGame> OwnedGames { get; set; }
    }
}
