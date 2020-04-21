using System;
using System.Text.Json.Serialization;
using System.Web.Mvc;

namespace SteamGameStatistics.Models.Steam
{
    public class Game
    {
        [JsonPropertyName("appid")]
        public long Appid { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("playtime_2weeks")]
        public long Playtime2Weeks { get; set; }

        [JsonPropertyName("playtime_forever")]
        public long PlaytimeForever { get; set; }

        [JsonPropertyName("img_icon_url")]
        public string ImgIconUrl { get; set; }

        [JsonPropertyName("img_logo_url")]
        public string ImgLogoUrl { get; set; }

        [JsonPropertyName("playtime_windows_forever")]
        public long PlaytimeWindowsForever { get; set; }

        [JsonPropertyName("playtime_mac_forever")]
        public long PlaytimeMacForever { get; set; }

        [JsonPropertyName("playtime_linux_forever")]
        public long PlaytimeLinuxForever { get; set; }

        [OutputCache(Duration = 86400, VaryByParam = "none")]
        public string GetIconUrl()
        {
            return $"http://media.steampowered.com/steamcommunity/public/images/apps/{Appid}/{ImgIconUrl}.jpg";
        }

        [OutputCache(Duration = 86400, VaryByParam = "none")]
        public string GetLogoUrl()
        {
            return $"http://media.steampowered.com/steamcommunity/public/images/apps/{Appid}/{ImgLogoUrl}.jpg";
        }

        public string GetTotalGamePlaytime()
        {
            TimeSpan time = TimeSpan.FromMinutes(PlaytimeForever);
            return $"{Math.Round(time.TotalHours, 2)} hours";
        }

        public string GetLastTwoWeeksPlaytime()
        {
            TimeSpan time = TimeSpan.FromMinutes(Playtime2Weeks);
            return $"{Math.Round(time.TotalHours, 2)} hours";
        }
    }
}
