using System.Collections.Generic;
using System.Threading.Tasks;
using SteamGameStatistics.Models.Steam;

namespace SteamGameStatistics.Interfaces
{
    public interface ISteamService
    {
        Task<User> GetSteamUser();
        Task<List<Game>> GetRecentlyPlayedGames();
        Task<bool> GetAllGamesFromSteam();
        Task<List<Game>> LoadAllGamesFromFile();
    }
}
