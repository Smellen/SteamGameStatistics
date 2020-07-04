using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteamGameStatistics.Interfaces
{
    public interface IFileService
    {
        Task<List<Models.Json.Game>> LoadGamesWithAchievementsFromFile(string sort);
        Task<List<Models.Steam.Game>> LoadAllGamesFromFile();
    }
}
