using AutoMapper;
using SteamGameStatistics.Application.DTOs;
using SteamGameStatistics.Data.DAOs;

namespace SteamGameStatistics
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            ApplyMappings();
        }

        private void ApplyMappings()
        {
            CreateMap<PlayerDto, PlayerDao>().ReverseMap();
            CreateMap<GameDto, GameDao>().ReverseMap();
        }
    }
}
