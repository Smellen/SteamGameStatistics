using System;
using System.Text.Json.Serialization;

namespace SteamGameStatistics.Models.Steam
{
    public class User
    {
        [JsonPropertyName("steamid")]
        public string Steamid { get; set; }

        [JsonPropertyName("communityvisibilitystate")]
        public long Communityvisibilitystate { get; set; }

        [JsonPropertyName("profilestate")]
        public long Profilestate { get; set; }

        [JsonPropertyName("personaname")]
        public string Personaname { get; set; }

        [JsonPropertyName("commentpermission")]
        public long Commentpermission { get; set; }

        [JsonPropertyName("profileurl")]
        public Uri Profileurl { get; set; }

        [JsonPropertyName("avatar")]
        public Uri Avatar { get; set; }

        [JsonPropertyName("avatarmedium")]
        public Uri Avatarmedium { get; set; }

        [JsonPropertyName("avatarfull")]
        public Uri Avatarfull { get; set; }

        [JsonPropertyName("lastlogoff")]
        public long Lastlogoff { get; set; }

        [JsonPropertyName("personastate")]
        public long Personastate { get; set; }

        [JsonPropertyName("realname")]
        public string Realname { get; set; }

        [JsonPropertyName("primaryclanid")]
        public string Primaryclanid { get; set; }

        [JsonPropertyName("timecreated")]
        public long Timecreated { get; set; }

        [JsonPropertyName("personastateflags")]
        public long Personastateflags { get; set; }

        [JsonPropertyName("loccountrycode")]
        public string Loccountrycode { get; set; }

        [JsonPropertyName("locstatecode")]
        public string Locstatecode { get; set; }

        public string GetLastLoggedInDate()
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dateTime = dateTime.AddSeconds(this.Lastlogoff);

            return dateTime.ToUniversalTime().ToString();
        }

        public string GetProfileCreateDate()
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dateTime = dateTime.AddSeconds(this.Timecreated);

            return dateTime.ToUniversalTime().ToString();
        }
    }
}
