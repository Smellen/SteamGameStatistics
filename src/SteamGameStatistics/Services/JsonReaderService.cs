using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SteamGameStatistics.Interfaces;
using SteamGameStatistics.Models.Json;
using SteamGameStatistics.Models.Steam.Responses;

namespace SteamGameStatistics.Services
{
    public class JsonReaderService : IJsonReaderService
    {
        private const string NameSortField = "name";
        private const string AchievementCountSortField = "ac-count";
        private const string AllGamesFileName = "all-games.json";

        /// <summary>
        /// Update this value
        /// </summary>
        private const string TempFileLocation = @"C:\Users\Ellen\Desktop\AllSteamGamesStats.json";

        /// <summary>
        /// Load all games from file. No achievement information.
        /// </summary>
        /// <returns>A list of all owned games.</returns>
        public async Task<List<Models.Steam.Game>> LoadAllGamesFromFile()
        {
            SteamResponse steamResponse;
            try
            {
                using var sr = new StreamReader($@"C:\Users\Ellen\Desktop\steamdata\{AllGamesFileName}");
                string fileContent = await sr.ReadToEndAsync();

                steamResponse = System.Text.Json.JsonSerializer.Deserialize<SteamResponse>(fileContent);
            }
            catch (Exception)
            {
                return null;
            }

            return steamResponse.Response.Games.ToList();
        }

        /// <summary>
        /// Gets all games from file.
        /// </summary>
        /// <param name="sort">The order to sort the games on.</param>
        /// <returns>A list of games.</returns>
        public async Task<List<Game>> LoadGamesWithAchievementsFromFile(string sort)
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
