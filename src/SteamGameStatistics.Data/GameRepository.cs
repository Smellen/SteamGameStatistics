using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SteamGameStatistics.Application.DTOs;
using SteamGameStatistics.Data.DAOs;
using SteamGameStatistics.Domain.Interfaces;

namespace SteamGameStatistics.Data
{
    public class GameRepository : IGameRepository
    {
        private readonly IMapper _mapper;
        private readonly GameDbContext _context;

        public GameRepository(IMapper mapper, GameDbContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<GameDto> AddGame(GameDto game)
        {
            var gameToBeAdded = _mapper.Map<GameDao>(game);
            await _context.Games.AddAsync(gameToBeAdded);
            await _context.SaveChangesAsync();
            _context.Entry(gameToBeAdded).State = EntityState.Detached;

            return game;
        }

        public async Task<GameDto> GetGame(long appId)
        {
            var game = await _context.Games.AsNoTracking().SingleOrDefaultAsync(e => e.AppId.Equals(appId));

            return game == null ? null : _mapper.Map<GameDto>(game);
        }

        public async Task<bool> RemoveGame(GameDto game)
        {
            var gameToBeRemoved = _mapper.Map<GameDao>(game);
            _context.Games.Remove(gameToBeRemoved);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
