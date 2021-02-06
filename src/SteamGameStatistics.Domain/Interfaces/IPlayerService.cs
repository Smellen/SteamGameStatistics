using System.Collections.Generic;
using System.Threading.Tasks;
using SteamGameStatistics.Application.DTOs;

namespace SteamGameStatistics.Domain.Interfaces
{
    public interface IPlayerService
    {
        Task<PlayerDto> AddPlayer(PlayerDto player);
        Task<PlayerDto> GetPlayer(string steamId);
        Task<List<PlayerDto>> GetAllPlayers();
    }
}