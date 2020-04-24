using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SteamGameStatistics.Interfaces;

namespace SteamGameStatistics.Controllers
{
    public class SteamController : Controller
    {
        private readonly ILogger<SteamController> _logger;
        private readonly ISteamService _steamService;
        private readonly IFileReaderService _jsonReaderService;

        public SteamController(ILogger<SteamController> logger, ISteamService steamService, IFileReaderService jsonService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _steamService = steamService ?? throw new ArgumentNullException(nameof(steamService));
            _jsonReaderService = jsonService ?? throw new ArgumentNullException(nameof(jsonService));
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetPlayer()
        {
            var user = await _steamService.GetSteamUser();
            return View(user);
        }

        public async Task<ActionResult> GetRecentlyPlayedGames()
        {
            var games = await _steamService.GetRecentlyPlayedGames();

            return View(games);
        }

        public async Task<ActionResult> DisplayAllGames(string sortOrder)
        {
            var sortedGames = await _jsonReaderService.LoadGamesWithAchievementsFromFile(sortOrder);
            return View(sortedGames);
        }

        public async Task<ActionResult> LoadAllGames()
        {
            var games = await _jsonReaderService.LoadAllGamesFromFile();
            return View(games.OrderByDescending(e => e.PlaytimeForever).ToList());
        }
    }
}