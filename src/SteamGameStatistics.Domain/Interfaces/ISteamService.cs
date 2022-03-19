using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SteamGameStatistics.Application.DTOs;

namespace SteamGameStatistics.Domain.Interfaces
{
    public  interface ISteamService
    {
        Task<PlayerDto> GetSteamUser();
        //Task<List<Game>> GetRecentlyPlayedGames();
       // Task<List<OwnedGame>> GetAllGamesFromSteam();
    }
}
