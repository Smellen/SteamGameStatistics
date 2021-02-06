using System.Text.Json.Serialization;
using SteamGameStatistics.Application.DTOs;

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
        public PlayerDto[] Users { get; set; }
    }
}
