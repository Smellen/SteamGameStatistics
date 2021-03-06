﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SteamGameStatistics.Interfaces;
using SteamGameStatistics.Models.Json;
using SteamGameStatistics.Models.Steam.Responses;

namespace SteamGameStatistics.Services
{
    public class FileService : IFileService
    {
        private const string NameSortField = "name";
        private const string AchievementCountSortField = "ac-count";
        private const string AllGamesFileName = "all-games.json";

        private readonly ILogger<FileService> _logger;

        public FileService(ILogger<FileService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

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
            SteamListOfGamesResponse steamResponse;
            try
            {
                using var sr = new StreamReader($@"C:\Users\Ellen\Desktop\steamdata\{AllGamesFileName}");
                string fileContent = await sr.ReadToEndAsync();

                steamResponse = System.Text.Json.JsonSerializer.Deserialize<SteamListOfGamesResponse>(fileContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while reading in a local file.");
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
            List<Game> sortedGames = null;

            try
            {
                var games = JsonConvert.DeserializeObject<List<Game>>(await File.ReadAllTextAsync(TempFileLocation));

                sortedGames = sort switch
                {
                    NameSortField => games.OrderBy(e => e.Name).ToList(),
                    AchievementCountSortField => games.OrderByDescending(e => e.Achievements.Count).ToList(),
                    _ => games.OrderBy(e => e.Name).ToList(),
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while reading in a local file.");
                return null;
            }

            return sortedGames;
        }
    }
}
