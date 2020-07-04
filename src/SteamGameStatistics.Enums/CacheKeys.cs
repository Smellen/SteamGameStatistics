using System;
using System.Collections.Generic;

namespace SteamGameStatistics.Enums
{
    public static class CacheKeys
    {
        public static string UserKey = "STEAMUSER";
        public static string RecentlyPlayedGamesKey = "RECENTLYPLAYED";
        public static string AllGamesKey = "ALLGAMES";

        public static List<string> GetKeysUsersCanAccess()
        {
            return new List<string>()
            {
                UserKey,
                RecentlyPlayedGamesKey,
                AllGamesKey
            };
        }
    }
}
