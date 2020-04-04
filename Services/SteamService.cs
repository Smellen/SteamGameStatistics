using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SteamGameStatistics.Interfaces;
using SteamGameStatistics.Models.Steam;
using SteamGameStatistics.Models.Steam.Responses;

namespace SteamGameStatistics.Services
{
    public class SteamService : ISteamService
    {
        private const string SteamKey = "STEAM KEY";
        private const string SteamId = "STEAM ID";

        public readonly HttpClient _client;

        public SteamService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <summary>
        /// Get a steam user.
        /// </summary>
        /// <param name="steamId">The steam identifier.</param>
        /// <returns>A steam user.</returns>
        public async Task<User> GetSteamUser(string steamId)
        {
            if (string.IsNullOrWhiteSpace(steamId))
            {
                steamId = SteamId;
            }

            User user = null;
            var uri = new Uri($"http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={SteamKey}&steamids={steamId}");
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var responseStr = await response.Content.ReadAsStringAsync();
                var userResponse = JsonSerializer.Deserialize<PlayerInformation>(responseStr);
                user = userResponse.Response.Users.FirstOrDefault();
            }

            return user;
        }

        /// <summary>
        /// Gets the recently played games.
        /// </summary>
        /// <param name="steamId">The steam identifier.</param>
        /// <returns>A list of recently played games.</returns>
        public async Task<List<Game>> GetRecentlyPlayedGames(string steamId)
        {
            if (string.IsNullOrWhiteSpace(steamId))
            {
                steamId = SteamId;
            }

            List<Game> games = null;
            var uri = new Uri($"http://api.steampowered.com/IPlayerService/GetRecentlyPlayedGames/v0001/?key={SteamKey}&steamid={SteamId}&format=json");
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var responseStr = await response.Content.ReadAsStringAsync();
                var recentlyPlayedGamesResponse = JsonSerializer.Deserialize<SteamResponse>(responseStr);
                games = recentlyPlayedGamesResponse.Response.Games.ToList();
            }

            return games;
        }

        public async Task<List<Game>> GetAllGamesFromSteam()
        {
            List<Game> games = null;
            var uri = new Uri($"http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={SteamKey}&steamid={SteamId}&format=json&include_appinfo=true");
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var responseStr = await response.Content.ReadAsStringAsync();
                SaveFile("all-games", responseStr);
                var recentlyPlayedGamesResponse = JsonSerializer.Deserialize<SteamResponse>(responseStr);
                games = recentlyPlayedGamesResponse.Response.Games.ToList();
            }

            return games;
        }

        public async Task<List<Game>> LoadAllGamesFromFile()
        {
            var fileContent = await LoadFile("all-games");
            var games = JsonSerializer.Deserialize<SteamResponse>(fileContent);

            return games.Response.Games.ToList();
        }

        private bool SaveFile(string filename, string content)
        {
            try
            {
                using (var sw = new StreamWriter($@"C:\Users\Ellen\Desktop\steamdata\{filename}.json"))
                {
                    sw.WriteLineAsync(content);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private async Task<string> LoadFile(string filename)
        {
            string result = string.Empty;

            try
            {
                using (var sr = new StreamReader($@"C:\Users\Ellen\Desktop\steamdata\{filename}.json"))
                {
                    result = await sr.ReadToEndAsync();
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }

            return result;
        }
    }
}
