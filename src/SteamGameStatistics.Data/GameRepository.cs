using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SteamGameStatistics.Application.DTOs;
using SteamGameStatistics.Data.DAOs;
using SteamGameStatistics.Domain.Interfaces;

namespace SteamGameStatistics.Data
{
    public class GameRepository(GameDbContext context) : IGameRepository
    {
        private readonly GameDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        private GameDao MapGameDTOtoGameDAO(GameDto game)
        {
            return new()
            {
                AppId = game.AppId,
                Name = game.Name,
                Playtime = game.Playtime,
                IconUrl = game.IconUrl,
                WindowsPlaytime = game.WindowsPlaytime,
                MacPlaytime = game.MacPlaytime,
                LinuxPlaytime = game.LinuxPlaytime
            };
        }

        private GameDto MapGameDAOtoGameDTO(GameDao game)
        {
            return new ()
            {
                
                AppId = game.AppId,
                Name = game.Name,
                Playtime = game.Playtime,
                IconUrl = game.IconUrl,
                WindowsPlaytime = game.WindowsPlaytime,
                MacPlaytime = game.MacPlaytime,
                LinuxPlaytime = game.LinuxPlaytime
            };
        }

        public async Task<GameDto> AddGame(GameDto game)
        {
            var gameToBeAdded = MapGameDTOtoGameDAO(game);
            await _context.Games.AddAsync(gameToBeAdded);
            await _context.SaveChangesAsync();
            _context.Entry(gameToBeAdded).State = EntityState.Detached;

            return game;
        }

        public async Task<GameDto> GetGame(long appId)
        {
            var game = await _context.Games.AsNoTracking().SingleOrDefaultAsync(e => e.AppId.Equals(appId));

            return game == null ? null : MapGameDAOtoGameDTO(game);
        }

        public async Task<bool> RemoveGame(GameDto game)
        {
            var gameToBeRemoved = MapGameDTOtoGameDAO(game);
            _context.Games.Remove(gameToBeRemoved);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
