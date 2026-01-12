using System;
using System.Text.Json.Serialization;

namespace SteamGameStatistics.Application.DTOs
{
    public class GameDto
    {
        [JsonPropertyName("appid")]
        public long AppId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("playtime_forever")]
        public long Playtime { get; set; }

        [JsonPropertyName("img_icon_url")]
        public string IconUrl { get; set; }

        [JsonPropertyName("playtime_windows_forever")]
        public long WindowsPlaytime { get; set; }

        [JsonPropertyName("playtime_mac_forever")]
        public long MacPlaytime { get; set; }

        [JsonPropertyName("playtime_linux_forever")]
        public long LinuxPlaytime { get; set; }

        public string GetIconUrl()
        {
            return $"http://media.steampowered.com/steamcommunity/public/images/apps/{AppId}/{IconUrl}.jpg";
        }

        public string GetTotalGamePlaytime()
        {
            TimeSpan time = TimeSpan.FromMinutes(Playtime);
            return $"{Math.Round(time.TotalHours, 2)} hours";
        }
    }
}
