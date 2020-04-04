using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SteamGameStatistics.Interfaces;
using SteamGameStatistics.Models.Json;

namespace SteamGameStatistics.Services
{
    public class JsonReaderService : IJsonReaderService
    {
        private const string NameSortField = "name";
        private const string AchievementCountSortField = "ac-count";

        /// <summary>
        /// Update this value
        /// </summary>
        private const string TempFileLocation = @"C:\Users\Ellen\Desktop\AllSteamGamesStats.json";

        /// <summary>
        /// Gets all games from file.
        /// </summary>
        /// <param name="sort">The order to sort the games on.</param>
        /// <returns>A list of games.</returns>
        public async Task<List<Game>> GetGamesFromFile(string sort)
        {
            var games = JsonConvert.DeserializeObject<List<Game>>(await File.ReadAllTextAsync(TempFileLocation));

            var sortedGames = sort switch
            {
                NameSortField => games.OrderBy(e => e.Name).ToList(),
                AchievementCountSortField => games.OrderByDescending(e => e.Achievements.Count).ToList(),
                _ => games.OrderBy(e => e.Name).ToList(),
            };

            return sortedGames;
        }
    }
}
