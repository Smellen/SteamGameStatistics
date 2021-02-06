using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        }
    }
}
