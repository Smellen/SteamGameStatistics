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
        private readonly IEnvironmentVariablesService _environmentVariablesService;

        public HomeController(ILogger<HomeController> logger, ISteamService steamService, IEnvironmentVariablesService environmentVariablesService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _steamService = steamService ?? throw new ArgumentNullException(nameof(steamService));
            _environmentVariablesService = environmentVariablesService ?? throw new ArgumentNullException(nameof(environmentVariablesService));
        }

        public async Task<IActionResult> Index(string steamId, string steamKey)
        {
            if (!string.IsNullOrEmpty(steamId) || !string.IsNullOrEmpty(steamKey))
            {
                SetEnvironmentVariables(steamId, steamKey);
            }

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

        private void SetEnvironmentVariables(string steamId, string steamKey)
        {
            _environmentVariablesService.SetSteamId(steamId);
            _environmentVariablesService.SetSteamKey(steamKey);
        }
    }
}
