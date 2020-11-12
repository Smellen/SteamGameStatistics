using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SteamGameStatistics.Cache;
using SteamGameStatistics.Enums;
using SteamGameStatistics.Interfaces;
using SteamGameStatistics.Models.Steam;
using SteamGameStatistics.Models.Steam.Responses;

namespace SteamGameStatistics.Services
{
    public class SteamService : ISteamService
    {
        private readonly ILogger<SteamService> _logger;
        private readonly HttpClient _client;
        private readonly ICacheService _cache;
        private readonly IEnvironmentVariablesService _environmentVariablesService;

        public string SteamKey { get; private set; }
        public string SteamId { get; private set; }

        public SteamService(ILogger<SteamService> logger, HttpClient client, ICacheService cache, IEnvironmentVariablesService environmentVariablesService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _environmentVariablesService = environmentVariablesService ?? throw new ArgumentNullException(nameof(environmentVariablesService));
        }

        /// <summary>
        /// Get a steam user.
        /// </summary>
        /// <returns>A steam user.</returns>
        public async Task<User> GetSteamUser()
        {
            SetEnvironmentVariables();

            User user = null;

            var cachedUser = _cache.TryGet(CacheKeys.UserKey);
            if (cachedUser == null)
            {
                var uri = new Uri($"http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={SteamKey}&steamids={SteamId}");

                _logger.LogDebug($"Sending GET request for getting Steam user : {uri.AbsoluteUri}");

                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var responseStr = await response.Content.ReadAsStringAsync();
                    _logger.LogDebug($"Response: {response.StatusCode} - {responseStr}");
                    var userResponse = JsonSerializer.Deserialize<PlayerInformation>(responseStr);
                    user = userResponse.Response.Users.FirstOrDefault();

                    _cache.Create(CacheKeys.UserKey, user);
                }
            }
            else
            {
                user = cachedUser as User;
            }

            return user;
        }

        /// <summary>
        /// Gets the recently played games.
        /// </summary>
        /// <returns>A list of recently played games.</returns>
        public async Task<List<Game>> GetRecentlyPlayedGames()
        {
            SetEnvironmentVariables();

            List<Game> recentlyPlayedGmes = null;
            var cachedRecentlyPlayedGames = _cache.TryGet(CacheKeys.RecentlyPlayedGamesKey);
            if (cachedRecentlyPlayedGames == null)
            {
                var uri = new Uri($"http://api.steampowered.com/IPlayerService/GetRecentlyPlayedGames/v0001/?key={SteamKey}&steamid={SteamId}&format=json");

                _logger.LogDebug($"Sending GET request for recently played games : {uri.AbsoluteUri}");

                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var responseStr = await response.Content.ReadAsStringAsync();
                    _logger.LogDebug($"Response: {response.StatusCode} - {responseStr}");
                    var recentlyPlayedGamesResponse = JsonSerializer.Deserialize<SteamListOfGamesResponse>(responseStr);
                    recentlyPlayedGmes = recentlyPlayedGamesResponse.Response.Games.ToList();

                    _cache.Create(CacheKeys.RecentlyPlayedGamesKey, recentlyPlayedGmes);
                }
            }
            else
            {
                recentlyPlayedGmes = cachedRecentlyPlayedGames as List<Game>;
            }

            return recentlyPlayedGmes;
        }

        /// <summary>
        /// Gets all games from steam and save the response into a file.
        /// </summary>
        /// <returns>True if the games have been successfully saved to a file.</returns>
        public async Task<List<OwnedGame>> GetAllGamesFromSteam()
        {
            SetEnvironmentVariables();

            List<OwnedGame> allOwnedGames = null;
            var cachedAllOwnedGames = _cache.TryGet(CacheKeys.AllGamesKey);

            if (cachedAllOwnedGames == null)
            {
                var uri = new Uri($"http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={SteamKey}&steamid={SteamId}&format=json&include_appinfo=true");

                _logger.LogDebug($"Sending GET request for all owned games : {uri.AbsoluteUri}");

                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var responseStr = await response.Content.ReadAsStringAsync();
                    _logger.LogDebug($"Response: {response.StatusCode} - {responseStr}");
                    var allOwnedGamesResponse = JsonSerializer.Deserialize<SteamGetAllOwnedGamesResponse>(responseStr);
                    allOwnedGames = allOwnedGamesResponse.Response.OwnedGames;

                    _cache.Create(CacheKeys.AllGamesKey, allOwnedGames);
                }
            }
            else
            {
                allOwnedGames = cachedAllOwnedGames as List<OwnedGame>;
            }

            return allOwnedGames;
        }

        private void SetEnvironmentVariables()
        {
            SteamKey = _environmentVariablesService.GetSteamKey();
            SteamId = _environmentVariablesService.GetSteamId();
        }
    }
}
