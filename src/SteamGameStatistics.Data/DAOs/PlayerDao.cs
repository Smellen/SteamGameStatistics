using System;

namespace SteamGameStatistics.Data.DAOs
{
    public class PlayerDao
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string Steamid { get; set; }

        public long CommunityVisibilityState { get; set; }

        public long Profilestate { get; set; }

        public string PersonaName { get; set; }

        public Uri ProfileUrl { get; set; }

        public Uri Avatar { get; set; }

        public Uri AvatarMedium { get; set; }

        public Uri AvatarFull { get; set; }

        public string AvatarHash { get; set; }

        public long Lastlogoff { get; set; }

        public long PersonaState { get; set; }

        public string RealName { get; set; }

        public string PrimaryClanid { get; set; }

        public long TimeCreated { get; set; }

        public long PersonaStateFlags { get; set; }

        public string LocCountryCode { get; set; }

        public string LocStateCode { get; set; }
    }
}
