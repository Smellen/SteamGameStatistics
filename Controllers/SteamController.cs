using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SteamGameStatistics.Interfaces;
using SteamGameStatistics.Models.Json;

namespace SteamGameStatistics.Controllers
{
    public class SteamController : Controller
    {
        private const string NameSortField = "name";
        private const string AchievementCountSortField = "ac-count";

        private readonly ISteamService _steamService;
        private readonly IJsonReaderService _jsonReaderService;

        public SteamController(ISteamService steamService, IJsonReaderService jsonService)
        {
            _steamService = steamService ?? throw new ArgumentNullException(nameof(steamService));
            _jsonReaderService = jsonService ?? throw new ArgumentNullException(nameof(jsonService));
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetPlayer(string steamId)
        {
            var user = await _steamService.GetSteamUser(steamId);
            return View(user);
        }

        public async Task<ActionResult> GetRecentlyPlayedGames(string steamId)
        {
            var games = await _steamService.GetRecentlyPlayedGames(steamId);
            return View(games);
        }

        public async Task<ActionResult> DisplayAllGames(string sortOrder)
        {
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? NameSortField : string.Empty;

            var games = await _jsonReaderService.GetGamesFromFile();
            List<Game> sortedGames = null;
            sortedGames = sortOrder switch
            {
                NameSortField => games.OrderBy(e => e.Name).ToList(),
                AchievementCountSortField => games.OrderBy(e => e.Achievements.Count).ToList(),
                _ => games.OrderBy(e => e.Name).ToList(),
            };

            return View(sortedGames);
        }

        public async Task<ActionResult> LoadAllGames()
        {
            var games = await _steamService.LoadAllGamesFromFile();
            return View(games.OrderByDescending(e => e.PlaytimeForever).ToList());
        }
    }
}