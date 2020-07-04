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
        private readonly IFileService _jsonReaderService;

        public SteamController(ILogger<SteamController> logger, ISteamService steamService, IFileService jsonService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _steamService = steamService ?? throw new ArgumentNullException(nameof(steamService));
            _jsonReaderService = jsonService ?? throw new ArgumentNullException(nameof(jsonService));
        }

        public ActionResult Index()
        {
            _logger.LogInformation("Loading the steam controller index.");
            
            return View();
        }

        public async Task<ActionResult> GetPlayer()
        {
            var user = await _steamService.GetSteamUser();

            _logger.LogInformation($"Loading the steam user: {user.Personaname}");

            return View(user);
        }

        public async Task<ActionResult> GetRecentlyPlayedGames()
        {
            _logger.LogInformation("Loading recently played games for steam controller.");

            var games = await _steamService.GetRecentlyPlayedGames();

            return View(games);
        }

        public async Task<ActionResult> DisplayAllGames(string sortOrder)
        {
            _logger.LogInformation("Loading all games for steam controller.");
            var sortedGames = await _jsonReaderService.LoadGamesWithAchievementsFromFile(sortOrder);
            return View(sortedGames);
        }

        public async Task<ActionResult> LoadAllGames()
        {
            _logger.LogInformation("Loading recently played games for steam controller from file.");

            var games = await _jsonReaderService.LoadAllGamesFromFile();
            return View(games.OrderByDescending(e => e.PlaytimeForever).ToList());
        }
    }
}