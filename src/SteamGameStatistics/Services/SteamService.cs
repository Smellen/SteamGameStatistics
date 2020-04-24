using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SteamGameStatistics.Interfaces;
using SteamGameStatistics.Models.Steam;
using SteamGameStatistics.Models.Steam.Responses;

namespace SteamGameStatistics.Services
{
    public class SteamService : ISteamService
    {
        private const string EnvironmentKeySteamKey = "SteamKey";
        private const string EnvironmentKeySteamId = "SteamId";

        private readonly ILogger<SteamService> _logger;
        public readonly HttpClient _client;

        public string SteamKey { get; private set; }
        public string SteamId { get; private set; }

        public SteamService(ILogger<SteamService> logger, HttpClient client)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _client = client ?? throw new ArgumentNullException(nameof(client));
            SteamKey = Environment.GetEnvironmentVariable(EnvironmentKeySteamKey);
            SteamId = Environment.GetEnvironmentVariable(EnvironmentKeySteamId);
        }

        /// <summary>
        /// Get a steam user.
        /// </summary>
        /// <returns>A steam user.</returns>
        public async Task<User> GetSteamUser()
        {
            User user = null;
            var uri = new Uri($"http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={SteamKey}&steamids={SteamId}");

            _logger.LogDebug($"Sending GET request for getting Steam user : {uri.AbsoluteUri}");

            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var responseStr = await response.Content.ReadAsStringAsync();
                _logger.LogDebug($"Response: {response.StatusCode} - {responseStr}");
                var userResponse = JsonSerializer.Deserialize<PlayerInformation>(responseStr);
                user = userResponse.Response.Users.FirstOrDefault();
            }

            return user;
        }

        /// <summary>
        /// Gets the recently played games.
        /// </summary>
        /// <returns>A list of recently played games.</returns>
        public async Task<List<Game>> GetRecentlyPlayedGames()
        {
            List<Game> games = null;
            var uri = new Uri($"http://api.steampowered.com/IPlayerService/GetRecentlyPlayedGames/v0001/?key={SteamKey}&steamid={SteamId}&format=json");

            _logger.LogDebug($"Sending GET request for recently played games : {uri.AbsoluteUri}");

            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var responseStr = await response.Content.ReadAsStringAsync();
                _logger.LogDebug($"Response: {response.StatusCode} - {responseStr}");
                var recentlyPlayedGamesResponse = JsonSerializer.Deserialize<SteamResponse>(responseStr);
                games = recentlyPlayedGamesResponse.Response.Games.ToList();
            }

            return games;
        }

        /// <summary>
        /// Gets all games from steam and save the response into a file.
        /// </summary>
        /// <returns>True if the games have been successfully saved to a file.</returns>
        public async Task<bool> GetAllGamesFromSteam()
        {
            var uri = new Uri($"http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={SteamKey}&steamid={SteamId}&format=json&include_appinfo=true");
            _logger.LogDebug($"Sending GET request for all played games : {uri.AbsoluteUri}");

            try
            {
                using (HttpResponseMessage response = await _client.GetAsync(uri))
                using (Stream streamToReadFrom = await response.Content.ReadAsStreamAsync())
                {
                    _logger.LogDebug($"Response: {response.StatusCode}");

                    if (response.IsSuccessStatusCode)
                    {
                        string fileToWriteTo = $"all-games";
                        using (Stream streamToWriteTo = File.Open(fileToWriteTo, FileMode.Create))
                        {
                            await streamToReadFrom.CopyToAsync(streamToWriteTo);
                        }

                        response.Content = null;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while getting all games from steam.");
                return false;
            }

            return true;
        }
    }
}
