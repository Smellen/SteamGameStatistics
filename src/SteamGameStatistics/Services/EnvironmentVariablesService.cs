using System;
using Microsoft.Extensions.Logging;
using SteamGameStatistics.Enums;
using SteamGameStatistics.Interfaces;

namespace SteamGameStatistics.Services
{
    public class EnvironmentVariablesService : IEnvironmentVariablesService
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

        public bool SetSteamId(string steamId)
        {
            _logger.LogInformation($"Setting {steamId} as the steam id to use.");
            Environment.SetEnvironmentVariable(EnvironmentVariables.SteamId, steamId);
            return GetSteamId() == steamId;
        }

        public bool SetSteamKey(string steamKey)
        {
            _logger.LogInformation($"Setting {steamKey} as the steam key to use.");
            Environment.SetEnvironmentVariable(EnvironmentVariables.SteamKey, steamKey);
            return GetSteamKey() == steamKey;
        }
    }
}
