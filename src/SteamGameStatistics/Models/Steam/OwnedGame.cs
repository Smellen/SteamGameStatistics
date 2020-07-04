using System;
using System.Text.Json.Serialization;

namespace SteamGameStatistics.Models.Steam
{
    public class OwnedGame
    {
        [JsonPropertyName("appid")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("playtime_forever")]
        public long TotalPlaytime { get; set; }

        [JsonPropertyName("playtime_windows_forever")]
        public long WindowPlaytime { get; set; }

        [JsonPropertyName("playtime_mac_forever")]
        public long MacPlaytime { get; set; }

        [JsonPropertyName("playtime_linux_forever")]
        public long LinuxPlaytime { get; set; }

        [JsonPropertyName("has_community_visible_stats")]
        public bool CommunityStatsVisible { get; set; }

        [JsonPropertyName("img_icon_url")]
        public string ImgIconUrl { get; set; }

        [JsonPropertyName("img_logo_url")]
        public string ImgLogoUrl { get; set; }

        public string GetIconUrl()
        {
            return $"http://media.steampowered.com/steamcommunity/public/images/apps/{Id}/{ImgIconUrl}.jpg";
        }

        public string GetLogoUrl()
        {
            return $"http://media.steampowered.com/steamcommunity/public/images/apps/{Id}/{ImgLogoUrl}.jpg";
        }

        public string GetTotalGamePlaytime()
        {
            TimeSpan time = TimeSpan.FromMinutes(TotalPlaytime);
            return $"{Math.Round(time.TotalHours, 2)} hours";
        }

        public string GetTotalWindowsPlaytime()
        {
            TimeSpan time = TimeSpan.FromMinutes(WindowPlaytime);
            return $"{Math.Round(time.TotalHours, 2)} hours";
        }

        public string GetTotalMacPlaytime()
        {
            TimeSpan time = TimeSpan.FromMinutes(MacPlaytime);
            return $"{Math.Round(time.TotalHours, 2)} hours";
        }

        public string GetTotalLinuxPlaytime()
        {
            TimeSpan time = TimeSpan.FromMinutes(LinuxPlaytime);
            return $"{Math.Round(time.TotalHours, 2)} hours";
        }
    }
}
