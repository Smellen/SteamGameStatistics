using System.Collections.Generic;
using System.Threading.Tasks;
using SteamGameStatistics.Models.Steam;

namespace SteamGameStatistics.Interfaces
{
    public interface ISteamService
    {
        Task<User> GetSteamUser(string steamId);
        Task<List<Game>> GetRecentlyPlayedGames(string steamId);
    }
}
