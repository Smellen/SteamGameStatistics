using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SteamGameStatistics.Application.DTOs;
using SteamGameStatistics.Domain.Interfaces;

namespace SteamGameStatistics.Domain.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
        }

        public async Task<PlayerDto> AddPlayer(PlayerDto player)
        {
            var exists = await this.GetPlayer(player.Steamid);
            if(exists == null)
            {
                player = await _playerRepository.AddPlayer(player);
            }

            return player;
        }

        public async Task<PlayerDto> GetPlayer(string steamId)
        {
            return await _playerRepository.GetPlayer(steamId);
        }

        public async Task<List<PlayerDto>> GetAllPlayers()
        {
            return await _playerRepository.GetAllPlayers();
        }
    }
}