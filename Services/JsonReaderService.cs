using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SteamGameStatistics.Interfaces;
using SteamGameStatistics.Models.Json;

namespace SteamGameStatistics.Services
{
    public class JsonReaderService : IJsonReaderService
    {
        /// <summary>
        /// Update this value
        /// </summary>
        private const string TempFileLocation = @"C:\Users\Ellen\Desktop\AllSteamGamesStats.json";

        public async Task<List<Game>> GetGamesFromFile()
        {
            return JsonConvert.DeserializeObject<List<Game>>(await File.ReadAllTextAsync(TempFileLocation));
        }
    }
}
