using System.Text.Json.Serialization;

namespace SteamGameStatistics.Application.DTOs
{
    public class PlayerResponseDto
    {
        [JsonPropertyName("response")]
        public PlayerResponseBodyDto Response { get; set; }
    }

    public class PlayerResponseBodyDto
    {
        [JsonPropertyName("players")]
        public PlayerDto[] Users { get; set; }
    }
}
