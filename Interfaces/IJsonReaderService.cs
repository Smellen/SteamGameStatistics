using System.Collections.Generic;
using System.Threading.Tasks;
using SteamGameStatistics.Models.Json;

namespace SteamGameStatistics.Interfaces
{
    public interface IJsonReaderService
    {
        Task<List<Game>> GetGamesFromFile(string sort);
    }
}
