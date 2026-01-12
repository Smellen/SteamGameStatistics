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
    public class PlayerRepository(GameDbContext context) : IPlayerRepository
    {
        private readonly GameDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        private PlayerDao MapPlayerDTOtoPlayerDAO(PlayerDto player)
        {
            return new PlayerDao()
            {
                Steamid = player.Steamid,
                CommunityVisibilityState = player.Communityvisibilitystate,
                Profilestate = player.Profilestate,
                PersonaName = player.Personaname,
                ProfileUrl = player.Profileurl,
                Avatar = player.Avatar,
                AvatarMedium = player.Avatarmedium,
                AvatarFull = player.Avatarfull,
                AvatarHash = player.AvatarHash,
                Lastlogoff = player.Lastlogoff,
                PersonaState = player.Personastate,
                RealName = player.Realname,
                PrimaryClanid = player.Primaryclanid,
                TimeCreated = player.Timecreated,
                PersonaStateFlags = player.Personastateflags,
                LocCountryCode = player.Loccountrycode,
                LocStateCode = player.Locstatecode                
            };
        }

        private PlayerDto MapPlayerDAOtoPlayerDTO(PlayerDao player)
        {
            return new PlayerDto()
            {
                Steamid = player.Steamid,
                Communityvisibilitystate = player.CommunityVisibilityState,
                Profilestate = player.Profilestate,
                Personaname = player.PersonaName,
                Profileurl = player.ProfileUrl,
                Avatar = player.Avatar,
                Avatarmedium = player.AvatarMedium,
                Avatarfull = player.AvatarFull,
                AvatarHash = player.AvatarHash,
                Lastlogoff = player.Lastlogoff,
                Personastate = player.PersonaState,
                Realname = player.RealName,
                Primaryclanid = player.PrimaryClanid,
                Timecreated = player.TimeCreated,
                Personastateflags = player.PersonaStateFlags,
                Loccountrycode = player.LocCountryCode,
                Locstatecode = player.LocStateCode
            };
        }

        public async Task<PlayerDto> AddPlayer(PlayerDto player)
        {
            var playerToBeAdded = MapPlayerDTOtoPlayerDAO(player);
            await _context.Players.AddAsync(playerToBeAdded);
            await _context.SaveChangesAsync();
            _context.Entry(playerToBeAdded).State = EntityState.Detached;

            return player;
        }

        public async Task<PlayerDto> GetPlayer(string steamId)
        {
            var player = await _context.Players.AsNoTracking().SingleOrDefaultAsync(e => e.Steamid == steamId);

            return player == null ? null : MapPlayerDAOtoPlayerDTO(player);
        }

        public async Task<List<PlayerDto>> GetAllPlayers()
        {
            var players = await _context.Players.AsNoTracking().ToListAsync();
            if (players == null || !players.Any())
            {
                return null;
            }

            var mappedPlayers = new List<PlayerDto>();
            foreach(var p in players)
            {
                mappedPlayers.Add(MapPlayerDAOtoPlayerDTO(p));
            }

            return mappedPlayers;
        }

        public async Task<bool> RemovePlayer(PlayerDto player)
        {
            var playerToBeDeleted = MapPlayerDTOtoPlayerDAO(player);
            _context.Players.Remove(playerToBeDeleted);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
