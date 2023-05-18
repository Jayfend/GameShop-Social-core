using AutoMapper;
using GameShop.Data.Entities;
using GameShop.ViewModels.Catalog.Games;
using GameShop.ViewModels.Catalog.Publishers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Application.Mapper
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<GameViewModel, GameElasticModel>()
                 .ForMember(dest => dest.ESId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<Game, GameElasticModel>()
                .ForMember(dest => dest.ESId, opt => opt.MapFrom(src => src.Id))
               .ReverseMap();
        }
    }
}
