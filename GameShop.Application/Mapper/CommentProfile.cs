using AutoMapper;
using GameShop.Data.Entities;
using GameShop.ViewModels.Catalog.Comments;

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
