using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using SteamGameStatistics.Application.DTOs;
using SteamGameStatistics.Data.DAOs;
using SteamGameStatistics.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SteamGameStatistics.Data
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IMapper _mapper;
        private readonly GameDbContext _context;

        public PlayerRepository(IMapper mapper, GameDbContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<PlayerDto> AddPlayer(PlayerDto player)
        {
            var playerToBeAdded = _mapper.Map<PlayerDao>(player);
            await _context.Players.AddAsync(playerToBeAdded);
            await _context.SaveChangesAsync();
            _context.Entry(playerToBeAdded).State = EntityState.Detached;

            return player;
        }

        public async Task<PlayerDto> GetPlayer(string steamId)
        {
            var player = await _context.Players.AsNoTracking().SingleOrDefaultAsync(e => e.Steamid == steamId);

            return player == null ? null : _mapper.Map<PlayerDto>(player);
        }

        public async Task<List<PlayerDto>> GetAllPlayers()
        {
            var players = await _context.Players.AsNoTracking().ToListAsync();
            if (players == null || !players.Any())
            {
                return null;
            }

            return _mapper.Map<List<PlayerDto>>(players);
        }

        public async Task<bool> RemovePlayer(PlayerDto player)
        {
            var playerToBeDeleted = _mapper.Map<PlayerDao>(player);
            _context.Players.Remove(playerToBeDeleted);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
