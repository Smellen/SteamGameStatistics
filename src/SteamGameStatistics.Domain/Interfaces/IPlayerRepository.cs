using System.Collections.Generic;
using System.Threading.Tasks;
using SteamGameStatistics.Application.DTOs;

namespace SteamGameStatistics.Domain.Interfaces
{
    public interface IPlayerRepository
    {
        Task<PlayerDto> AddPlayer(PlayerDto player);
        Task<PlayerDto> GetPlayer(string steamId);
        Task<List<PlayerDto>> GetAllPlayers();
        Task<bool> RemovePlayer(PlayerDto player);
    }
}
