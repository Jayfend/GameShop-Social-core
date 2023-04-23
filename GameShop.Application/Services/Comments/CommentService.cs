using AutoMapper;
using GameShop.Application.Common;
using GameShop.Application.Mapper;
using GameShop.Data.EF;
using GameShop.Data.Entities;
using GameShop.Utilities.Exceptions;
using GameShop.ViewModels.Catalog.Comments;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Services.Comments
{
    public class CommentService : ICommentService
    {
        private readonly GameShopDbContext _context;
        private readonly IStorageService _storageService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public CommentService(GameShopDbContext context, IStorageService storageService,UserManager<AppUser> userManager,IMapper mapper)
        {
            _context = context;
            _storageService = storageService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<CommentDTO> CreateComment(CommentCreateReqDTO req)
        {
            if(req == null)
            {
                throw new GameShopException("Yêu cầu không được rỗng");
            }
            var user = await _userManager.FindByIdAsync(req.UserId.ToString());
            if(user == null)
            {
                throw new GameShopException("Không tìm thấy user");
            }
            var game = await _context.Games.Where(x => x.Id == req.GameId).FirstOrDefaultAsync();
            if(game == null)
            {
                throw new GameShopException("không tìm thấy game");
            }
            var newComment = new Comment()
            {
               Game = game,
               AppUser = user,
                Content = req.Content,
               
            };
            await _context.Comments.AddAsync(newComment);
            await _context.SaveChangesAsync();
           return _mapper.Map<CommentDTO>(newComment);

        }
    }
}
