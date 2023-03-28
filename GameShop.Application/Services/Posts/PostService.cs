using AutoMapper;
using GameShop.Application.Common;
using GameShop.Data.EF;
using GameShop.Data.Entities;
using GameShop.Utilities.Exceptions;
using GameShop.ViewModels.Catalog.Comments;
using GameShop.ViewModels.Catalog.Games;
using GameShop.ViewModels.Catalog.Posts;
using GameShop.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Services.Posts
{
    public class PostService : IPostService
    {   private readonly ISaveFileService _saveFileService;
        private readonly UserManager<AppUser> _userManager;
        private readonly GameShopDbContext _context;
        private readonly IMapper _mapper;
        public PostService(ISaveFileService saveFileService, GameShopDbContext context, IMapper mapper, UserManager<AppUser> userManager) 
        {
            _saveFileService = saveFileService;
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<PostCreateResModel> CreateAsync(PostCreateReqModel req)
        {
            if(req == null)
            {
                throw new GameShopException("yêu cầu không được rỗng");

            }
            var user = await _userManager.FindByIdAsync(req.UserId.ToString());
            if(user == null)
            {
                throw new GameShopException("Không tìm thấy người dùng");
            }
            var newPost = new Post()
            {
                CreatorId = req.UserId,
                Content = req.Content,
                ImagePath = await _saveFileService.SaveFileAsync(req.Image),
                CreatedDate = DateTime.Now,
                Comments = new List<Comment>(),
                Likes = new List<Like>(),
                Status = true,
                UserName = user.UserName
                
            };
            await  _context.Posts.AddAsync(newPost);
            await _context.SaveChangesAsync();
            return _mapper.Map<Post, PostCreateResModel>(newPost);
            
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var post =  await _context.Posts.Where(x => x.Id == id && x.Status == true).FirstOrDefaultAsync();
            if(post == null)
            {
                throw new GameShopException("Không tìm thấy bài viết");
            }
            post.Status = false;
             _context.Posts.Update(post);
             await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PagedResult<PostResModel>> GetAsync(GetPostPagingRequest req)
        {
            var query = _context.Posts.AsQueryable();

            // filter
            if (!string.IsNullOrEmpty(req.Keyword))
            {
                query = query.Where(x => x.Content.Contains(req.Keyword));
            }

            if (req.UserName != null)
            {
                query = query.Where(x => x.UserName == req.UserName);
            }
            //paging
            int totalrow = await query.CountAsync();
            var result = _mapper.Map<List<Post>, List<PostResModel>>(await query.ToListAsync());
            var pagedResult = new PagedResult<PostResModel>()
            {
                TotalRecords = totalrow,
                PageIndex = req.PageIndex,
                PageSize = req.PageSize,
                Items = result
            };
            return pagedResult;
        }

        public async Task<PostResModel> GetByIdAsync(Guid id)
        {
            var post = await _context.Posts.Where(x => x.Id == id && x.Status == true).FirstOrDefaultAsync();
            if (post == null)
            {
                throw new GameShopException("Không tìm thấy bài viết");
            }
            
            var result = _mapper.Map<Post, PostResModel>(post);
            return result;
        }

        public async Task<PostUpdateResModel> UpdateAsync(PostUpdateReqModel req)
        {
            var post = await _context.Posts.Where(x => x.Id == req.Id && x.Status == true).FirstOrDefaultAsync();
            if (post == null)
            {
                throw new GameShopException("Không tìm thấy bài viết");
            }
            post.Content = req.Content;
            post.ImagePath = await _saveFileService.SaveFileAsync(req.Image);
            post.UpdatedDate = DateTime.Now;
             _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return _mapper.Map<Post,PostUpdateResModel>(post);
        }
    }
}
