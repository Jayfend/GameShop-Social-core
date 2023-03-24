using AutoMapper;
using GameShop.Application.Common;
using GameShop.Data.EF;
using GameShop.Data.Entities;
using GameShop.Utilities.Exceptions;
using GameShop.ViewModels.Catalog.Comments;
using GameShop.ViewModels.Catalog.Posts;
using Microsoft.AspNetCore.Http;
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
        private readonly GameShopDbContext _context;
        private readonly IMapper _mapper;
        public PostService(ISaveFileService saveFileService, GameShopDbContext context, IMapper mapper) 
        {
            _saveFileService = saveFileService;
            _context = context;
            _mapper = mapper;
        }
        public async Task<PostCreateResModel> CreateAsync(PostCreateReqModel req)
        {
            if(req == null)
            {
                throw new GameShopException("yêu cầu không được rỗng");

            }
            var newPost = new Post()
            {
                CreatorId = req.UserId,
                Content = req.Content,
                ImagePath = await _saveFileService.SaveFileAsync(req.Image),
                CreatedDate = DateTime.Now,
                Comments = new List<Comment>(),
                Likes = new List<Like>(),
                Status = true
                
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
