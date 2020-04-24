using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SteamGameStatistics.Interfaces;
using SteamGameStatistics.Models;
using SteamGameStatistics.Models.Steam;

namespace SteamGameStatistics.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISteamService _steamService;

        public HomeController(ILogger<HomeController> logger, ISteamService steamService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _steamService = steamService ?? throw new ArgumentNullException(nameof(steamService));
        }

        public async Task<IActionResult> Index()
        {
            User steamUser = await _steamService.GetSteamUser();

            _logger.LogInformation($"Steam user loading: {steamUser?.Personaname}");

            return View(steamUser);
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Loading Privacy");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
