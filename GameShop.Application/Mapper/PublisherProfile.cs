using AutoMapper;
using GameShop.Data.Entities;
using GameShop.ViewModels.Catalog.Comments;
using GameShop.ViewModels.Catalog.Publishers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Application.Mapper
{
    public class PublisherProfile : Profile
    {
        public PublisherProfile()
        {
            CreateMap<Publisher, PublisherDTO>();
        }
    }
}
