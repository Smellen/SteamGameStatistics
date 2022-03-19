using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SteamGameStatistics.Application.DTOs;
using SteamGameStatistics.Domain.Interfaces;
using SteamGameStatistics.Models;

namespace SteamGameStatistics.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPlayerService _playerService;
        private readonly ISteamService _steamService;
        private readonly IEnvironmentVariablesService _environmentVariablesService;
        protected string SteamId { get; set; }
        protected string SteamKey { get; set; }

        public HomeController(ILogger<HomeController> logger, IPlayerService playerService, ISteamService steamService, IEnvironmentVariablesService environmentVariablesService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _playerService = playerService ?? throw new ArgumentNullException(nameof(playerService));
            _steamService = steamService ?? throw new ArgumentNullException(nameof(steamService));
            _environmentVariablesService = environmentVariablesService ?? throw new ArgumentNullException(nameof(environmentVariablesService));
        }

        public async Task<IActionResult> Index(string steamId, string steamKey)
        {
            if (string.IsNullOrEmpty(steamId) || string.IsNullOrEmpty(steamKey))
            {
                SetEnvironmentVariables(steamId, steamKey);
            }

            PlayerDto steamUser = await _playerService.GetPlayer(SteamId);
            if (steamUser == null)
            {
                steamUser = await _steamService.GetSteamUser();
                await _playerService.AddPlayer(steamUser);
            }

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
            SteamId = _environmentVariablesService.SetSteamId(steamId);
            SteamKey = _environmentVariablesService.SetSteamKey(steamKey);
        }
    }
}
