using System;
using Microsoft.Extensions.Logging;
using SteamGameStatistics.Domain.Interfaces;
using SteamGameStatistics.Enums;

namespace SteamGameStatistics.Domain.Services
{
    public  class EnvironmentVariablesService : IEnvironmentVariablesService
    {
        private readonly ILogger<EnvironmentVariablesService> _logger;

        public EnvironmentVariablesService(ILogger<EnvironmentVariablesService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public string GetSteamId()
        {
            return Environment.GetEnvironmentVariable(EnvironmentVariables.SteamId);
        }

        public string GetSteamKey()
        {
            return Environment.GetEnvironmentVariable(EnvironmentVariables.SteamKey);
        }

        public string SetSteamId(string steamId)
        {
            if (string.IsNullOrEmpty(steamId))
            {
                steamId = this.GetSteamId();
            }

            _logger.LogInformation($"Setting {steamId} as the steam id to use.");
            Environment.SetEnvironmentVariable(EnvironmentVariables.SteamId, steamId);

            return steamId;
        }

        public string SetSteamKey(string steamKey)
        {
            if (string.IsNullOrEmpty(steamKey))
            {
                steamKey = this.GetSteamKey();
            }

            _logger.LogInformation($"Setting {steamKey} as the steam key to use.");
            Environment.SetEnvironmentVariable(EnvironmentVariables.SteamKey, steamKey);
            return steamKey;
        }
    }
}
