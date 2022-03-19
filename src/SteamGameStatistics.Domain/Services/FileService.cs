using System;
using Microsoft.Extensions.Logging;
using SteamGameStatistics.Domain.Interfaces;

namespace SteamGameStatistics.Domain.Services
{
    public class FileService : IFileService
    {
        private const string NameSortField = "name";
        private const string AchievementCountSortField = "ac-count";
        private const string AllGamesFileName = "all-games.json";

        private readonly ILogger<FileService> _logger;

        public FileService(ILogger<FileService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

    }
}