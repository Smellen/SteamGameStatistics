using System.Text.Json.Serialization;

namespace SteamGameStatistics.Application.DTOs
{
    public class RecentlyPlayedResponseDto
    {
        [JsonPropertyName("response")]
        public RecentlyPlayedResponseBodyDto Response { get; set; }
    }

    public class RecentlyPlayedResponseBodyDto
    {
        [JsonPropertyName("games")]
        public GameDto[] Games { get; set; }

        [JsonPropertyName("total_count")]
        public long Count {get;set;}
    }
}
