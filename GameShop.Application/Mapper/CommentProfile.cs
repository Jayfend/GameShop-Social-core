using AutoMapper;
using GameShop.Data.Entities;
using GameShop.ViewModels.Catalog.Comments;
using GameShop.ViewModels.Catalog.Likes;
using GameShop.ViewModels.Catalog.Posts;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Application.Mapper
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        { 
                CreateMap<Comment,CommentDTO>();  
        }
    }
}
