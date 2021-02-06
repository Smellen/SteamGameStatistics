using System.Collections.Generic;
using System.Threading.Tasks;
using SteamGameStatistics.Application.DTOs;
using SteamGameStatistics.Models.Steam;

namespace SteamGameStatistics.Interfaces
{
    public interface ISteamService
    {
        Task<PlayerDto> GetSteamUser();
        Task<List<Game>> GetRecentlyPlayedGames();
        Task<List<OwnedGame>> GetAllGamesFromSteam();
    }
}
