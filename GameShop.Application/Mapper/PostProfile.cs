using AutoMapper;
using GameShop.Data.Entities;
using GameShop.ViewModels.Catalog.Likes;
using GameShop.ViewModels.Catalog.Posts;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Application.Mapper
{
    public class PostProfile : Profile
    {
        public PostProfile() { 
            CreateMap<Post, PostResModel>();
            CreateMap<Post, PostCreateResModel>();
            CreateMap<Post, PostUpdateResModel>();
            CreateMap<Like, LikeDTO>();
        }
    }
}
