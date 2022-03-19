using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SteamGameStatistics.Domain.Interfaces;

namespace SteamGameStatistics.Controllers
{
    public class SteamController : Controller
    {
        private readonly ILogger<SteamController> _logger;
        private readonly ISteamService _steamService;

        public SteamController(ILogger<SteamController> logger, ISteamService steamService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _steamService = steamService ?? throw new ArgumentNullException(nameof(steamService));
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

            //var games = await _steamService.GetRecentlyPlayedGames();

            return View(null);
        }

        public async Task<ActionResult> DisplayAllGames(string sortOrder)
        {
            _logger.LogInformation("Loading all games for steam controller.");
            //var sortedGames = await _steamService.GetAllGamesFromSteam();

            //if(sortOrder == "name")
            //{
            //    sortedGames = sortedGames.OrderBy(e => e.Name).ToList();
            //}
            //else if (sortOrder == "tpt")
            //{
            //    sortedGames = sortedGames.OrderByDescending(e => e.TotalPlaytime).ToList();
            //}

            return View(null);
        }

        public async Task<ActionResult> LoadAllGames()
        {
            throw new NotImplementedException();
        }
    }
}