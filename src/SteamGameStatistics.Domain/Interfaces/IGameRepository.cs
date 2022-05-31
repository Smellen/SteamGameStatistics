using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SteamGameStatistics.Application.DTOs;

namespace SteamGameStatistics.Domain.Interfaces
{
    public interface IGameRepository
    {
        Task<GameDto> AddGame(GameDto game);
        Task<GameDto> GetGame(long appId);
        Task<bool> RemoveGame(GameDto game);
    }
}
