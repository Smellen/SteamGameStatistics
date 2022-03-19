using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SteamGameStatistics.Application.DTOs;
using SteamGameStatistics.Domain.Interfaces;

namespace SteamGameStatistics.Domain.Services
{
    public class SteamService : ISteamService
    {
        private readonly ILogger<SteamService> _logger;
        private readonly HttpClient _client;
        private readonly IEnvironmentVariablesService _environmentVariablesService;

        public string SteamKey { get; private set; }
        public string SteamId { get; private set; }

        public SteamService(ILogger<SteamService> logger, HttpClient client, IEnvironmentVariablesService environmentVariablesService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _environmentVariablesService = environmentVariablesService ?? throw new ArgumentNullException(nameof(environmentVariablesService));

            SetEnvironmentVariables();
        }

        /// <summary>
        /// Get a steam user.
        /// </summary>
        /// <returns>A steam user.</returns>
        public async Task<PlayerDto> GetSteamUser()
        {
            PlayerDto user = null;

            var uri = new Uri($"http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={SteamKey}&steamids={SteamId}");

            _logger.LogDebug($"Sending GET request for getting Steam user : {uri.AbsoluteUri}");

            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var responseStr = await response.Content.ReadAsStringAsync();
                _logger.LogDebug($"Response: {response.StatusCode} - {responseStr}");
                var userResponse = JsonSerializer.Deserialize<PlayerResponseDto>(responseStr);
                user = userResponse.Response.Users.FirstOrDefault();

            }

            return user;
        }

        /// <summary>
        /// Gets the recently played games.
        /// </summary>
        /// <returns>A list of recently played games.</returns>
        //public async Task<List<Game>> GetRecentlyPlayedGames()
        //{
        //    List<Game> recentlyPlayedGmes = null;
        //    var uri = new Uri($"http://api.steampowered.com/IPlayerService/GetRecentlyPlayedGames/v0001/?key={SteamKey}&steamid={SteamId}&format=json");

        //    _logger.LogDebug($"Sending GET request for recently played games : {uri.AbsoluteUri}");

        //    var response = await _client.GetAsync(uri);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var responseStr = await response.Content.ReadAsStringAsync();
        //        _logger.LogDebug($"Response: {response.StatusCode} - {responseStr}");
        //        var recentlyPlayedGamesResponse = JsonSerializer.Deserialize<SteamListOfGamesResponse>(responseStr);
        //        if (recentlyPlayedGamesResponse.Response.TotalCount != 0 && recentlyPlayedGamesResponse.Response.Games != null)
        //        {
        //            recentlyPlayedGmes = recentlyPlayedGamesResponse.Response.Games.ToList();
        //        }
        //    }

        //    return recentlyPlayedGmes;
        //}

        /// <summary>
        /// Gets all games from steam and save the response into a file.
        /// </summary>
        /// <returns>True if the games have been successfully saved to a file.</returns>
        //public async Task<List<OwnedGame>> GetAllGamesFromSteam()
        //{
        //    List<OwnedGame> allOwnedGames = null;

        //    var uri = new Uri($"http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={SteamKey}&steamid={SteamId}&format=json&include_appinfo=true");

        //    _logger.LogDebug($"Sending GET request for all owned games : {uri.AbsoluteUri}");

        //    var response = await _client.GetAsync(uri);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var responseStr = await response.Content.ReadAsStringAsync();
        //        _logger.LogDebug($"Response: {response.StatusCode} - {responseStr}");
        //        var allOwnedGamesResponse = JsonSerializer.Deserialize<SteamGetAllOwnedGamesResponse>(responseStr);
        //        allOwnedGames = allOwnedGamesResponse.Response.OwnedGames;
        //    }

        //    return allOwnedGames;
        //}

        private void SetEnvironmentVariables()
        {
            SteamKey = _environmentVariablesService.GetSteamKey();
            SteamId = _environmentVariablesService.GetSteamId();
        }
    }
}
