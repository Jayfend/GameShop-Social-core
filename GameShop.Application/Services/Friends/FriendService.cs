using AutoMapper;
using GameShop.Application.Common;
using GameShop.Data.EF;
using GameShop.Data.Entities;
using GameShop.Utilities.Exceptions;
using GameShop.ViewModels.Friends;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Services.Friends
{
    public class FriendService : IFriendService
    {
        private readonly ISaveFileService _saveFileService;
        private readonly UserManager<AppUser> _userManager;
        private readonly GameShopDbContext _context;
        private readonly IMapper _mapper;
        public FriendService(ISaveFileService saveFileService, GameShopDbContext context, IMapper mapper, UserManager<AppUser> userManager)
        {
            _saveFileService = saveFileService;
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<bool> AddFriendAsync(AddFriendReqModel req)
        {
            var friend = await _context.Friends.Where(x => (x.UserId1 == req.UserId1 || x.UserId2 == req.UserId1) && (x.UserId2 == req.UserId1 || x.UserId2 == req.UserId2)).FirstOrDefaultAsync();
            if(friend != null)
            {
                throw new GameShopException("Các bạn đã là bạn của nhau rồi");
            }
            var newFriend = new Friend()
            {
                UserId1 = req.UserId1,
                UserId2 = req.UserId2,
                CreatedDate = DateTime.Now,
                
            };
            await _context.Friends.AddAsync(newFriend);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
